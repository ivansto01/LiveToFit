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
    public class CommentController : ApiController
    {
        ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        [Authorize]
        public List<CommentViewModel> GetCommentsByUserName(string userName, int skip = 0, int take = 10)
        {

            List<CommentViewModel> comments = this.commentService.GetCommentsByUserName(userName, skip, take);

            return comments;
        }

        [Authorize]
        public HttpResponseMessage CreateNewComment(CommentViewModel model)
        {

            var userName = User.Identity.Name;
            model.UserName = userName;
           

            int id = commentService.CreateNewComment(model);

            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };

        }
    }
}
