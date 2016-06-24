using ImplementRole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImplementRole.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Content("You are authorized");
            }
            else
            {
                return Content("You are not authorized");
            }
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new CustomUser { UserName = registerViewModel.Email, Email = registerViewModel.Email, FirstName = registerViewModel.FirstName, LastName = registerViewModel.LastName};
                var password = registerViewModel.Password;
                var result = await userManager.CreateAsync(user,password);
                if (result.Succeeded)
	            {
                    await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index","Home");
	            }
            }
            return View(registerViewModel);
        }

        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}