using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Library_Management_System.Services
{
    public interface ISessionService
    {
        void SetSessionValue<T>(string key, T value);
        T GetSessionValue<T>(string key);
        void RemoveSessionValue(string key);
    }

    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetSessionValue<T>(string key, T value)
        {
            _httpContextAccessor.HttpContext?.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public T GetSessionValue<T>(string key)
        {
            var sessionValue = _httpContextAccessor.HttpContext?.Session.GetString(key);

            return sessionValue != null
                ? JsonConvert.DeserializeObject<T>(sessionValue)
                : default;
        }

        public void RemoveSessionValue(string key)
        {
            _httpContextAccessor.HttpContext?.Session.Remove(key);
        }
    }
}
