using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcTrainingProject.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // 1️⃣ ViewResult
        public ActionResult Index()
        {
            return View();
        }

        // 2️⃣ PartialViewResult
        public ActionResult ShowPartial()
        {
            return PartialView("Partial"); // Partial.cshtml
        }

        // 3️⃣ JsonResult
        public ActionResult GetJson()
        {
            var data = new { Name = "Hariom", Role = "Admin" };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // 4️⃣ ContentResult
        public ActionResult ShowContent()
        {
            return Content("This is plain text content from Admin Area");
        }

        // 5️⃣ RedirectResult
        public ActionResult GoToGoogle()
        {
            return Redirect("https://google.com");
        }

        // 6️⃣ RedirectToActionResult
        public ActionResult GoToIndex()
        {
            return RedirectToAction("Index");
        }

        // 7️⃣ RedirectToRouteResult (Cross Area)
        public ActionResult GoToUserArea()
        {
            return RedirectToRoute(new
            {
                area = "User",
                controller = "Home",
                action = "Index"
            });
        }

        // 8️⃣ FileResult
        public ActionResult DownloadFile()
        {
            byte[] fileBytes = Encoding.UTF8.GetBytes("Hello this is a sample file.");
            return File(fileBytes, "text/plain", "Sample.txt");
        }

        // 9️⃣ StatusCodeResult
        public ActionResult ReturnStatus()
        {
            return new HttpStatusCodeResult(404, "Not Found");
        }

        // 🔟 EmptyResult
        public ActionResult DoNothing()
        {
            return new EmptyResult();
        }
    }
}