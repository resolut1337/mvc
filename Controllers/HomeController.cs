using Microsoft.AspNetCore.Mvc;

namespace OnlineLearningMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Courses");
        }
    }
}
