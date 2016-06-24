using ImplementRole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImplementRole.Controllers
{
    [Authorize(Roles = "accounting,admin")]
    public class CompanyController : BaseController
    {
        // GET: Company
        public ActionResult Index()
        {
            var role = userManager.GetRolesAsync(User.Identity.Name);
            var a = role;
            if (User.IsInRole(Security.Admin))
            {
                return Content("Welcome Admin");
            }
            if (User.IsInRole(Security.Accounting))
            {
                return Content("Welcome to accounting");
            }
            else
            {
                return Content("Welcome to other role");
            }
            
        }

        [AllowAnonymous]
        public ActionResult EmployeeList()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Content("Employee private List");
            }
            else
            {
                return Content("Employee public List");
            }
        }
    }
}