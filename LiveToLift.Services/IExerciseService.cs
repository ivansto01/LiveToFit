﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Web.Infrastructure.Models;

namespace LiveToLift.Services
{
    public interface IExerciseService
    {
        List<ExerciseInstanceViewModel> GetExerciseInstancesByExerciseId(int exerciseId);
        List<ExerciseVeiwModel> GetExerciseByName(string name, int skip = 0, int take = 10);
    }
}
