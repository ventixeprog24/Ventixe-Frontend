using Authentication.Entities;
using Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Bookings;
using Presentation.Models.Events;
using Presentation.Services;
using UserProfileServiceProvider;
using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;


namespace Presentation.Controllers
{
    [Authorize]
    public class BookingController(BookingService bookingService, ICurrentUserService currentUserService, LocationService locationService, UserManager<AppUserEntity> userManager, UserProfileServiceClient userProfileService) : Controller
    {
        private readonly BookingService _bookingService = bookingService;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly LocationService _locationService = locationService;
        private readonly UserManager<AppUserEntity> _userManager = userManager;
        private readonly UserProfileServiceClient _userProfileService = userProfileService;

        [Route("bookings")]
        public async Task<IActionResult> Index()
        {
            var isAdmin = User.IsInRole("Admin");

            string userId = string.Empty;

            // CHECK IF USER IS ADMIN
            if (!isAdmin)
            {
                // GET USERID
                userId = _userManager.GetUserId(User)!;
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // GET USER BOOKINGS OR ALL BOOKINGS IF ADMIN
            var bookings = isAdmin
                ? await _bookingService.GetAllBookings()
                : await _bookingService.GetAllBookingsByUserId(userId);

            ViewData["Title"] = "Bookings";
            if (!bookings.IsSuccess)
            {
                return View(new List<BookingViewModel>());
            }
            return View(bookings.Bookings);
        }

        [Route("bookings/bookingdetails/{id}")]
        public async Task<IActionResult> BookingDetails(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Index", "Home");
            }
            // GET BOOKING BY ID
            var bookingModel = await _bookingService.GetBookingAsync(id);

            if (!bookingModel.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Title"] = "Booking Details";
            return View(bookingModel.Booking);
        }

        [HttpPost("bookings/createbooking")]
        public async Task<IActionResult> BookingForm(EventViewModel eventModel)
        {
            // GET USERID
            string userId = _userManager.GetUserId(User)!;
            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            //GET USER PROFILE BY ID
            var userProfileReply = await _userProfileService.GetUserProfileByIdAsync(new RequestByUserId { UserId = userId });
            if (userProfileReply == null || userProfileReply.Profile == null)
            {
                TempData["ErrorMessage"] = "User profile not found. Please update your profile information.";
                return RedirectToAction("Index", "UserProfile");
            }

            // GET LOCATION BY ID
            var locationReply = await _locationService.GetLocationById(eventModel.LocationId!);
            if (locationReply.Result == null)
            {
                TempData["ErrorMessage"] = "Location not found. Please select a valid location.";
                return RedirectToAction("Index", "Event");
            }

            // BUILD BOOKING MODEL
            BookingViewModel bookingModel = new()
            {
                UserId = userId,
                EventId = eventModel.EventId,
                FirstName = userProfileReply.Profile.FirstName,
                LastName = userProfileReply.Profile.LastName,
                Email = userProfileReply.Profile.Email,
                PhoneNumber = userProfileReply.Profile.PhoneNumber,
                Address = userProfileReply.Profile.Address,
                PostalCode = userProfileReply.Profile.PostalCode,
                City = userProfileReply.Profile.City,
                EventName = eventModel.EventTitle,
                LocationName = locationReply.Result!.Name,
                TicketPrice = eventModel.Price,
                EventDate = eventModel.Date!.Value,
                TotalTickets = eventModel.TotalTickets,
                TicketsSold = eventModel.TicketsSold,
            };

            ViewData["Title"] = "Create Booking";
            return View(bookingModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingViewModel bookingModel)
        {
            // VALIDATE TICKET AMOUNT
            if (bookingModel.TicketAmount < 1)
            {
                ModelState.AddModelError("TicketAmount", "Ticket amount must be at least 1.");
                return View("BookingForm", bookingModel);

            }

            if (!ModelState.IsValid)
            {
                return View("BookingForm", bookingModel);
            }

            // CREATE BOOKING
            var booking = await _bookingService.CreateBookingAsync(bookingModel.UserId, bookingModel.EventId, bookingModel.TicketAmount);
            if (!booking.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["Title"] = "Create Booking";
            return RedirectToAction("Index", "Booking");
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