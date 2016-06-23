using ImplementRole.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImplementRole.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            var context = new ApplicationDbContext();
            var userStore = new UserStore<CustomUser>(context);
            var manager = new UserManager<CustomUser>(userStore);

            var email = "foo@bar.com";
            var password = "Passw0rd";
            var user = await manager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new CustomUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = "Super",
                    LastName = "Admin"
                };
                await manager.CreateAsync(user, password);
            }
            else
            {
                user.FirstName = "Super";
                user.LastName = "Admin";
                await manager.UpdateAsync(user);
            }
            return Content("Hello Index");
        }
    }
}