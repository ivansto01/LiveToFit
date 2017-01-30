
using LiveToLift.Web.Infrastructure.Models;

namespace LiveToLift.Services
{
    public interface IUserService
    {
        UserDetailsViewModel ListUserTotalDetails(string username);
    }
}
