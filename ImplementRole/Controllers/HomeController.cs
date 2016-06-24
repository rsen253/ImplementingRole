using ImplementRole.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImplementRole.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            var email = "foo@bar.com";
            var password = "Passw0rd";
            var user = await userManager.FindByEmailAsync(email);
            var roles = ApplicationRoleManeger.Create(HttpContext.GetOwinContext());

            if (!await roles.RoleExistsAsync(Security.Admin))
            {
                await roles.CreateAsync(new IdentityRole { Name = Security.Admin });
            }
            if (!await roles.RoleExistsAsync(Security.Accounting))
            {
                await roles.CreateAsync(new IdentityRole { Name = Security.Accounting });
            }
            if (!await roles.RoleExistsAsync(Security.It))
            {
                await roles.CreateAsync(new IdentityRole { Name = Security.It });
            }
            if (!await roles.RoleExistsAsync(Security.Police))
            {
                await roles.CreateAsync(new IdentityRole { Name = Security.Police });
            }
            if (!await roles.RoleExistsAsync(Security.Teacher))
            {
                await roles.CreateAsync(new IdentityRole { Name = Security.Teacher });
            }
            if (!await roles.RoleExistsAsync(Security.Student))
            {
                await roles.CreateAsync(new IdentityRole { Name = Security.Student });
            }
            if (user == null)
            {
                user = new CustomUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = "Super",
                    LastName = "Admin"
                };
                await userManager.CreateAsync(user, password);
            }
            else
            {
                await userManager.AddToRoleAsync(user.Id, Security.Admin);
            }
            //return Content("Hello Index");
            return View();
        }

        public async Task<ActionResult> Login()
        {
            var email = "foo@bar.com";
            //var password = "Passw0rd";
            var user = await userManager.FindByEmailAsync(email);
            await signInManager.SignInAsync(user,true,true);
            return RedirectToAction("Index");
        }

        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}