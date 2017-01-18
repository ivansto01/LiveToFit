using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class FitnessProgramInstance:BaseModel<int>
    {
        [Required]
        public string ApplicationUsersId { get; set; }
        public ApplicationUser ApplicationUsers { get; set; }


        private ICollection<TrainingDay> trainingDays;

        public int   FitnessProgramId { get; set; }
        public virtual FitnessProgram FitnessProgram { get; set; }

        public virtual ICollection<TrainingDay> TrainingDays
        {
            get { return trainingDays; }
            set { trainingDays = value; }
        }

        public FitnessProgramInstance()
        {
            this.trainingDays = new HashSet<TrainingDay>();
            
        }
    }
}
