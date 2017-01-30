using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Services
{
    public interface IFitnessProgramService
    {
        /// <summary>
        /// Create new fitness program
        /// </summary>
        /// <param name="model">Connstains reqiured props</param>
        /// <returns>The id of the newly creared Fitness program</returns>
        int CreateNewProgram(FitnessProgramViewModel model);
        List<FitnessProgramViewModel> DisplayFitnessPrograms(int skip = 0, int take = 10);
        DetailedFitnessProgramViewModel FitnessProgramDetails(int id,bool isAuth);
        int UpdateFitnessProgram(DetailedFitnessProgramViewModel viewModel, bool isAdmin, string userId);
        void AddTrainingToFitnessProgram(AddTrainingToProgramViewModel model, bool isAdmin, string userId);
        int AddFitnessProgramInstance(FitnessProgramInstanceViewModel model);
        List<CommentViewModel> GetCommentsByFitnessProgramId(int programId, int skip = 0, int take = 10);
    }
}
