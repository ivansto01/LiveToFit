using LiveToLift.Services;
using LiveToLift.Web.Infrastructure.Models;
using LiveToLift.Web.Infrastructure.Serialization;
using Microsoft.AspNet.Identity;
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

        public List<TrainingDayViewModel> GetTrainingdaysByProgramInstanceId(int programInstanceId)
        {
            List<TrainingDayViewModel> trainingDays = this.trainingDayService.GetTrainingdaysByProgramInstanceId(programInstanceId);
            return trainingDays;
        }

        
        [Authorize]
        public HttpResponseMessage UpdateTrainingDay(TrainingDayViewModel viewModel)
        {
            var isAdmin = User.IsInRole("admin");
            var userId = User.Identity.GetUserId();

            int id = this.trainingDayService.UpdateTrainingDay(viewModel, isAdmin, userId);
            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };
        }
    }
}
