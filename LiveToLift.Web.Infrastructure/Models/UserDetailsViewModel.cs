using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class UserDetailsViewModel : BaseViewModel, IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public int TotalFitnesProgramInstances { get; set; }

        public int TotalProgressSheets { get; set; }

        public int AverageRating { get; set; }



        public void CreateMappings(IConfiguration configuration)
        {
            

            configuration.CreateMap<ApplicationUser, UserDetailsViewModel>()
            .ForMember(m => m.TotalFitnesProgramInstances, opt => opt.MapFrom(t => t.FitnessProgramInstances.Count))
               .ForMember(m => m.TotalFitnesProgramInstances, opt => opt.MapFrom(t => t.ProgressSheets.Count))
                 .ForMember(m => m.AverageRating, opt => opt.MapFrom(t => t.Ratings.Average(s => s.Value)))
             .ReverseMap();

        }
    }
}
