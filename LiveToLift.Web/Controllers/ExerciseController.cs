using LiveToLift.Services;
using LiveToLift.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiveToLift.Web.Controllers
{
    public class ExerciseController : ApiController
    {
        IExerciseService exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }

        [HttpGet]
        [Authorize]
        public List<ExerciseInstanceViewModel> GetExerciseInstancesByExerciseId(int exerciseId)
        {

            List<ExerciseInstanceViewModel> exInstances = this.exerciseService.GetExerciseInstancesByExerciseId(exerciseId);

            return exInstances;
        }

        [HttpGet]
        [Authorize]
        public List<ExerciseVeiwModel> GetExerciseByName(string name)
        {

            List<ExerciseVeiwModel> exercise = this.exerciseService.GetExerciseByName(name);

            return exercise;
        }
    }
}
