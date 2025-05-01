using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class EventController : Controller
    {
        [Route("Home/events")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Events";
            return View();
        }
    }
}
