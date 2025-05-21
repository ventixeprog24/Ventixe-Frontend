using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Bookings;
using Presentation.Services;

namespace Presentation.Controllers
{
    public class BookingController(BookingService bookingService) : Controller
    {
        private readonly BookingService _bookingService = bookingService;

        [Route("Home/bookings")]
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookings();

            Console.WriteLine($"CONTROLLER: Is Bookings null: {bookings.Bookings?.Count}");
            //if (!bookings.IsSuccess)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            ViewData["Title"] = "Bookings";
            return View(bookings.Bookings);
        }
    }
}
