using HtmlAgilityPack;
using LiveToLift.Data;
using LiveToLift.Models;
using LiveToLift.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LiveToLift.Crawler
{
    public class Crawler
    {


        public static void Main(string[] args)
        {
            StartCrawlerAsync();

            Console.ReadLine();
        }

        private static void StartCrawlerAsync()
        {
            using (ApplicationDbContext data = new ApplicationDbContext())
            {
                var url = "http://darebee.com/programs.html";
                var httpCLient = new HttpClient();

                var html = httpCLient.GetStringAsync(url).Result;

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var divs = htmlDocument.DocumentNode.Descendants("article").Where(node => node.GetAttributeValue("class", "").Equals("item-view blog-view")).ToList();

                foreach (var div in divs)
                {
                    string text = div?.Descendants("h2")?.FirstOrDefault()?.InnerText;
                    var fitnesProgram = new FitnessProgram
                    {
                        Name = Regex.Replace(text != null ? text :"", @"\t|\n|\r|", "").Trim() ,
                        PhotoUrl = "http://darebee.com/"  + div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value,
                        CreatedOn = DateTime.Now
                        
                    };

                    var link = "http://darebee.com/" + div.Descendants("a").FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value;
                    var htmlDetails = httpCLient.GetStringAsync(link).Result;
                    var htmlDetailsDocument = new HtmlDocument();
                    htmlDetailsDocument.LoadHtml(htmlDetails);
                    Console.WriteLine(htmlDetailsDocument);
                    data.FitnessPrograms.Add(fitnesProgram);
                
                }

                data.SaveChanges();

            }
        }


    }
}
