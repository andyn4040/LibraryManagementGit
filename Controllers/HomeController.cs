using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ISessionService _sessionService;
        private int currentUserId;
        private User currentUser;
        private bool isAdmin;

        public HomeController(ApplicationContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        #region GET ************************************************************************************************************************************************
        //GET: Display home page
        public IActionResult Index()
        {
            currentUserId = _sessionService.GetSessionValue<int>("UserId");
            currentUser = _context.Users.FirstOrDefault(x => x.UserId == currentUserId);

            if (currentUser != null) 
            {
                isAdmin = (currentUser.Type == UserType.Admin);
            }

            ViewData["IsAdmin"] = isAdmin;
            return View("HomeIndex");
        }

        //GET: Display admin page
        public IActionResult AdminIndex()
        {
            return View("AdminIndex");
        }
        #endregion


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}