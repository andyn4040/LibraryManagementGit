using Library_Management_System.Models;

namespace Library_Management_System.Services
{
    public class AuthenticationService
    {
        private readonly ApplicationContext _context;
        private readonly ISessionService _sessionService;

        public AuthenticationService(ApplicationContext context, ISessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        public bool AuthenticateUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email == email && e.Password == password);

            if (user != null)
            {
                _sessionService.SetSessionValue("UserId", user.UserId);
                return true;
            }

            return false;
        }
    }
}
