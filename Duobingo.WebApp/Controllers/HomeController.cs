using Microsoft.AspNetCore.Mvc;

namespace Duobingo.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
