using System.Web.Mvc;
using Serilog;
using MvcTrainingProject.Models;
using MvcTrainingProject.Filters;
namespace MvcTrainingProject.Controllers
{
    [AuthFilter]
    [ActionLogFilter]
    [ResultFilter]
    [ExceptionFilter]
    public class StudentController : Controller
    {
        // -----------------------------
        // Normal Register Page
        // -----------------------------
        public ActionResult Register()
        {
            Log.Information("Register page loaded");

            // Show success message after POST-Redirect-Get
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"].ToString();
            }

            return View();
        }

        // -----------------------------
        // 1️⃣ Custom Manual Exception
        // -----------------------------
        public ActionResult TestException()
        {
            Log.Information("TestException action triggered");
            throw new System.Exception("Testing Database Exception");
        }

        // -----------------------------
        // 2️⃣ Divide By Zero Exception
        // -----------------------------
        public ActionResult DivideByZero()
        {
            Log.Information("DivideByZero action triggered");

            int x = 10;
            int y = 0;
            int result = x / y;   // Runtime error

            return View();
        }

        // -----------------------------
        // 3️⃣ Null Reference Exception
        // -----------------------------
        public ActionResult NullReference()
        {
            Log.Information("NullReference action triggered");

            string name = null;
            int length = name.Length;  // Runtime error

            return View();
        }

        // -----------------------------
        // 4️⃣ Format Exception
        // -----------------------------
        public ActionResult FormatError()
        {
            Log.Information("FormatError action triggered");

            int number = System.Convert.ToInt32("ABC");

            return View();
        }

        // -----------------------------
        // POST Register
        // -----------------------------
        [HttpPost]
        public ActionResult Register(Student student)
        {
            if (!ModelState.IsValid)
            {
                // ✅ Validation logging add kiya
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        Log.Warning("Validation error in Register: {ErrorMessage}", error.ErrorMessage);
                    }
                }

                return View(student);
            }

            Log.Information("Registration successful for {FullName}", student.FullName);

            // Use Post-Redirect-Get to show success message and avoid double submit
            TempData["Success"] = "Registration Successful for " + student.FullName;
            return RedirectToAction("Register");
        }
    }
}