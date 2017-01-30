using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Data;
using LiveToLift.Web.Infrastructure.Models;
using LiveToLift.Models;

namespace LiveToLift.Services
{
    public class TrainerService : CommonService, ITrainerService
    {
        public TrainerService(IUowData data) : base(data)
        {
        }

        public int AddUserToActiveTrainerUsers(AddActiveTrainerUsersViewModel viewModel, string userId)
        {
            if (data.ActiveTrainingUsers.All().Any(n => n.TrainerId == userId && n.TraineeId == viewModel.TraineeId))
            {
                throw new ArgumentException("There shouldn’t be duplicates");
            }
            ActiveTrainingUsers activeUsers = new ActiveTrainingUsers();
            activeUsers.TraineeId = viewModel.TraineeId;
            activeUsers.TrainerId = userId;

            TrainerTraineeRequest trainerRequest = new TrainerTraineeRequest();
            trainerRequest.CreatorId = userId;
            trainerRequest.ReceiverId = viewModel.TraineeId;
            trainerRequest.IsTrainerCreator = true;
            trainerRequest.IsRequestApproved = false;


            this.data.ActiveTrainingUsers.Add(activeUsers);
            this.data.TrainerTraineeRequests.Add(trainerRequest);
            this.data.SaveChanges();

            return activeUsers.Id;
        }
    }
}
