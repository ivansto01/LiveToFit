﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Models
{
   public class ActiveTrainingUsers:BaseModel<int>
    {
        
        public string TrainerId { get; set; }
        public ApplicationUser Trainer { get; set; }

        
        public string TraineeId { get; set; }
        public ApplicationUser Trainee { get; set; }
    }
}
