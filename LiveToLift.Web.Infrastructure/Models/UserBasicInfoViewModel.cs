using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class UserBasicInfoViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

    }
}
