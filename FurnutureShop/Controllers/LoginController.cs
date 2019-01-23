using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnutureShop.Models;

namespace FurnutureShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            // show the login, req login, saves gets the userdata, gets current userdata
            if (HttpContext.Request.RequestType=="POST")

            {
                var Email = Request.Form["email"];
                var Password = Request.Form["password"];

                var CurrentUser = UserData.GetUserData(Email);
                if (CurrentUser != null && CurrentUser.Password == Password)
                {
                    Session["UserId"] = CurrentUser.Id;
                    return RedirectToAction("Index", "Home");

                }
            

            }
            return View();
        }
    }
}