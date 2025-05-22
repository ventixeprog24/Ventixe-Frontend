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

        [HttpGet("bookings/bookingdetails")] // lägg till ID
        //[Route("bookins/bookingdetails")]
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

        public async Task<IActionResult> CreateBooking()
        {
            var booking = await _bookingService.GetAllBookings();
            if (!booking.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Title"] = "Create Booking";
            return View(booking.Bookings);
        }

        public async Task<IActionResult> DeleteBooking(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Index", "Home");
            }

            // DELETE BOOKING
            var deleteResult = await _bookingService.DeleteBookingAsync(id);

            if (!deleteResult.IsSuccess)
            {
                // GET BOOKING IF DELETE FAILED
                var booking = await _bookingService.GetBookingAsync(id);
                if (booking == null)
                {
                    TempData["ErrorMessage"] = "Failed to cancel booking, try again later.";
                    return RedirectToAction("Index", "Booking");
                }

                ViewBag.ErrorMessage = "Failed to cancel booking, try again later.";
                return View("BookingDetails", booking.Booking);
            }

            return RedirectToAction("Index", "Booking");
        }
    }
}
