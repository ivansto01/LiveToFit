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


        public void AddExerciseInstanceToProgressSheet(AddExInstanceToProgressSheetViewModel model, bool isAdmin, string userId)
        {
            ProgressSheet dbProgressSheet = this.data.ProgressSheets.All().FirstOrDefault(p => p.Id == model.ProgressSheetId);
            ExerciseInstance dbExInstance = this.data.ExerciseInstances.All().FirstOrDefault(e => e.Id == model.ExInstanceId);

            if (isAdmin == true || userId == dbProgressSheet.UserId)
            {

                dbProgressSheet.ExerciseInstances.Add(dbExInstance);
                this.data.ProgressSheets.Update(dbProgressSheet);
                data.SaveChanges();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
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

            //user.UserRoles.Any(s=>s.) &&   
            if ( activeTraining !=null)
            {
                result = true;
            }

            return result;
        }

        public int UpdateProgressSheet(ProgressSheetViewModel viewModel, bool isAdmin, string userId)
        {
            var dbProgressSheet = this.data.ProgressSheets.All().FirstOrDefault(p => p.Id == viewModel.Id);
            dbProgressSheet.Date = viewModel.Date;
            dbProgressSheet.PhotoUrl = viewModel.PhotoUrl;
            dbProgressSheet.VideoUrl = viewModel.VideoUrl;

            if (isAdmin == true || userId == dbProgressSheet.UserId)
            {
                this.data.ProgressSheets.Update(dbProgressSheet);
                this.data.SaveChanges();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            return dbProgressSheet.Id;
        }
    }
}
