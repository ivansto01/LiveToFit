
using System.Collections.Generic;
using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Models;

namespace LiveToLift.Services
{
    public interface IUserService
    {
        UserDetailsViewModel ListUserTotalDetails(string username);
        UserFullProfileViewModel GetProfileUserInfo(string name);
        UserBasicInfoViewModel GetBasicUserInfo(string id);
        void AddRatingToUser(RatingViewModel viewModel, string id);
        List<UserInstancesViewModel> GetUserIntances(string userId, int skip = 0, int take = 10);
        List<TrainingDayViewModel> GetUserTrainingDays(string userId, int skip = 0, int take = 10);
        List<UserFullProfileViewModel> GetListUsers(string currentUser,string name = "", int skip = 0, int take = 10);
    }
}
