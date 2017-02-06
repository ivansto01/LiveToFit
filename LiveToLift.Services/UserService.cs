using System;
using System.Collections.Generic;
using AutoMapper;
using LiveToLift.Data;
using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Models;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace LiveToLift.Services
{
    public class UserService : CommonService, IUserService
    {
        public UserService(IUowData data) : base(data)
        {
        }

        public void AddRatingToUser(RatingViewModel viewModel, string id)
        {
            ApplicationUser user = this.data.Identity.GetById(id);
            Rating ratings = this.data.Ratings.All().FirstOrDefault(r => r.Id == viewModel.Id);
            if (ratings.Value >= 1 && ratings.Value <=5)
            {
                user.Ratings.Add(ratings);
                this.data.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Rating can be only between [1..5] range");
            }
        }

        public UserBasicInfoViewModel GetBasicUserInfo(string id)
        {
            ApplicationUser profile = this.data.Identity.GetById(id);

            UserBasicInfoViewModel model = Mapper.Map<UserBasicInfoViewModel>(profile);

            return model;

        }


        



        public UserFullProfileViewModel GetProfileUserInfo(string name)
        {

            ApplicationUser userProfile = this.data.Identity.GetByUsername(name);
           

            UserFullProfileViewModel model = Mapper.Map<UserFullProfileViewModel>(userProfile);
            return model;
        }


        public List<UserInstancesViewModel> GetUserIntances(string userId, int skip = 0, int take = 10)
        {
            List<UserInstancesViewModel> userInstances = this.data.FitnessProgramInstances.All().OrderBy(u => u.CreatedOn).Skip(skip).Take(take)
                .Where(u => u.ApplicationUsersId == userId)
                .Project().To<UserInstancesViewModel>().ToList();

            return userInstances;
        }


        public List<TrainingDayViewModel> GetUserTrainingDays(string userId, int skip = 0, int take = 10)
        {
            List<TrainingDayViewModel> userTrainingDays = this.data.TrainingDays.All().OrderBy(t => t.CreatedOn).Skip(skip).Take(take)
                .Where(t => t.FitnessProgramInstance.ApplicationUsersId == userId).Project().To<TrainingDayViewModel>().ToList();

            return userTrainingDays;
        }



        public UserDetailsViewModel ListUserTotalDetails(string username)
        {
            ApplicationUser dbUser = this.data.Identity.GetById(username);
            

            UserDetailsViewModel model = Mapper.Map<UserDetailsViewModel>(dbUser);

            return model;
        }



        public List<UserFullProfileViewModel> GetListUsers(string name = "", int skip = 0, int take = 10)
        {
            List<UserFullProfileViewModel> users = this.data.Identity.All().Where(e => e.Name.Contains(name)).OrderBy(e => e.Name).Skip(skip).Take(take)
                                                  .Project().To<UserFullProfileViewModel>().ToList();

            return users;

        }
    }
}
