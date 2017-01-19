using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Data;
using LiveToLift.Web.Infrastructure.Models;
using AutoMapper.QueryableExtensions;

namespace LiveToLift.Services
{
    public class TrainingDayService : CommonService, ITrainingDayService
    {
        public TrainingDayService(IUowData data) 
            : base(data)
        {
        }

        public List<TrainingDayViewModel> GetCommentsByFitnessProgramId(int programInstanceId)
        {
            var fitnessProgramInstance = this.data.FitnessProgramInstances .All().FirstOrDefault(f => f.Id == programInstanceId);
            

            List<TrainingDayViewModel> comments = (fitnessProgramInstance.TrainingDays).AsQueryable().Project().To<TrainingDayViewModel>().ToList();

            return comments;
        }
    }
}
