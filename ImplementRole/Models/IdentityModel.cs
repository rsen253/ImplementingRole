using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ImplementRole.Models
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext() :  base(){}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class CustomUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string CountryId { get; set; }
        public string StateId { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        DbSet<Country> CountryDetails { get; set; }
        DbSet<State> StateList { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CustomUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public static class Security
    {
        public const string Admin = "admin";
        public const string Accounting = "accounting";
        public const string It = "it";
        public const string Police = "police";
        public const string Teacher = "teacher";
        public const string Student = "student";
    }
}