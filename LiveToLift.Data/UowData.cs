using LiveToLift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Data
{
    public class UowData : IUowData
    {
        private readonly ApplicationDbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();


        public UowData()
           : this(new ApplicationDbContext())
        {
        }

        public UowData(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ApplicationDbContext Context
        {

                get { return this.context; }
        }

        public IDbRepository<ExerciseInstance> ExerciseInstances
        {
            get
            {
                return this.GetRepository<ExerciseInstance>();
            }
        }

        public IDbRepository<Exercise> Exercises
        {
            get
            {
                return this.GetRepository<Exercise>();
            }
        }

        public IDbRepository<FitnessProgram> FitnessPrograms
        {
            get
            {
                return this.GetRepository<FitnessProgram>();
            }
        }

        public IDbRepository<FitnessProgramInstance> FitnessProgramInstances
        {
            get
            {
                return this.GetRepository<FitnessProgramInstance>();
            }
        }

        public IDbRepository<ProgressSheet> ProgressSheets
        {
            get
            {
                return this.GetRepository<ProgressSheet>();
            }
        }

        public IDbRepository<TrainingDay> TrainingDays
        {
            get
            {
                return this.GetRepository<TrainingDay>();
            }
        }

        public IDbRepository<Training> Trainings
        {
            get
            {
                return this.GetRepository<Training>();
            }
        }

        public IDbRepository<Rating> Ratings
        {
            get
            {
                return this.GetRepository<Rating>();
            }
        }

        public IDbRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IDbRepository<Menu> Menus
        {
            get
            {
                return this.GetRepository<Menu>();
            }
        }

        public IDbRepository<MenuNode> MenuNodes
        {
            get
            {
                return this.GetRepository<MenuNode>();
            }
        }

        public IIdentityRepository Identity
        {
            get
            {
                return new IdentityRepository(this.context);
            }
        }

     

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IDbRepository<T> GetRepository<T>() where T : BaseModel<int>
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DbRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDbRepository<T>)this.repositories[typeof(T)];
        }
    }
}
