
using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System.Collections.Generic;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class FitnessProgramViewModel : BaseViewModel,IMapFrom<FitnessProgram>
    {
        public string Name { get; set; }

        public string AuthorName { get; set; }

        public string PhotoUrl { get; set; }

        public string VideoUrl { get; set; }

        public string CreatorId { get; set; }

        public int OverallTrainingCount { get; set; }
    }
}