using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Library_Management_System.Controllers
{
    [Authorize]
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
