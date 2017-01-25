using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Data;
using LiveToLift.Web.Infrastructure.Models;
using LiveToLift.Models;
using AutoMapper.QueryableExtensions;

namespace LiveToLift.Services
{
    public class ExerciseService : CommonService, IExerciseService
    {
        public ExerciseService(IUowData data) : base(data)
        {
        }

        public List<ExerciseInstanceViewModel> GetExerciseInstancesByExerciseId(int exerciseId)
        {

            List<ExerciseInstanceViewModel> exInstances = this.data.ExerciseInstances.All().Where(e => e.ExerciseId == exerciseId)
                                                          .Project().To<ExerciseInstanceViewModel>().ToList();

            return exInstances;



        }
    }
}
