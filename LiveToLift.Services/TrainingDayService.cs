using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Data;
using LiveToLift.Web.Infrastructure.Models;
using AutoMapper.QueryableExtensions;
using LiveToLift.Models;

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

        public List<TrainingDayViewModel> GetTrainingdaysByProgramInstanceId(int programInstanceId)
        {
            var fitnessProgramInstance = this.data.FitnessProgramInstances.All().FirstOrDefault(f => f.Id == programInstanceId);

            List<TrainingDayViewModel> trainingDays = (fitnessProgramInstance.TrainingDays).AsQueryable().Project().To<TrainingDayViewModel>().ToList();

            return trainingDays;
        }



        public int UpdateTrainingDay(TrainingDayViewModel viewModel, bool isAdmin, string userId)
        {
            TrainingDay dbTrainDay = this.data.TrainingDays.All().FirstOrDefault(t => t.Id == viewModel.Id);

            //tozi red e za da si vzemem ApplicationUser - koito ni trqbva za authentikaciqta. No toi se namira vyv FitnessProgramInstances
            FitnessProgramInstance fpInstance = this.data.FitnessProgramInstances.All().FirstOrDefault(t => t.Id == dbTrainDay.FitnessProgramInstanceId);

            dbTrainDay.Date = viewModel.Date;
            dbTrainDay.Number = viewModel.Number;

            for (int i = 0; i < viewModel.ExerciseInstances.Count; i++)
            {
                var cuurentInstance = viewModel.ExerciseInstances.ElementAt(i);
                var trainingDay = dbTrainDay.ExerciseInstances.FirstOrDefault(s => s.Id == cuurentInstance.Id);
                if (trainingDay!=null)
                {
                    trainingDay.Weight = cuurentInstance.Weight;
                    trainingDay.Count =  cuurentInstance.Count;
                    trainingDay.Tempo =  cuurentInstance.Tempo;
                }

                
            }

            if (isAdmin == true || fpInstance.ApplicationUsersId == userId)
            {
                this.data.TrainingDays.Update(dbTrainDay);
                this.data.SaveChanges();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            return dbTrainDay.Id;
        }

       
    }
}
