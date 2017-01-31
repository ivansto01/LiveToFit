using Autofac;
using LiveToLift.Models;
using LiveToLift.Services;
using LiveToLift.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiveToLift.Web.Controllers
{
    public class UserController : ApiController
    {
        IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public UserDetailsViewModel GetByName(string name)
        {
            UserDetailsViewModel details = this.userService.ListUserTotalDetails(name);

            return details;
        }

        public UserFullProfileViewModel GetProfileUserInfo(string name)
        {
            UserFullProfileViewModel profileInfo = this.userService.GetProfileUserInfo(name);

            return profileInfo;  
        }
    }
}
