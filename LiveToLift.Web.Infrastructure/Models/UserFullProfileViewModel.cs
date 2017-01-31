using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class UserFullProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }


        

        public UserFullProfileViewModel()
        {
            this.FitnessProgramInstances = new HashSet<FitnessProgramInstanceViewModel>();
            this.ProgressSheets = new HashSet<ProgressSheetViewModel>();
            this.Ratings = new HashSet<RatingViewModel>();

            
        }

        public virtual ICollection<RatingViewModel> Ratings { get; set; }
      

        public virtual ICollection<FitnessProgramInstanceViewModel> FitnessProgramInstances { get; set; }


        public virtual ICollection<ProgressSheetViewModel> ProgressSheets { get; set; }
    }
}
