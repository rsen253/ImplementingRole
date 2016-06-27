using ImplementRole.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
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
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Account
        public ActionResult Index()
        {
            if (User.IsInRole(Security.Admin))
            {
                ViewBag.TotalUser = (from u in db.Users select u).Count() - 1;
                return View("Admin");
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            List<Country> CountryList = new List<Country>();
            List<State> StateList = new List<State>();
            CountryList = db.CountryDetails.OrderBy(c => c.CountryName).ToList();
            ViewBag.CountryID = new SelectList(CountryList, "CountryId", "CountryName");
            ViewBag.StateID = new SelectList(StateList, "StateId", "StateName");
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new CustomUser { UserName = registerViewModel.Email, Email = registerViewModel.Email, FirstName = registerViewModel.FirstName, LastName = registerViewModel.LastName, CountryId = registerViewModel.CountryId , StateId = registerViewModel.StateId };
                var password = registerViewModel.Password;
                var result = await userManager.CreateAsync(user,password);
                if (result.Succeeded)
	            {
                    if (!User.IsInRole(Security.Admin))
                    {
                        await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    
                    return RedirectToAction("Index","Home");
	            }
            }
            return View(registerViewModel);
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var result = await signInManager.PasswordSignInAsync(loginViewModel.Email,loginViewModel.Password,loginViewModel.RememberMe,shouldLockout:false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                    return RedirectToLocal(returnUrl);
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(loginViewModel);
            }
        }
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public JsonResult GetStates(RegisterViewModel registerViewModel)
        {
            List<State> StateList = new List<State>();
            var CountryID = registerViewModel.CountryId;
            int ID = CountryID;
            if (CountryID > 0)
            {
                
                 StateList = db.StateList.Where(a => a.CountryId.Equals(ID)).OrderBy(a => a.StateName).ToList();
                
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = StateList,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                //return Json(StateList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new JsonResult
                {
                    Data = "Not valid request",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}