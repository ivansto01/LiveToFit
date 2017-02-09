using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class TrainerTraineeRequestViewModel : BaseViewModel, IMapFrom<TrainerTraineeRequest>
    {
        public string CreatorId { get; set; }
        

        public string ReceiverId { get; set; }
       
 
        public bool IsRequestApproved { get; set; }

        public bool IsTrainerCreator { get; set; }

        public bool IsNew { get; set; }

        public bool IsRejected { get; set; }
    }
}
