using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Users_management_application.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {

        public ActionResult Index()
        {
            var model = (new UsersRepository()).GetAllUsers();
            return View(model);
        }

        public ActionResult Details(Guid Id)
        {
            var model = (new UsersRepository()).GetUserById(Id);
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return new HttpNotFoundResult("User not found");
            }
        }

        public ActionResult Image(string id)
        {
            var yy = (new UsersRepository()).GetAllUsers();
            var dir = Server.MapPath("~/App_Data/UsersPictures");
            var path = Path.Combine(dir, id);

            return File(path, "image/png");
        }
    }
}