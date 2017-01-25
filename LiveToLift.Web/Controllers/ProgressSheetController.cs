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
    }
}
