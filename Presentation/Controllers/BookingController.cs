using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BookingController : Controller
    {
        [Route("Home/bookings")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Bookings";
            return View();
        }
    }
}
