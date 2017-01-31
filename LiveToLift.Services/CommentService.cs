using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Data;
using LiveToLift.Web.Infrastructure.Models;
using AutoMapper.QueryableExtensions;
using LiveToLift.Models;
using AutoMapper;

namespace LiveToLift.Services
{
    public class CommentService : CommonService, ICommentService
    {
        public CommentService(IUowData data) : base(data)
        {
        }

        public CommentViewModel CreateNewComment(CommentViewModel model)
        {
            Comment dbModel = Mapper.Map<Comment>(model);

            data.Comments.Add(dbModel);
            data.SaveChanges();

            CommentViewModel newComment = Mapper.Map<CommentViewModel>(this.data.Comments.All(new string[] { "User" }).FirstOrDefault(s=>s.Id== dbModel.Id));

            return newComment;
        }

        public List<CommentViewModel> GetCommentsByUserName(string userName, int skip = 0, int take = 10)
        {
            List<CommentViewModel> comments = this.data.Comments.All().Where(c => c.User.Name == userName).OrderBy(c => c.CreatedOn).Skip(skip).Take(take).Project().To<CommentViewModel>().ToList();

            return comments;
        }
    }
}
