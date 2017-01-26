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
    public class ProgressSheetController : ApiController
    {
        IProgressSheetService progressSheetService;

        public ProgressSheetController(IProgressSheetService progressSheetService)
        {
            this.progressSheetService = progressSheetService;
        }

        [HttpGet]
        [Authorize]
        public List<ProgressSheetViewModel> GetUserProgressSheet()
        {

            var userId = User.Identity.GetUserId();

            List<ProgressSheetViewModel> progressSheet = this.progressSheetService.GetUserProgressSheet(userId);
            return progressSheet;
        }

        [HttpGet]
        [Authorize]
        public List<ProgressSheetViewModel> GetUserProgressSheetById(string Id)
        {

            List<ProgressSheetViewModel> progressSheet = this.progressSheetService.GetUserProgressSheet(Id);
            return progressSheet;
        }


        [Authorize]
        public HttpResponseMessage CreateNewProgressSheet(ProgressSheetViewModel model)
        {
            var userId = User.Identity.GetUserId();


            if (model.UserId==null)
            {
                model.UserId = userId;
            }
            else if(model.UserId != userId)
            {
                bool hasAuthority = this.progressSheetService.HasAuthorityToCreateProgressSheet(userId, model.UserId);

                if ( !hasAuthority)
                {
                    throw new UnauthorizedAccessException("Can't create progress sheet to this person");
                }
            }

            int id = progressSheetService.CreateNewProgressSheet(model);

            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };

        }

        [Authorize]
        public HttpResponseMessage UpdateProgressSheet(ProgressSheetViewModel viewModel)
        {
            var isAdmin = User.IsInRole("admin");
            var userId = User.Identity.GetUserId();
            viewModel.UserId = userId;

            int id = this.progressSheetService.UpdateProgressSheet(viewModel, isAdmin, userId);
            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };
        }

        [Authorize]
        public HttpResponseMessage AddExerciseInstanceToProgressSheet(AddExInstanceToProgressSheetViewModel model)
        {
            var isAdmin = User.IsInRole("admin");
            var userId = User.Identity.GetUserId();
              

            this.progressSheetService.AddExerciseInstanceToProgressSheet(model, isAdmin, userId);
            return new HttpResponseMessage() { Content = new JsonContent(new { connected = true }) };
        }
    }
}
