﻿using Autofac;
using LiveToLift.Models;
using LiveToLift.Services;
using LiveToLift.Web.Infrastructure.Models;
using LiveToLift.Web.Infrastructure.Serialization;
using Microsoft.AspNet.Identity;
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

        public UserBasicInfoViewModel GetBasicUserInfo(string id)
        {
            UserBasicInfoViewModel profileInfo = this.userService.GetBasicUserInfo(id);

            return profileInfo;
        }

        [Authorize]
        public HttpResponseMessage AddRatingToUser(RatingViewModel viewModel, string id)
        {
            //var isAdmin = User.IsInRole("admin");
            //var userId = User.Identity.GetUserId();

            this.userService.AddRatingToUser(viewModel, id);

            return new HttpResponseMessage() { Content = new JsonContent(new { connected = true }) };
        }
    }
}
