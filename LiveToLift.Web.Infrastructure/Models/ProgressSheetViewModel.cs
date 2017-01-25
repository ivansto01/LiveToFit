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
    public class ProgressSheetViewModel: BaseViewModel, IMapFrom<ProgressSheet>,IHaveCustomMappings
    {
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        public ICollection<ExerciseInstanceViewModel> ExerciseInstances { get; set; }

        public string PhotoUrl { get; set; }

        public string VideoUrl { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<ProgressSheet, ProgressSheetViewModel>()
             .ForMember(m => m.UserName, opt => opt.MapFrom(t => t.User != null ? t.User.Name : null))

             .ReverseMap();
        }
    }
}
