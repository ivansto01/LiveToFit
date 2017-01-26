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
        //public ApplicationUserRole()
        //    : base()
        //{ }

        public int Id { get; set; }


    }
}
