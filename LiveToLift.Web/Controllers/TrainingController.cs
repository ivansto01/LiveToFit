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
    public class TrainingController : ApiController
    {
        ITrainingService trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            this.trainingService = trainingService;
        }

        [HttpGet]
        public List<TrainingViewModel> GetAllTrainings(int skip = 0, int take = 10)
        {
            List<TrainingViewModel> trainings = this.trainingService.DisplayFitnessPrograms(skip, take);

            return trainings;
        }


        [HttpGet]
        public TrainingViewModel GetTrainingById(int id)
        {
            TrainingViewModel train = this.trainingService.TrainingById(id);

            return train;
        }

        [Authorize]
        public HttpResponseMessage AddNewTraining(TrainingViewModel model)
        {
            var userId = User.Identity.GetUserId();
            model.CreatorId = userId;

            int id = trainingService.CreateNewTraining(model);

            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };

        }

        //[Authorize]
        //public HttpResponseMessage Update(TrainingViewModel viewModel)
        //{
        //    var isAdmin = User.IsInRole("admin");
        //    var userId = User.Identity.GetUserId();

        //    int id = this.trainingService.UpdateTraining(viewModel, isAdmin, userId);
        //    return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };
        //}

        
    }
}
