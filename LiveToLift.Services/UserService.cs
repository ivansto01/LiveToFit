using System;
using System.Collections.Generic;
using AutoMapper;
using LiveToLift.Data;
using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Models;
using System.Linq;


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
                throw new ArgumentException("Rating can be only between range [1..5]");
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

        public UserDetailsViewModel ListUserTotalDetails(string username)
        {
            ApplicationUser dbUser = this.data.Identity.GetById(username);
            

            UserDetailsViewModel model = Mapper.Map<UserDetailsViewModel>(dbUser);

            return model;
        }
    }
}
