using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class DetailedFitnessProgramViewModel: BaseViewModel, IMapFrom<FitnessProgram>
    {
       

        public string Name { get; set; }

        public string AuthorName { get; set; }

        public string PhotoUrl { get; set; }

        public string VideoUrl { get; set; }

        private ICollection<TrainingViewModel> trainings;
        private ICollection<CommentViewModel> comments;

        public virtual ICollection<CommentViewModel> Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public virtual ICollection<TrainingViewModel> Trainings
        {
            get { return trainings; }
            set { trainings = value; }
        }
    }
}
