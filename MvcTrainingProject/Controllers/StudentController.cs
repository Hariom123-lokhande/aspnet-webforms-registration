using System.Web.Mvc;
using MvcTrainingProject.Models;

namespace MvcTrainingProject.Controllers
{
    public class StudentController : Controller
    {
        // GET
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            ViewBag.Success = "Registration Successful for " + student.FullName;

            ModelState.Clear();

            return View(new Student());   // Fresh empty model
        }
    }
}