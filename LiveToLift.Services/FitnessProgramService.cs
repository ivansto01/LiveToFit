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

        // Pri Edit i Update moje da se naloji vs propertita da se mapvat na ryka :

        public int UpdateFitnessProgram(DetailedFitnessProgramViewModel viewModel, bool isAdmin, string userId)
        {
           
            // Mapvane na ryka
            var db = this.data.FitnessPrograms.All().FirstOrDefault(s => s.Id == viewModel.Id);
            db.Name = viewModel.Name;
            db.PhotoUrl = viewModel.PhotoUrl;
            db.AuthorName = viewModel.AuthorName;
            db.VideoUrl = viewModel.VideoUrl;

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
