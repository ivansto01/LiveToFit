﻿using LiveToLift.Services;
using LiveToLift.Web.Infrastructure.Models;
using LiveToLift.Web.Infrastructure.Serialization;
using LiveToLift.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiveToLift.Web.Controllers
{
    public class FitnessProgramsController : ApiController
    {

        IFitnessProgramService fitnessProgramService;

        public FitnessProgramsController(IFitnessProgramService fitnessProgramService)
        {
            this.fitnessProgramService = fitnessProgramService;
        }

        [Authorize]
        public HttpResponseMessage CreateNewProgram(FitnessProgramViewModel model)
        {
           
            var userId = User.Identity.GetUserId();
            model.CreatorId = userId;

            int id = fitnessProgramService.CreateNewProgram(model);

            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };
           
        }

        [HttpGet]
        public List<FitnessProgramViewModel> ShowFitnessPrograms(int skip = 0, int take = 10)
        {

            List<FitnessProgramViewModel> fitnessPrograms = this.fitnessProgramService.DisplayFitnessPrograms(skip,take);

            return fitnessPrograms;
        }

       
        [HttpGet]
        public DetailedFitnessProgramViewModel GetFitnessProgramById(int id)
        {
            var isAuthenticated = User.Identity.IsAuthenticated;

            DetailedFitnessProgramViewModel fitnessProgam = this.fitnessProgramService.FitnessProgramDetails(id, isAuthenticated);

            return fitnessProgam;
        }

        [Authorize]
        public HttpResponseMessage UpdateFitnessProgram(DetailedFitnessProgramViewModel viewModel)
        {
            var isAdmin = User.IsInRole("admin");
            var userId = User.Identity.GetUserId();

            int id = this.fitnessProgramService.UpdateFitnessProgram( viewModel,  isAdmin,  userId);
            return new HttpResponseMessage() { Content = new JsonContent(new { id = id }) };
        }

        [Authorize]
        public HttpResponseMessage AddTrainingToFitnessProgram(AddTrainingToProgramViewModel viewModel)
        {
            var isAdmin = User.IsInRole("admin");
            var userId = User.Identity.GetUserId();

            this.fitnessProgramService.AddTrainingToFitnessProgram(viewModel, isAdmin, userId);

            return new HttpResponseMessage() { Content = new JsonContent(new { connected = true }) };
        }
    }
}
