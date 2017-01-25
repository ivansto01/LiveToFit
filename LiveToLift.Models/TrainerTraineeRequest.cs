using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
    public class TrainerTraineeRequest : BaseModel<int>
    {
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public bool IsRequestApproved { get; set; }

        public bool IsTrainerCreator { get; set; }
    }
}
