using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Models;
using System.Web;

namespace Library_Management_System.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ISessionService _sessionService;

        public LoginController(ApplicationContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email == email && e.Password == password);
            if (user != null)
            {
                _sessionService.SetSessionValue("UserId", user.UserId);
                return RedirectToAction("", "Home");
            }
            else
            {
                return View("Login");
            }
        }

        public IActionResult CreateAccount()
        {
            return RedirectToAction("Create", "Users");
        }
    }
}
