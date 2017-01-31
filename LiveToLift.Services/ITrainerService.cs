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
    }
}
