using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class ExerciseInstance : BaseModel<int>
    {
        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public decimal? Weight { get; set; }

        public int? Count { get; set; }

        public string Tempo { get; set; }

        public ProgressSheet ProgresSheets { get; set; }
    }
}
