using LiveToLift.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveToLift.Services
{
    public class CommonService : ICommonService
    {
        public IUowData data;

        public CommonService(IUowData data)
        {
            this.data = data;
        }
    }
}
