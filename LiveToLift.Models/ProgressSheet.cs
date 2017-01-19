using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class ProgressSheet : BaseModel<int>
    {
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<ExerciseInstance> ExerciseInstances { get; set; }

        public ProgressSheet()
        {
            this.ExerciseInstances = new HashSet<ExerciseInstance>();
        }

        public string PhotoUrl { get; set; }

        public string VideoUrl { get; set; }
    }
}
