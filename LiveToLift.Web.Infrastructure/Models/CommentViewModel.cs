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
    public class CommentViewModel : BaseViewModel, IMapFrom<Comment>, IHaveCustomMappings
    {

        public int? FitnessProgramId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserPicture { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
             .ForMember(m => m.UserName, opt => opt.MapFrom(t => t.User != null ? t.User.Name : null))
             .ForMember(m => m.UserPicture, opt => opt.MapFrom(t => t.User != null ? t.User.PhotoUrl : null))

             .ReverseMap();
        }
    }
}
  