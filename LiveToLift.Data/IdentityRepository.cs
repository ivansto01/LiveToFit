using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveToLift.Models;
using System.Data.Entity;

namespace LiveToLift.Data
{
    public class IdentityRepository :IIdentityRepository
    {
        private ApplicationDbContext Context { get; }

        public IdentityRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = (ApplicationDbContext)context;
        }

        public ApplicationUser GetById(string id)
        {
            return this.Context.Users.FirstOrDefault(s => s.Id == id);
        }

        public ApplicationRole GetRoleById(string id)
        {
            return (ApplicationRole)this.Context.Roles.FirstOrDefault(s => s.Id == id);
        }

        public ApplicationUser GetByUsername(string name)
        {
            return this.Context.Users.FirstOrDefault(s => s.UserName == name);
        }

        public IQueryable<ApplicationUser> All()
        {
            return this.Context.Users;
        }
    }
}
