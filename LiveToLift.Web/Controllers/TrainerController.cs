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
            //var isAdmin = User.IsInRole("admin");
            var userId = User.Identity.GetUserId();

            int id = this.trainerService.AddUserToActiveTrainerUsers(viewModel, userId);

            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };
        }
    }
}
