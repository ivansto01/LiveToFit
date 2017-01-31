
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
    }
}
