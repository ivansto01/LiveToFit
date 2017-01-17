using LiveToLift.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Services
{
    public interface ITrainingService
    {
        List<TrainingViewModel> DisplayFitnessPrograms(int skip = 0, int take = 10);
        TrainingViewModel TrainingById(int id);
        int CreateNewTraining(TrainingViewModel model);
        int UpdateTraining(TrainingViewModel viewModel, bool isAdmin, string userId);
    }
}
