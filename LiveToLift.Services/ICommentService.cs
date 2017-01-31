using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Web.Infrastructure.Models;

namespace LiveToLift.Services
{
    public interface ICommentService
    {
        List<CommentViewModel> GetCommentsByUserName(string userName, int skip = 0, int take = 10);
        CommentViewModel CreateNewComment(CommentViewModel model);
    }
}
