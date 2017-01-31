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
    public class TrainerController : ApiController
    {
        ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }

        [Authorize]
        public HttpResponseMessage AddUserToActiveTrainerUsers(AddActiveTrainerUsersViewModel viewModel)
        {
            var isTrainer = User.IsInRole("Trainer");
            var userId = User.Identity.GetUserId();
            int id = 0;
            if (isTrainer == true)
            {
                 id = this.trainerService.AddUserToActiveTrainerUsers(viewModel, userId);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };
        }

        [Authorize]
        public HttpResponseMessage ActivateUser(AddActiveTrainerUsersViewModel viewModel)
        {
            var isTrainer = User.IsInRole("Trainer");
            var userId = User.Identity.GetUserId();
            int id = 0;
            if (isTrainer == true)
            {
                id = this.trainerService.ActivateUser(viewModel, userId);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };
        }

        [Authorize]
        public HttpResponseMessage DeaktivateUser(AddActiveTrainerUsersViewModel viewModel)
        {
            var isTrainer = User.IsInRole("Trainer");
            var userId = User.Identity.GetUserId();
            int id = 0;
            if (isTrainer == true)
            {
                id = this.trainerService.DeaktivateUser(viewModel, userId);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };
        }
    }
}
