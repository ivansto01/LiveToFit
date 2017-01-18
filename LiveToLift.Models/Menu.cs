using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class Menu : BaseModel<int>
    {
        public string Name { get; set; }

        public ICollection<MenuNode> MenuNodes { get; set; }

        public Menu()
        {
            this.MenuNodes = new HashSet<MenuNode>();
        }
    }
}
