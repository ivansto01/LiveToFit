using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using LiveToLift.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class TrainingDayViewModel : BaseViewModel, IMapFrom<TrainingDay>
    {
        public DateTime? Date { get; set; }

        public virtual ICollection<ExerciseInstanceViewModel> ExerciseInstances { get; set; }

        public virtual ICollection<ExerciseVeiwModel> Exercises { get; set; }
        
    }
}
