using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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