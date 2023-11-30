using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class AdminController : Controller
    {
        #region GET
        // GET: AdminController
        public ActionResult AdminIndex()
        {
            return View("AdminIndex");
        }
        #endregion

        public IActionResult Back()
        {
            return RedirectToAction("", "Home");
        }
    }
}
