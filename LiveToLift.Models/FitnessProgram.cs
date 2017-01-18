using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class FitnessProgram : BaseModel<int>
    {
        

        public string Name { get; set; }

        public string AuthorName { get; set; }

        public string PhotoUrl { get; set; }

        public string VideoUrl { get; set; }

        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        private ICollection<Training> trainings;
        private ICollection<Comment> comments;
       
        
        public FitnessProgram()
        {
            this.trainings = new HashSet<Training>();

            this.comments = new HashSet<Comment>();

        }

        public virtual ICollection<Comment> Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public virtual ICollection<Training> Trainings
        {
            get { return trainings; }
            set { trainings = value; }
        }

        public int OverallTrainingCount { get; set; }


    }
}
