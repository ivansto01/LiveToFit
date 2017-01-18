using LiveToLift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Data
{

    public interface IIdentityRepository
    {
        ApplicationUser GetById(string id);
        ApplicationRole GetRoleById(string id);
    }
}
