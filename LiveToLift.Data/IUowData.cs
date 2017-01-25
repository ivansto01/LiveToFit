using LiveToLift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Data
{
    public interface IUowData
    {
        ApplicationDbContext Context { get; }

        IDbRepository<Exercise> Exercises { get; }

        IDbRepository<ExerciseInstance> ExerciseInstances { get; }

        IDbRepository<FitnessProgram> FitnessPrograms { get; }

        IDbRepository<FitnessProgramInstance> FitnessProgramInstances { get; }

        IDbRepository<ProgressSheet> ProgressSheets { get; }

        IDbRepository<Training> Trainings { get; }

        IDbRepository<TrainingDay> TrainingDays { get; }

        IDbRepository<Comment> Comments { get; }

        IDbRepository<Rating> Ratings { get; }

        IDbRepository<Menu> Menus { get; }

        IDbRepository<MenuNode> MenuNodes { get; }

        IDbRepository<TrainerTraineeRequest> TrainerTraineeRequests { get; }

        IIdentityRepository Identity { get; }
        IDbRepository<ActiveTrainingUsers> ActiveTrainingUsers { get; }
        IDbRepository<InactiveTrainingUsers> InactiveTrainingUsers { get; }




        int SaveChanges();
    }
}
