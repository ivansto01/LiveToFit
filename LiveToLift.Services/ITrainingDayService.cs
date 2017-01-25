using LiveToLift.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Services
{
    public interface ITrainingDayService
    {
        List<TrainingDayViewModel> GetCommentsByFitnessProgramId(int programId);
        List<TrainingDayViewModel> GetTrainingdaysByProgramInstanceId(int programInstanceId);
        int UpdateTrainingDay(TrainingDayViewModel viewModel, bool isAdmin, string userId);
    }
}
