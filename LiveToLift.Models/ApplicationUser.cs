using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace LiveToLift.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }


        private ICollection<FitnessProgramInstance> fitnessProgramsInstances;
        private ICollection<ProgressSheet> progressSheets;
        private ICollection<Rating> ratings;

        public ApplicationUser()
        {
            this.fitnessProgramsInstances = new HashSet<FitnessProgramInstance>();
            this.progressSheets = new HashSet<ProgressSheet>();
            this.ratings = new HashSet<Rating>();
        }

        public virtual ICollection<Rating> Ratings
        {
            get { return ratings; }
            set { ratings = value; }
        }

        public virtual ICollection<FitnessProgramInstance> FitnessProgramInstances
        {
            get { return fitnessProgramsInstances; }
            set { fitnessProgramsInstances = value; }
        }

        [Required]
        public virtual ICollection<ProgressSheet> ProgressSheets
        {
            get { return progressSheets; }
            set { progressSheets = value; }
        }

    }
}
