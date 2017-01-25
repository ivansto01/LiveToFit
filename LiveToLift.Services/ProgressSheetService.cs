using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Data;
using LiveToLift.Web.Infrastructure.Models;
using AutoMapper.QueryableExtensions;
using LiveToLift.Models;
using AutoMapper;

namespace LiveToLift.Services
{
    public class ProgressSheetService : CommonService, IProgressSheetService
    {
        public ProgressSheetService(IUowData data) : base(data)
        {
        }

        public int CreateNewProgressSheet(ProgressSheetViewModel model)
        {
            ProgressSheet dbModel = Mapper.Map<ProgressSheet>(model);


            data.ProgressSheets.Add(dbModel);

            data.SaveChanges();

            return dbModel.Id;
        }

        public List<ProgressSheetViewModel> GetUserProgressSheet(string userId)
        {
            List<ProgressSheetViewModel> progressSheets = this.data.ProgressSheets.All().Where(p => p.UserId == userId).
            Project().To<ProgressSheetViewModel>().ToList();

            return progressSheets;
        }

        public bool HasAuthorityToCreateProgressSheet(string userId, string adressUserId)
        {
            var user = this.data.Identity.GetById(userId);
            var result = false;
            
            var activeTraining = this.data.ActiveTrainingUsers.All().Where(s => s.TrainerId == userId).FirstOrDefault(s=>s.TraineeId == adressUserId);

            if (user.UserRoles.Any(s => s.Role.Name == "trainer") && activeTraining !=null)
            {
                result = true;
            }

            return result;
        }

    }
}
