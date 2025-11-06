using System.Collections.Generic;

namespace OnlineLearningMVC.Services
{
    public interface ICartService
    {
        void AddToCart(string userId, int courseId);
        void RemoveFromCart(string userId, int courseId);
        List<int> GetCart(string userId);
        int GetCount(string userId);
        void ClearCart(string userId);
    }
}
