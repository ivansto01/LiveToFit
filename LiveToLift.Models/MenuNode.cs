using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class MenuNode : BaseModel<int>
    {
        public string Text { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public ICollection<MenuNode> Children { get; set; }

        public MenuNode()
        {
            this.Children = new HashSet<MenuNode>();
            this.Menus = new HashSet<Menu>();
            this.ApplicationRoles = new HashSet<ApplicationRole>();
        }

        public ICollection<Menu> Menus { get; set; }

        public ICollection<ApplicationRole> ApplicationRoles { get; set; }


    }
}
