using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serilog;
namespace MvcTrainingProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                Session["User"] = "admin";
                return RedirectToAction("Register", "Student");
            }
            Log.Warning("invalid login attempt for username: {Username}", username);
            ViewBag.Error = "Invalid Credentials";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}