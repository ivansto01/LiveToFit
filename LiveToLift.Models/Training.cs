using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class Training : BaseModel<int>
    {
        public int? Number { get; set; }

        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        private ICollection<Exercise> exercises;

        public Training()
        {
            this.exercises = new HashSet<Exercise>();
        }


        public virtual ICollection<Exercise> Exercises
        {
            get { return exercises; }
            set { exercises = value; }
        }

        public int? Duration { get; set; }
    }
}
