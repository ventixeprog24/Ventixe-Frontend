using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class LocationController : Controller
    {
        [Route("Home/locations")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Locations";
            return View();
        }
    }
}
