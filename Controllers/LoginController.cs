using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Models;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Library_Management_System.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Library_Management_System.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ISessionService _sessionService;
        private readonly Services.AuthenticationService _authenticationService;

        public LoginController(ApplicationContext context, ISessionService sessionService, Services.AuthenticationService authenticationService)
        {
            _context = context;
            _sessionService = sessionService;
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (_authenticationService.AuthenticateUser(email, password))
            {
                var user = _context.Users.FirstOrDefault(e => e.Email == email && e.Password == password);
                if (user != null)
                {
                    _sessionService.SetSessionValue("UserId", user.UserId);
                    
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties {};
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                    return View("Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View("Login");
            }
        }

        public IActionResult CreateAccount()
        {
            return RedirectToAction("Create", "Users");
        }
    }
}
