using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class AddTrainingToProgramViewModel
    {
        public int TrainingId { get; set; }

        public int FitnessProgramId { get; set; }
    }
}
