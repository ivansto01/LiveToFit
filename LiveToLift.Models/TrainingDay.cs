using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class TrainingDay : BaseModel<int>
    {
        public DateTime? Date { get; set; }


        public int? Number { get; set; }

        //private ICollection<Exercise> exercises;
        private ICollection<ExerciseInstance> exerciseInstances;

        public TrainingDay()
        {
            //this.exercises = new HashSet<Exercise>();
            this.exerciseInstances = new HashSet<ExerciseInstance>();
        }

        public virtual ICollection<ExerciseInstance> ExerciseInstances
        {
            get { return exerciseInstances; }
            set { exerciseInstances = value; }
        }


        public int FitnessProgramInstanceId { get; set; }
        public FitnessProgramInstance FitnessProgramInstance { get; set; }

        //public virtual ICollection<Exercise> Exercises
        //{
        //    get { return exercises; }
        //    set { exercises = value; }
        //}
    }
}
