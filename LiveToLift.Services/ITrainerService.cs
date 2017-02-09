using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Web.Infrastructure.Models;

namespace LiveToLift.Services
{
    public interface ITrainerService
    {
        int AddUserToActiveTrainerUsers(AddActiveTrainerUsersViewModel viewModel, string userId);
        int ActivateUser(AddActiveTrainerUsersViewModel viewModel, string userId);
        int DeaktivateUser(AddActiveTrainerUsersViewModel viewModel, string userId);
        List<TrainerTraineeRequestViewModel> GetNewRequest(string userId, int skip = 0, int take = 0);
        List<TrainerTraineeRequestViewModel> GetAllRequests(string userId, int skip = 0, int take = 0);
        int UpdateViewRequest(TrainerTraineeRequestViewModel viewModel, string userId);
        int AproveRequest(TrainerTraineeRequestViewModel viewModel, string userId);
        int RejectRequest(TrainerTraineeRequestViewModel viewModel, string userId);
    }
}
