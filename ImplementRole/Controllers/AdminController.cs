using ImplementRole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

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
            var result = (from m in db.Roles select m).ToList();
            return View(result);
        }
    }
}