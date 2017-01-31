using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class AddRatingToUser
    {
        public string UserId { get; set; }

        public int  RatingId { get; set; }
    }
}
