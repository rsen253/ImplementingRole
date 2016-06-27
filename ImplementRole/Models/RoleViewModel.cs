using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImplementRole.Models
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Assigned Role")]
        public string RoleName { get; set; }
    }
}