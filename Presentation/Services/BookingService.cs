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
                var bookingsReply = await _bookingService.GetAllBookingsAsync(new Empty());
                if (bookingsReply == null)
                {
                    return new BookingServiceResult { IsSuccess = false, Message = "No bookings found in database." };
                }

                var bookingsResult = new BookingServiceResult { IsSuccess = true, Bookings = new List<BookingViewModel>() };

                // TESTING WITH DUMMY DATA
                var bookingViewModel = BookingModelFactory.ToBookingViewModel();
                bookingsResult.Bookings?.Add(bookingViewModel);

                //foreach (var booking in bookingsReply!.Bookings)
                //{
                //    var bookingViewModel = BookingModelFactory.CreateBookingViewModel(booking);

                //    bookingsResult.Bookings?.Add(bookingViewModel);
                //}
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
                //var booking = await _bookingService.GetBookingAsync(new RequestGetBooking { Id = bookingId });
                //if (booking == null)
                //{
                //    return new BookingServiceResult { IsSuccess = false, Message = "No booking found in database." };
                //}

                var bookingViewModel = BookingModelFactory.ToBookingViewModel(/* booking.Booking */);

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
