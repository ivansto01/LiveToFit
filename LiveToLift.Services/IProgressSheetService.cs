using LiveToLift.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Services
{
    public interface IProgressSheetService
    {
        List<ProgressSheetViewModel> GetUserProgressSheet(string userId);
        int CreateNewProgressSheet(ProgressSheetViewModel model);
        bool HasAuthorityToCreateProgressSheet(string userId, string adressUserId);
    }
}
