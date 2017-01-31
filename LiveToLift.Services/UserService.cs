using System;
using System.Collections.Generic;
using AutoMapper;
using LiveToLift.Data;
using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Models;

namespace LiveToLift.Services
{
    public class UserService : CommonService, IUserService
    {
        public UserService(IUowData data) : base(data)
        {
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
