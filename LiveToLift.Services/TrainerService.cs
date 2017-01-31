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

        public int ActivateUser(AddActiveTrainerUsersViewModel viewModel, string userId)
        {
            ActiveTrainingUsers activeuser = new ActiveTrainingUsers();
            activeuser.TrainerId = userId;
            activeuser.TraineeId = viewModel.TraineeId;

            InactiveTrainingUsers inactiveUser = this.data.InactiveTrainingUsers.All().FirstOrDefault(i => i.TrainerId == userId && i.TraineeId == viewModel.TraineeId);

            if (inactiveUser != null && !data.ActiveTrainingUsers.All().Any(n => n.TrainerId == userId && n.TraineeId == viewModel.TraineeId))
            {
                this.data.InactiveTrainingUsers.Delete(inactiveUser);
                this.data.ActiveTrainingUsers.Add(activeuser);
                this.data.SaveChanges();
            }
            return activeuser.Id;
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

        public int DeaktivateUser(AddActiveTrainerUsersViewModel viewModel, string userId)
        {
            InactiveTrainingUsers newinactiveUser = new InactiveTrainingUsers();
            newinactiveUser.TrainerId = userId;
            newinactiveUser.TraineeId = viewModel.TraineeId;

            InactiveTrainingUsers inactiveUser = this.data.InactiveTrainingUsers.All().FirstOrDefault(i => i.TrainerId == userId && i.TraineeId == viewModel.TraineeId);
            if (inactiveUser!=null)
            {
                throw new ArgumentException("User is allready deactivated");

            }
            

            ActiveTrainingUsers activeUser = this.data.ActiveTrainingUsers.All().FirstOrDefault(i => i.TrainerId == userId && i.TraineeId == viewModel.TraineeId);

            if (activeUser != null && !data.InactiveTrainingUsers.All().Any(n => n.TrainerId == userId && n.TraineeId == viewModel.TraineeId))
            {
                this.data.ActiveTrainingUsers.Delete(activeUser);
                this.data.InactiveTrainingUsers.Add(newinactiveUser);
                this.data.SaveChanges();
            }

            return inactiveUser.Id;
        }
    }
}
