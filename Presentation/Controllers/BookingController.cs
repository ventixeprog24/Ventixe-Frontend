using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Bookings;
using Presentation.Services;
using System.Globalization;

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

        [Route("bookings/bookingdetails")] // lägg till ID
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

        [HttpPost("event/bookevent")]
        public IActionResult BookingForm(BookingViewModel bookingModel)
        {            
            return View(bookingModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingViewModel bookingModel)
        {
            Console.WriteLine($"UserId: {bookingModel.UserId}, EventId: {bookingModel.EventId}, TicketAmount: {bookingModel.TicketAmount}");

            if(bookingModel.TicketAmount < 1)
            {
                ModelState.AddModelError("TicketAmount", "Ticket amount must be at least 1.");
                return View("BookingForm", bookingModel);

            }
            if (!ModelState.IsValid)
            {
                return View("BookingForm", bookingModel);
            }
            //var booking = await _bookingService.CreateBookingAsync(bookingModel.UserId, bookingModel.EventId, bookingModel.TicketAmount);
            //if (!booking.IsSuccess)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //ViewData["Title"] = "Create Booking";
            //return View(booking.Bookings);

            return RedirectToAction("Index", "Home");
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
