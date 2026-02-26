using System.Web.Mvc;
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
                return View();
            }

            // -----------------------------
            // 1️⃣ Custom Manual Exception
            // -----------------------------
            public ActionResult TestException()
            {
                throw new System. Exception("Testing Database Exception");
            }

            // -----------------------------
            // 2️⃣ Divide By Zero Exception
            // -----------------------------
            public ActionResult DivideByZero()
            {
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
                string name = null;
                int length = name.Length;  // Runtime error
                return View();
            }

            // -----------------------------
            // 4️⃣ Format Exception
            // -----------------------------
            public ActionResult FormatError()
            {
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
                    return View(student);
                }

                ViewBag.Success = "Registration Successful for " + student.FullName;

                ModelState.Clear();

                return View(new Student());
            }
        }
    }