using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult AdminIndex()
        {
            return View("AdminIndex");
        }

        public IActionResult Back()
        {
            return RedirectToAction("", "Home");
        }
    }
}
