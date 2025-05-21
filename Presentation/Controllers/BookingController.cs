using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Bookings;
using Presentation.Services;

namespace Presentation.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class BookingController(BookingService bookingService) : Controller
    {
        private readonly BookingService _bookingService = bookingService;

        [Route("Home/bookings")]
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookings();

            //if (!bookings.IsSuccess)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            ViewData["Title"] = "Bookings";
            return View(bookings.Bookings);
        }

        [Route("Home/bookingdetails")]
        public async Task<IActionResult> BookingDetails(string id)
        {
            var booking = await _bookingService.GetBookingAsync(id);
            if (!booking.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Title"] = "Booking Details";
            return View(booking.Booking);
        }
    }
}
