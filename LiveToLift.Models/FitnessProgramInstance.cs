using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class FitnessProgramInstance:BaseModel<int>
    {
        private ICollection<ApplicationUser> applicationUsers;

        public virtual ICollection<ApplicationUser> ApplicationUsers
        {
            get { return applicationUsers; }
            set { applicationUsers = value; }
        }


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
            this.applicationUsers = new HashSet<ApplicationUser>();
        }
    }
}
