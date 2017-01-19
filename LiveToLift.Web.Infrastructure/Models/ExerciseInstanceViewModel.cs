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
    public class ExerciseInstanceViewModel : BaseViewModel, IMapFrom<ExerciseInstance>
    {
        public int ExerciseId { get; set; }
        public virtual ExerciseVeiwModel Exercise { get; set; }

        public decimal? Weight { get; set; }

        public int? Count { get; set; }

        public string Tempo { get; set; }
    }
}
