using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using Users_management_application.Models;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(Users_management_application.Startup))]

namespace Users_management_application
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new UserManager<AppAccount>(new AppAccountsStore()));
            app.CreatePerOwinContext<SignInManager<AppAccount, string>>(CreateSignInManager);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });
        }


        private SignInManager<AppAccount,string> CreateSignInManager(IdentityFactoryOptions<SignInManager<AppAccount, string>> options, IOwinContext context)
        {
            return new SignInManager<AppAccount, string>(context.GetUserManager<UserManager<AppAccount>>(), context.Authentication);
        }
    }

}
