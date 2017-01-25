using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class ApplicationUserRole : IdentityUserRole
    {
        public ApplicationUserRole()
            : base()
        { }

        public string RoleId { get; set; }
        public ApplicationRole Role { get; set; }

        public string ApplicationUsersId { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }


    }
}
