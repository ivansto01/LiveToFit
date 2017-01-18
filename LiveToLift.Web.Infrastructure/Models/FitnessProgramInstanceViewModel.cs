using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class FitnessProgramInstanceViewModel : BaseViewModel, IMapFrom<FitnessProgramInstance>
    {
        public string ApplicationUsersId { get; set; }
        

        public int FitnessProgramId { get; set; }
        public virtual FitnessProgramViewModel FitnessProgram { get; set; }

        public virtual ICollection<TrainingDayViewModel> TrainingDays { get; set; }
    }
}
