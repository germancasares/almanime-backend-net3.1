using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AnimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}