using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Data;
using LiveToLift.Web.Infrastructure.Models;
using LiveToLift.Models;
using AutoMapper.QueryableExtensions;

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

        public List<TrainerTraineeRequestViewModel> GetAllRequests(string userId, int skip = 0, int take = 0)
        {
            List<TrainerTraineeRequestViewModel> requests = this.data.TrainerTraineeRequests.All().Where(r => r.ReceiverId == userId)
                                                            .OrderBy(r => r.CreatedOn).Skip(skip).Take(take).Project().To<TrainerTraineeRequestViewModel>().ToList();

            return requests;
        }

        public List<TrainerTraineeRequestViewModel> GetNewRequest(string userId, int skip = 0, int take = 0)
        {
            List<TrainerTraineeRequestViewModel> requests = this.data.TrainerTraineeRequests.All().Where(r => r.ReceiverId == userId && r.IsNew == true)
                                                            .OrderBy(r=>r.CreatedOn).Skip(skip).Take(take)
                                                            .Project().To<TrainerTraineeRequestViewModel>().ToList();

            return requests;
        }

        public int UpdateViewRequest(TrainerTraineeRequestViewModel model, string userId)
        {
            var requestDb = this.data.TrainerTraineeRequests.All().FirstOrDefault(r => r.Id == model.Id);
            requestDb.Id = model.Id;
            requestDb.IsNew = model.IsNew;
            requestDb.ReceiverId = model.ReceiverId;
             
            if (userId == requestDb.ReceiverId)
            {
                requestDb.IsNew = false;
                this.data.TrainerTraineeRequests.Update(requestDb);
                this.data.SaveChanges();
            }
            return requestDb.Id;
        }

        public int AproveRequest(TrainerTraineeRequestViewModel viewModel, string userId)
        {
            var requestDb = this.data.TrainerTraineeRequests.All().FirstOrDefault(r => r.Id == viewModel.Id);
            requestDb.Id = viewModel.Id;
            requestDb.IsNew = viewModel.IsNew;
            requestDb.ReceiverId = viewModel.ReceiverId;
            requestDb.IsRequestApproved = viewModel.IsRequestApproved;
            requestDb.IsRejected = viewModel.IsRejected;

            if (userId == requestDb.ReceiverId)
            {
                requestDb.IsNew = false;
                requestDb.IsRequestApproved = true;
                requestDb.IsRejected = false;
                this.data.TrainerTraineeRequests.Update(requestDb);
                this.data.SaveChanges();
            }
            return requestDb.Id;
        }

        public int RejectRequest(TrainerTraineeRequestViewModel viewModel, string userId)
        {
            var requestDb = this.data.TrainerTraineeRequests.All().FirstOrDefault(r => r.Id == viewModel.Id);
            requestDb.Id = viewModel.Id;
            requestDb.IsNew = viewModel.IsNew;
            requestDb.ReceiverId = viewModel.ReceiverId;
            requestDb.IsRequestApproved = viewModel.IsRequestApproved;
            requestDb.IsRejected = viewModel.IsRejected;

            if (userId == requestDb.ReceiverId)
            {
                requestDb.IsNew = false;
                requestDb.IsRequestApproved = false;
                requestDb.IsRejected = true;
                this.data.TrainerTraineeRequests.Update(requestDb);
                this.data.SaveChanges();
            }
            return requestDb.Id;
        }
    }
}
