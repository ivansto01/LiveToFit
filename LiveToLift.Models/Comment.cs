using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class Comment : BaseModel<int>
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [MaxLength(1000)]
        public string Content { get; set; }

        public int?  FitnessProgramId { get; set; }
        public virtual FitnessProgram FitnessProgram { get; set; }
    }
}
