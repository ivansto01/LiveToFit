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
        public ApplicationUser User { get; set; }

        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
