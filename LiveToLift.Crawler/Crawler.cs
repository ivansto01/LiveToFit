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
            var ExerciseTricepsCrawlingUrl = "http://www.total-gym-exercises.com/exercises/triceps/index.html";
            var ExerciseTricepsCrawlingImageUrl = "http://www.total-gym-exercises.com/exercises/triceps/";

            var ExerciseBicepsCrawlingUrl = "http://www.total-gym-exercises.com/exercises/biceps/index.html";
            var ExerciseBicepsCrawlingImage = "http://www.total-gym-exercises.com/exercises/biceps/";

            var ExerciseShouldersCrawlingUrl = "http://www.total-gym-exercises.com/exercises/shoulders/index.html";
            var ExerciseShouldersCrawlingImage = "http://www.total-gym-exercises.com/exercises/shoulders/";

            var ExerciseLegsCrawlingUrl = "http://www.total-gym-exercises.com/exercises/legs/index.html";
            var ExerciseLegsCrawlingImage = "http://www.total-gym-exercises.com/exercises/legs/";

            var ExerciseChestCrawlingUrl = "http://www.total-gym-exercises.com/exercises/chest/index.html";
            var ExerciseChestCrawlingImage = "http://www.total-gym-exercises.com/exercises/chest/";

            var ExerciseBackCrawlingUrl = "http://www.total-gym-exercises.com/exercises/back/index.html";
            var ExerciseBackCrawlingImage = "http://www.total-gym-exercises.com/exercises/back/";

            var ExerciseAbsCrawilingUrl = "http://www.total-gym-exercises.com/exercises/abs/index.html";
            var ExerciseAbsCrawilingImage = "http://www.total-gym-exercises.com/exercises/abs/";

            
            ExerciseCrawiling(ExerciseAbsCrawilingUrl, ExerciseAbsCrawilingImage);
            ExerciseCrawiling(ExerciseBackCrawlingUrl, ExerciseBackCrawlingImage);
            ExerciseCrawiling(ExerciseChestCrawlingUrl, ExerciseChestCrawlingImage);
            ExerciseCrawiling(ExerciseLegsCrawlingUrl, ExerciseLegsCrawlingImage);
            ExerciseCrawiling(ExerciseShouldersCrawlingUrl, ExerciseShouldersCrawlingImage);
            ExerciseCrawiling(ExerciseBicepsCrawlingUrl, ExerciseBicepsCrawlingImage);
            ExerciseCrawiling(ExerciseTricepsCrawlingUrl, ExerciseTricepsCrawlingImageUrl);

            //FitnesProgramCrawiling();
            Console.WriteLine("End");
            Console.ReadLine();

            
        }

        private static void FitnesProgramCrawiling()
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
                        Name = Regex.Replace(text != null ? text : "", @"\t|\n|\r|", "").Trim(),
                        PhotoUrl = "http://darebee.com/" + div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value,
                        CreatedOn = DateTime.Now

                    };


                    if (!data.FitnessPrograms.Any(f=>f.Name == fitnesProgram.Name))
                    {
                        data.FitnessPrograms.Add(fitnesProgram);
                    }
                    else
                    {
                        throw new ArgumentException("Cannot dublicate same Fitness Programs");
                    }

                }

                data.SaveChanges();

            }
        }

        private static void ExerciseCrawiling(string url, string imagesUrl)
        {
            using (ApplicationDbContext data = new ApplicationDbContext())
            {
                //var url = "http://www.total-gym-exercises.com/exercises/abs/index.html";
                var httpClient = new HttpClient();
                var html = httpClient.GetStringAsync(url).Result;
                //var imagesAbsUrl = "http://www.total-gym-exercises.com/exercises/abs/";

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("boxContainerDiv")).ToList();

                foreach (var div in divs)
                {
                    var exercise = new Exercise
                    {
                        Name = div?.Descendants("a")?.FirstOrDefault()?.InnerText,
                        Description = div?.Descendants("li")?.FirstOrDefault()?.InnerText,
                        PhotoUrl = imagesUrl + div?.Descendants("img")?.FirstOrDefault()?.ChildAttributes("src")?.FirstOrDefault()?.Value
                    };
                    if (exercise.Name == null || exercise.Name.StartsWith("Show More"))
                    {
                        continue;
                    }

                    if (!data.Exercises.Any(e=>e.Name == exercise.Name))
                    {
                        data.Exercises.Add(exercise);
                    }
                    else
                    {
                        throw new ArgumentException("Cannot dublicate same Exercises");
                    }
                }
                data.SaveChanges();
            }
        }

        //private static void ExerciseBackCrawling()
        //{
        //    using (ApplicationDbContext data = new ApplicationDbContext())
        //    {
        //        var url = "http://www.total-gym-exercises.com/exercises/back/index.html";
        //        var httpClient = new HttpClient();
        //        var html = httpClient.GetStringAsync(url).Result;
        //        var imagesAbsUrl = "http://www.total-gym-exercises.com/exercises/back/";

        //        var htmlDocument = new HtmlDocument();
        //        htmlDocument.LoadHtml(html);

        //        var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("boxContainerDiv")).ToList();

        //        foreach (var div in divs)
        //        {
        //            var exercise = new Exercise
        //            {
        //                Name = div?.Descendants("a")?.FirstOrDefault()?.InnerText,
        //                Description = div?.Descendants("li")?.FirstOrDefault()?.InnerText,
        //                PhotoUrl = imagesAbsUrl + div?.Descendants("img")?.FirstOrDefault()?.ChildAttributes("src")?.FirstOrDefault()?.Value
        //            };
        //            if (!data.Exercises.Any(e => e.Name == exercise.Name))
        //            {
        //                data.Exercises.Add(exercise);
        //            }
        //            else
        //            {
        //                throw new ArgumentException("Cannot dublicate same Exercises");
        //            }
        //        }
        //        data.SaveChanges();
        //    }
        //}

        //private static void ExerciseChestCrawling()
        //{
        //    using (ApplicationDbContext data = new ApplicationDbContext())
        //    {
        //        var url = "http://www.total-gym-exercises.com/exercises/chest/index.html";
        //        var httpClient = new HttpClient();
        //        var html = httpClient.GetStringAsync(url).Result;
        //        var imagesAbsUrl = "http://www.total-gym-exercises.com/exercises/chest/";

        //        var htmlDocument = new HtmlDocument();
        //        htmlDocument.LoadHtml(html);

        //        var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("boxContainerDiv")).ToList();

        //        foreach (var div in divs)
        //        {
        //            var exercise = new Exercise
        //            {
        //                Name = div?.Descendants("a")?.FirstOrDefault()?.InnerText,
        //                Description = div?.Descendants("li")?.FirstOrDefault()?.InnerText,
        //                PhotoUrl = imagesAbsUrl + div?.Descendants("img")?.FirstOrDefault()?.ChildAttributes("src")?.FirstOrDefault()?.Value
        //            };
        //            if (!data.Exercises.Any(e => e.Name == exercise.Name))
        //            {
        //                data.Exercises.Add(exercise);
        //            }
        //            else
        //            {
        //                throw new ArgumentException("Cannot dublicate same Exercises");
        //            }
        //        }
        //        data.SaveChanges();
        //    }
        //}

        //private static void ExerciseLegsCrawling()
        //{
        //    using (ApplicationDbContext data = new ApplicationDbContext())
        //    {
        //        var url = "http://www.total-gym-exercises.com/exercises/legs/index.html";
        //        var httpClient = new HttpClient();
        //        var html = httpClient.GetStringAsync(url).Result;
        //        var imagesAbsUrl = "http://www.total-gym-exercises.com/exercises/legs/";

        //        var htmlDocument = new HtmlDocument();
        //        htmlDocument.LoadHtml(html);

        //        var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("boxContainerDiv")).ToList();

        //        foreach (var div in divs)
        //        {
        //            var exercise = new Exercise
        //            {
        //                Name = div?.Descendants("a")?.FirstOrDefault()?.InnerText,
        //                Description = div?.Descendants("li")?.FirstOrDefault()?.InnerText,
        //                PhotoUrl = imagesAbsUrl + div?.Descendants("img")?.FirstOrDefault()?.ChildAttributes("src")?.FirstOrDefault()?.Value
        //            };
        //            if (!data.Exercises.Any(e => e.Name == exercise.Name))
        //            {
        //                data.Exercises.Add(exercise);
        //            }
        //            else
        //            {
        //                throw new ArgumentException("Cannot dublicate same Exercises");
        //            }
        //        }
        //        data.SaveChanges();
        //    }
        //}

        //private static void ExerciseShouldersCrawling()
        //{
        //    using (ApplicationDbContext data = new ApplicationDbContext())
        //    {
        //        var url = "http://www.total-gym-exercises.com/exercises/shoulders/index.html";
        //        var httpClient = new HttpClient();
        //        var html = httpClient.GetStringAsync(url).Result;
        //        var imagesAbsUrl = "http://www.total-gym-exercises.com/exercises/shoulders/";

        //        var htmlDocument = new HtmlDocument();
        //        htmlDocument.LoadHtml(html);

        //        var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("boxContainerDiv")).ToList();

        //        foreach (var div in divs)
        //        {
        //            var exercise = new Exercise
        //            {
        //                Name = div?.Descendants("a")?.FirstOrDefault()?.InnerText,
        //                Description = div?.Descendants("li")?.FirstOrDefault()?.InnerText,
        //                PhotoUrl = imagesAbsUrl + div?.Descendants("img")?.FirstOrDefault()?.ChildAttributes("src")?.FirstOrDefault()?.Value
        //            };
        //            if (!data.Exercises.Any(e => e.Name == exercise.Name))
        //            {
        //                data.Exercises.Add(exercise);
        //            }
        //            else
        //            {
        //                throw new ArgumentException("Cannot dublicate same Exercises");
        //            }
        //        }
        //        data.SaveChanges();
        //    }
        //}

        //private static void ExerciseBicepsCrawling()
        //{
        //    using (ApplicationDbContext data = new ApplicationDbContext())
        //    {
        //        var url = "http://www.total-gym-exercises.com/exercises/biceps/index.html";
        //        var httpClient = new HttpClient();
        //        var html = httpClient.GetStringAsync(url).Result;
        //        var imagesAbsUrl = "http://www.total-gym-exercises.com/exercises/biceps/";

        //        var htmlDocument = new HtmlDocument();
        //        htmlDocument.LoadHtml(html);

        //        var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("boxContainerDiv")).ToList();

        //        foreach (var div in divs)
        //        {
        //            var exercise = new Exercise
        //            {
        //                Name = div?.Descendants("a")?.FirstOrDefault()?.InnerText,
        //                Description = div?.Descendants("li")?.FirstOrDefault()?.InnerText,
        //                PhotoUrl = imagesAbsUrl + div?.Descendants("img")?.FirstOrDefault()?.ChildAttributes("src")?.FirstOrDefault()?.Value
        //            };
        //            if (exercise.Name == null)
        //            {
        //                continue;
        //            }
        //            if (!data.Exercises.Any(e => e.Name == exercise.Name))
        //            {
        //                data.Exercises.Add(exercise);
        //            }
        //            else
        //            {
        //                throw new ArgumentException("Cannot dublicate same Exercises");
        //            }
        //        }
        //        data.SaveChanges();
        //    }
        //}

        //private static void ExerciseTricepsCrawling()
        //{
        //    using (ApplicationDbContext data = new ApplicationDbContext())
        //    {
        //        var url = "http://www.total-gym-exercises.com/exercises/triceps/index.html";
        //        var httpClient = new HttpClient();
        //        var html = httpClient.GetStringAsync(url).Result;
        //        var imagesAbsUrl = "http://www.total-gym-exercises.com/exercises/triceps/";

        //        var htmlDocument = new HtmlDocument();
        //        htmlDocument.LoadHtml(html);

        //        var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("boxContainerDiv")).ToList();

        //        foreach (var div in divs)
        //        {
        //            var exercise = new Exercise
        //            {
        //                Name = div?.Descendants("a")?.FirstOrDefault()?.InnerText,
        //                Description = div?.Descendants("li")?.FirstOrDefault()?.InnerText,
        //                PhotoUrl = imagesAbsUrl + div?.Descendants("img")?.FirstOrDefault()?.ChildAttributes("src")?.FirstOrDefault()?.Value
        //            };
        //            if (!data.Exercises.Any(e => e.Name == exercise.Name))
        //            {
        //                data.Exercises.Add(exercise);
        //            }
        //            else
        //            {
        //                throw new ArgumentException("Cannot dublicate same Exercises");
        //            }
        //        }
        //        data.SaveChanges();
        //    }
        //}
    }
}
                  
                
        

