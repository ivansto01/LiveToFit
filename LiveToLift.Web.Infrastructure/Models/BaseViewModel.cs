using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class BaseViewModel
    {
       
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

     
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
