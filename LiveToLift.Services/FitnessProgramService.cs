using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Web.Infrastructure.Models;
using LiveToLift.Models;
using AutoMapper;
using LiveToLift.Data;
using System.Net.Http;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;

namespace LiveToLift.Services
{
    public class FitnessProgramService : CommonService, IFitnessProgramService
    {

        public FitnessProgramService(IUowData data) 
            : base(data)    
        {
        }

        public int AddFitnessProgramInstance(FitnessProgramInstanceViewModel model)
        {
            var fitnessProgram = this.data.FitnessPrograms.All().FirstOrDefault(p => p.Id ==  model.FitnessProgramId);

            var user = this.data.Identity.GetById(model.ApplicationUsersId);

            FitnessProgramInstance dbModel = Mapper.Map<FitnessProgramInstance>(model);

            if (fitnessProgram.Trainings.Count == 0)
            {
                throw new ArgumentException("Only pragrams which have trainings can be added to user.");
            }


            for (int i =0; i < fitnessProgram.OverallTrainingCount; i++)
            {
                TrainingDay newTrainingDay = new TrainingDay();

                //get 0,1,2 0,1,2...
                var training = fitnessProgram.Trainings.ElementAt(i % fitnessProgram.Trainings.Count);

                newTrainingDay.Number = training.Number;

                for (int j = 0; j < training.Exercises.Count; j++)
                {
                    var excercise = training.Exercises.ElementAt(j);

                    newTrainingDay.ExerciseInstances.Add(
                        new ExerciseInstance()
                    {
                        ExerciseId = excercise.Id
                    });
                }

                dbModel.TrainingDays.Add(newTrainingDay);
            }
            

            data.FitnessProgramInstances.Add(dbModel);

            user.FitnessProgramInstances.Add(dbModel);
            data.SaveChanges();
            

            return dbModel.Id;
        }

        

        public void AddTrainingToFitnessProgram(AddTrainingToProgramViewModel model, bool isAdmin, string userId)
        {
            FitnessProgram fitnessProgram = this.data.FitnessPrograms.All().FirstOrDefault(f => f.Id == model.FitnessProgramId);
            Training training = this.data.Trainings.All().FirstOrDefault(t => t.Id == model.TrainingId);

            if (fitnessProgram.Trainings.Any(n => n.Number == training.Number))
            {
                throw new ArgumentException("There shouldn’t be trainings with duplicate numbers");
            }

            if (isAdmin == true || userId == fitnessProgram.CreatorId)
            {

                fitnessProgram.Trainings.Add(training);
                this.data.FitnessPrograms.Update(fitnessProgram);
                data.SaveChanges();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }



        public int CreateNewProgram(FitnessProgramViewModel model)
        {

            FitnessProgram dbModel = Mapper.Map<FitnessProgram>(model);

            data.FitnessPrograms.Add(dbModel);
            data.SaveChanges();

            return dbModel.Id;
                
        }

        public List<FitnessProgramViewModel> DisplayFitnessPrograms(int skip = 0, int take = 10)
        {
            List<FitnessProgramViewModel> fitnessPrograms = this.data.FitnessPrograms.All().OrderByDescending(f=>f.CreatedOn).Skip(skip).Take(take).Project()
                                         .To<FitnessProgramViewModel>()
                                         .ToList();


            return fitnessPrograms;
        }

        public DetailedFitnessProgramViewModel FitnessProgramDetails(int id, bool isAuth)
        {
            var fitnesProgram = this.data.FitnessPrograms.All().FirstOrDefault(s => s.Id == id);
            DetailedFitnessProgramViewModel model = Mapper.Map<DetailedFitnessProgramViewModel>(fitnesProgram);

            if (!isAuth)
            {
                model.Comments = new List<CommentViewModel>();
                model.Trainings = new List<TrainingViewModel>();
            }    

            return model;
        }

        public List<CommentViewModel> GetCommentsByFitnessProgramId(int programId, int skip = 0, int take = 10)
        {
            var fitnessProgram = this.data.FitnessPrograms.All().FirstOrDefault(f => f.Id == programId);


            var comments = (fitnessProgram.Comments.OrderBy(c => c.CreatedOn).Skip(skip).Take(take)).AsQueryable().Project().To<CommentViewModel>().ToList();

            return comments;
        }

        

        // Pri Edit i Update moje da se naloji vs propertita da se mapvat na ryka :

        public int UpdateFitnessProgram(DetailedFitnessProgramViewModel viewModel, bool isAdmin, string userId)
        {
           
            // Mapvane na ryka
            var db = this.data.FitnessPrograms.All().FirstOrDefault(s => s.Id == viewModel.Id);
            db.Name = viewModel.Name;
            db.PhotoUrl = viewModel.PhotoUrl;
            db.AuthorName = viewModel.AuthorName;
            db.VideoUrl = viewModel.VideoUrl;
            db.OverallTrainingCount = viewModel.OverallTrainingCount;

            //FitnessProgram fitnesProgra = Mapper.Map<FitnessProgram>(viewModel);   // Avtomati4no mapvane s Imap-era


            if (isAdmin == true || userId == db.CreatorId)
            {
                this.data.FitnessPrograms.Update(db);
                this.data.SaveChanges();
            }
            else
            {
                throw new UnauthorizedAccessException();
            }


            return db.Id;
        }
    }
}
