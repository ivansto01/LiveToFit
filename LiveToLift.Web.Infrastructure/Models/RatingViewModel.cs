using AutoMapper;
using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class RatingViewModel : BaseViewModel, IMapFrom<Rating>, IHaveCustomMappings
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {


            configuration.CreateMap<Rating, RatingViewModel>()
            .ForMember(m => m.Name, opt => opt.MapFrom(t => t.User != null ? t.User.Name : null))
               .ForMember(m => m.PhotoUrl, opt => opt.MapFrom(t => t.User != null ? t.User.PhotoUrl : null))
                 
             .ReverseMap();

        }
    }
}
