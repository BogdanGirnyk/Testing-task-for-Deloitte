using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Users_management_application.Models;
using Users_management_application.ViewModels;

namespace Users_management_application.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {

        private SignInManager<AppAccount,string> signInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<SignInManager<AppAccount, string>>();
            }
        }


        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(LogInModel model, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await signInManager.PasswordSignInAsync(model.Login, model.Password, true,  false);
            if (result == SignInStatus.Success)
            {
                return RedirectToLocal(ReturnUrl);
            }
            else
            {
                // user authN failed
                ModelState.AddModelError("", "Invalid login or password");
                return View();
            }
        }


        //UTILS

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Users");
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }

    }
}