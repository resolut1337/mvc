using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OnlineLearningMVC.Services
{
    public class CookieCartService : ICartService
    {
        private readonly IHttpContextAccessor _http;
        private const string CookiePrefix = "cart_";

        public CookieCartService(IHttpContextAccessor http)
        {
            _http = http;
        }

        private string CookieName(string userId) => CookiePrefix + (string.IsNullOrEmpty(userId) ? "anon" : userId);

        public void AddToCart(string userId, int courseId)
        {
            var key = CookieName(userId);
            var req = _http.HttpContext.Request;
            var res = _http.HttpContext.Response;
            List<int> list = new List<int>();
            if (req.Cookies.TryGetValue(key, out var json))
            {
                list = JsonConvert.DeserializeObject<List<int>>(json) ?? new List<int>();
            }
            if(!list.Contains(courseId)) list.Add(courseId);
            res.Cookies.Append(key, JsonConvert.SerializeObject(list), new CookieOptions { Expires = System.DateTimeOffset.UtcNow.AddDays(30) });
        }

        public void RemoveFromCart(string userId, int courseId)
        {
            var key = CookieName(userId);
            var req = _http.HttpContext.Request;
            var res = _http.HttpContext.Response;
            if (req.Cookies.TryGetValue(key, out var json))
            {
                var list = JsonConvert.DeserializeObject<List<int>>(json) ?? new List<int>();
                list.Remove(courseId);
                res.Cookies.Append(key, JsonConvert.SerializeObject(list), new CookieOptions { Expires = System.DateTimeOffset.UtcNow.AddDays(30) });
            }
        }

        public List<int> GetCart(string userId)
        {
            var key = CookieName(userId);
            var req = _http.HttpContext.Request;
            if (req.Cookies.TryGetValue(key, out var json))
                return JsonConvert.DeserializeObject<List<int>>(json) ?? new List<int>();
            return new List<int>();
        }

        public int GetCount(string userId) => GetCart(userId).Count;

        public void ClearCart(string userId)
        {
            var key = CookieName(userId);
            _http.HttpContext.Response.Cookies.Delete(key);
        }
    }
}
