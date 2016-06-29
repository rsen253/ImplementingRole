using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImplementRole.Models
{
    public class RoleViewModel
    {
        public string UserId { get; set; }
        [Display(Name = "Email Address")]
        public string UserEmail { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Assigned Role")]
        public string RoleName { get; set; }
    }
}