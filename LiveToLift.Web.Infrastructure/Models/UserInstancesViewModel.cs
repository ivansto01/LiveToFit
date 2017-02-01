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
    public class UserInstancesViewModel : BaseViewModel, IMapFrom<FitnessProgramInstance>, IHaveCustomMappings
    {
        public string AppUserName { get; set; }


        public string FitnessProgramName { get; set; }
        public virtual FitnessProgramViewModel FitnessProgram { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {


            configuration.CreateMap<FitnessProgramInstance, UserInstancesViewModel>()

            .ForMember(m => m.AppUserName, opt => opt.MapFrom(t => t.ApplicationUsers.Name != null ? t.ApplicationUsers.Name : null))
               
             .ReverseMap();

        }
    }
}
