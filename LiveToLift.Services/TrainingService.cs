using LiveToLift.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Web.Infrastructure.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using LiveToLift.Models;

namespace LiveToLift.Services
{
    public class TrainingService : CommonService, ITrainingService
    {
        public TrainingService(IUowData data) 
            : base(data)    
        {
        }

        

        public int CreateNewTraining(TrainingViewModel model)
        {
            Training dbModel = Mapper.Map<Training>(model);

            
            AddExercisesToTraining(model, dbModel);


            data.Trainings.Add(dbModel);
 
            data.SaveChanges();

            return dbModel.Id;
        }

        public List<TrainingViewModel> DisplayFitnessPrograms(int skip = 0, int take = 10)
        {
            List<TrainingViewModel> trainings = this.data.Trainings.All().OrderByDescending(t => t.CreatedOn).Skip(skip).Take(take).
                                                Project().To<TrainingViewModel>().ToList();

            return trainings;
        }

        public TrainingViewModel TrainingById(int id)
        {
            var training = this.data.Trainings.All().FirstOrDefault(t => t.Id == id);
            TrainingViewModel model = Mapper.Map<TrainingViewModel>(training);

            return model;
        }

        public int UpdateTraining(TrainingViewModel viewModel, bool isAdmin, string userId)
        {

           

            var db = this.data.Trainings.All().FirstOrDefault(t => t.Id == viewModel.Id);
            db.Number = viewModel.Number;
            db.Duration = viewModel.Duration;

            //foreach (var item in db.Exercises)
            //{
            //    db.Exercises.Remove(item);
            //}

            
            while (db.Exercises.Count != 0)
            {

                db.Exercises.Remove(db.Exercises.ElementAt(0));
            }

            AddExercisesToTraining(viewModel, db);

            if (isAdmin == true || userId == db.CreatorId)
            {
                this.data.Trainings.Update(db);
                this.data.SaveChanges();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }


            return db.Id;

        }





        private void AddExercisesToTraining(TrainingViewModel viewModel, Training db)
        {

            // var result = this.data.Exercises.All().Where(db => model.ExerciseIds.Any(id => id == db.Id)).ToList();

            // Equals to up
            List<Exercise> secondResult = (from i in viewModel.ExerciseIds
                                           join g in data.Exercises.All() on i equals g.Id
                                           select g).ToList();

            foreach (var item in secondResult)
            {
                db.Exercises.Add(item);
            }
        }
    }
}
