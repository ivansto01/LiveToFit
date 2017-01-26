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

        public int CreateNewComment(CommentViewModel model)
        {
            Comment dbModel = Mapper.Map<Comment>(model);

            data.Comments.Add(dbModel);
            data.SaveChanges();

            return dbModel.Id;
        }

        public List<CommentViewModel> GetCommentsByUserName(string userName, int skip = 0, int take = 10)
        {
            List<CommentViewModel> comments = this.data.Comments.All().Where(c => c.User.Name == userName).OrderBy(c => c.CreatedOn).Skip(skip).Take(take).Project().To<CommentViewModel>().ToList();

            return comments;
        }
    }
}
