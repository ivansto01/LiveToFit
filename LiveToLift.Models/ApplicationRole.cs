using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
            this.MenuNodes = new HashSet<MenuNode>();
        }

       

        public virtual string Description { get; set; }

        public ICollection<MenuNode> MenuNodes { get; set; }

        
    }
}
