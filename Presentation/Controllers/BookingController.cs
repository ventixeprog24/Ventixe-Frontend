    using Authentication.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Presentation.Models.Bookings;
    using Presentation.Services;

    namespace Presentation.Controllers
    {
        [Authorize]
        public class BookingController(BookingService bookingService, ICurrentUserService currentUserService) : Controller
        {
            private readonly BookingService _bookingService = bookingService;
            private readonly ICurrentUserService _currentUserService = currentUserService;

            [Route("bookings")]
            public async Task<IActionResult> Index()
            {
                var isAdmin = User.IsInRole("Admin");

                string userId = string.Empty;
                if (!isAdmin)
                {
                    // GET USERID
                    var header = await _currentUserService.GetHeaderViewModelAsync();
                    userId = header.UserId;
                }

                // TESTING WITH DUMMY DATA
                var bookings = await _bookingService.GetAllBookings();

                // UNCOMMENT TO USE REAL DATA WHEN AVAILABLE
                //var bookings = isAdmin
                //    ? await _bookingService.GetAllBookings()
                //    : await _bookingService.GetAllBookingsByUserId(userId);

                ViewData["Title"] = "Bookings";
                if (!bookings.IsSuccess)
                {
                    return View(new List<BookingViewModel>());
                }
                return View(bookings.Bookings);
            }

            [Route("bookings/bookingdetails")] // lägg till ID
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

            [HttpPost("bookings/createbooking")]
            public IActionResult BookingForm(BookingViewModel bookingModel)
            {
                ViewData["Title"] = "Create Booking";
                return View(bookingModel);
            }

            [HttpPost]
            public async Task<IActionResult> CreateBooking(BookingViewModel bookingModel)
            {
                if (bookingModel.TicketAmount < 1)
                {
                    ModelState.AddModelError("TicketAmount", "Ticket amount must be at least 1.");
                    return View("BookingForm", bookingModel);

                }
                if (!ModelState.IsValid)
                {
                    return View("BookingForm", bookingModel);
                }
                var booking = await _bookingService.CreateBookingAsync(bookingModel.UserId, bookingModel.EventId, bookingModel.TicketAmount);
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
                    ViewData["Title"] = "Booking Details";
                    return RedirectToAction("BookingDetails", booking.Booking);
                }

                ViewData["Title"] = "Bookings";
                return RedirectToAction("Index", "Booking");
            }
        }
    }
