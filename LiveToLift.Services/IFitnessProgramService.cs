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
        int CreateNewProgram(FitnessProgramViewModel model);
        List<FitnessProgramViewModel> DisplayFitnessPrograms(int skip = 0, int take = 10);
        DetailedFitnessProgramViewModel FitnessProgramDetails(int id,bool isAuth);
        int UpdateFitnessProgram(DetailedFitnessProgramViewModel viewModel, bool isAdmin, string userId);
        void AddTrainingToFitnessProgram(AddTrainingToProgramViewModel model, bool isAdmin, string userId);

    }
}
