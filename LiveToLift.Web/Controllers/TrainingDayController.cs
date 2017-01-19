using LiveToLift.Services;
using LiveToLift.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiveToLift.Web.Controllers
{
    public class TrainingDayController : ApiController
    {
        ITrainingDayService trainingDayService;

        public TrainingDayController(ITrainingDayService trainingDayService)
        {
            this.trainingDayService = trainingDayService;
        }

        [HttpGet]
        [Authorize]
        public List<TrainingDayViewModel> GetCommentsByFitnessProgramId(int programInstanceId)
        {
            List<TrainingDayViewModel> trainingDays = this.trainingDayService.GetCommentsByFitnessProgramId(programInstanceId);

            return trainingDays;
        }
    }
}
