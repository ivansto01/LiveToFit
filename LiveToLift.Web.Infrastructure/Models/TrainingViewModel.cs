using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class TrainingViewModel : BaseViewModel, IMapFrom<Training>
    {
        public ICollection<int> ExerciseIds { get; set; }

        public string CreatorId { get; set; }

        public int? Number { get; set; }

        public int? Duration { get; set; }

        private ICollection<ExerciseVeiwModel> exercises;

        public virtual ICollection<ExerciseVeiwModel> Exercises
        {
            get { return exercises; }
            set { exercises = value; }
        }
    }
}
