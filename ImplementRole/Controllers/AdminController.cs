using ImplementRole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ImplementRole.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
       
        public ActionResult AddUser()
        {
            return RedirectToAction("Register", "Account");
        }
        //public ActionResult AddRoleToUser()
        //{
        //    //var applicationContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
        //    var result = from a in db.Users
        //                     from ur in a.Roles select ur;
        //    var userList = (from u in db.Users
        //            from ur in u.Roles
        //            join r in db.Roles on ur.RoleId equals r.Id
        //            select new
        //            {
        //                u.Id,
        //                Name = u.UserName,
        //                Role = r.Name,
        //            }).ToList();
        //    return View(result);
        //}

        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRole(RoleViewModel roleViewModel)
        {
            var roles = ApplicationRoleManeger.Create(HttpContext.GetOwinContext());
            if (!await roles.RoleExistsAsync(roleViewModel.RoleName))
            {
                await roles.CreateAsync(new IdentityRole { Name = roleViewModel.RoleName });
                //ViewBag.result = "Role Added Successfully";
            }
            //else
            //{
            //    ViewBag.result = "Role is already in the database";
            //}
            return View();
        }

        public ActionResult ManageUser(string SortingOrder)
        {
            var result = (from u in db.Users
                          join c in db.CountryDetails on u.CountryId equals c.CountryId
                          join s in db.StateList on u.StateId equals s.StateId
                          select u).OrderBy(u => u.FirstName);
            ViewBag.SortingFirstName = String.IsNullOrEmpty(SortingOrder) ? "FirstName" : "";
            ViewBag.SortingLastName = String.IsNullOrEmpty(SortingOrder) ? "LastName" : "";
            switch (SortingOrder)
            {
                case "FirstName":
                    result = result.OrderByDescending(u => u.FirstName);
                    break;
                case "LastName" :
                    result = result.OrderByDescending(u => u.LastName);
                    break;
            }
            
            return View(result);
        }

        public ActionResult ManageRole()
        {
            var roleList = db.Roles.ToList();
            return View(roleList);
        }

        public ActionResult AddRoleToUser()
        {

            return View();
        }
    }
}