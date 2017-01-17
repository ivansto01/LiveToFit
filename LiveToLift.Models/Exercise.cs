using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class Exercise : BaseModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public string VideoUrl { get; set; }


        public ICollection<Training> Trainings { get; set; }

        public Exercise()
        {
            this.Trainings = new HashSet<Training>();
        }
    }
}
