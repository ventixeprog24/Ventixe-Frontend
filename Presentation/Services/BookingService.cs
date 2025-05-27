using BookingServiceProvider;
using Google.Protobuf.WellKnownTypes;
using Presentation.Dtos;
using Presentation.Factories;
using Presentation.Models.Bookings;
using BookingServiceClient = BookingServiceProvider.BookingServiceContract.BookingServiceContractClient;


namespace Presentation.Services
{
    public class BookingService(BookingServiceClient bookingService)
    {
        private readonly BookingServiceClient _bookingService = bookingService;

        // GET ALL BOOKINGS
        public async Task<BookingServiceResult> GetAllBookings()
        {
            try
            {
                // GET ALL BOOKINGS
                var bookingsReply = await _bookingService.GetAllBookingsAsync(new Empty());
                if (bookingsReply == null)
                {
                    return new BookingServiceResult { IsSuccess = false, Message = "No bookings found in database." };
                }

                var bookingsResult = new BookingServiceResult { IsSuccess = true, Bookings = new List<BookingViewModel>() };

                // BUILD BOOKING MODEL FROM REPLY
                foreach (var booking in bookingsReply!.Bookings)
                {
                    var bookingViewModel = BookingModelFactory.ToBookingViewModel(booking);

                    bookingsResult.Bookings?.Add(bookingViewModel);
                }
                return bookingsResult;
            }
            catch (Exception ex)
            {
                return new BookingServiceResult { IsSuccess = false, Message = ex.Message };
            }
        }

        // GET ALL BOOKINGS FOR USER
        public async Task<BookingServiceResult> GetAllBookingsByUserId(string userId)
        {
            try
            {
                // GET ALL USER SPECIFIC BOOKINGS
                var bookingsReply = await _bookingService.GetAllBookingsByUserIdAsync(new RequestGetAllBookingsByUserId { Userid = userId });
                if (bookingsReply == null)
                {
                    return new BookingServiceResult { IsSuccess = false, Message = "No user bookings found in database." };
                }

                var bookingsResult = new BookingServiceResult { IsSuccess = true, Bookings = new List<BookingViewModel>() };

                // BUILD BOOKING MODEL FROM REPLY
                foreach (var booking in bookingsReply!.Bookings)
                {
                        var bookingViewModel = BookingModelFactory.ToBookingViewModel(booking);
                        bookingsResult.Bookings?.Add(bookingViewModel);                    
                }

                return bookingsResult;
            }
            catch (Exception ex)
            {
                return new BookingServiceResult { IsSuccess = false, Message = ex.Message };
            }
        }

        // GET BOOKING
        public async Task<BookingServiceResult> GetBookingAsync(string bookingId)
        {
            try
            {
                // GET BOOKING BY ID
                var booking = await _bookingService.GetBookingAsync(new RequestGetBooking { Id = bookingId });
                if (booking == null)
                {
                    return new BookingServiceResult { IsSuccess = false, Message = "No booking found in database." };
                }

                // BUILD BOOKING MODEL FROM REPLY
                var bookingViewModel = BookingModelFactory.ToBookingViewModel( booking.Booking);

                return new BookingServiceResult { IsSuccess = true, Booking = bookingViewModel };
            }
            catch (Exception ex)
            {
                return new BookingServiceResult { IsSuccess = false, Message = ex.Message };
            }
        }

        // CREATE BOOKING
        public async Task<BookingServiceResult> CreateBookingAsync(string userId, string eventId, int ticketAmount)
        {
            try
            {
                // CREATE BOOKING
                var isBookingCreated = await _bookingService.CreateBookingAsync(
                    new RequestCreateBooking
                    {
                        Userid = userId,
                        Eventid = eventId,
                        Ticketamount = ticketAmount
                    });
                if (!isBookingCreated.IsSuccess)
                {
                    return new BookingServiceResult { IsSuccess = false, Message = "Failed to create booking." };
                }

                return new BookingServiceResult { IsSuccess = true, Message = "Booking created successfully." };
            }
            catch (Exception ex)
            {
                return new BookingServiceResult { IsSuccess = false, Message = ex.Message };
            }
        }

        // DELETE BOOKING
        public async Task<BookingServiceResult> DeleteBookingAsync(string bookingId)
        {
            try
            {
                // DELETE BOOKING BY ID
                var isBookingDeleted = await _bookingService.DeleteBookingAsync(new RequestDeleteBooking { Id = bookingId });
                if (!isBookingDeleted.IsSuccess)
                {
                    return new BookingServiceResult { IsSuccess = false, Message = "Failed to delete booking." };
                }

                return new BookingServiceResult { IsSuccess = true, Message = "Booking deleted successfully." };
            }
            catch (Exception ex)
            {
                return new BookingServiceResult { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
