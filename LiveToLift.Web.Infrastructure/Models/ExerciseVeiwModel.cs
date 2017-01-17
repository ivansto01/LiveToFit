using LiveToLift.Models;
using LiveToLift.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Web.Infrastructure.Models
{
    public class ExerciseVeiwModel: BaseViewModel, IMapFrom<Exercise>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public string VideoUrl { get; set; }
    }
}
