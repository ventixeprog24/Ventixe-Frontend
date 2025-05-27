using BookingServiceProvider;
using Presentation.Models.Bookings;

namespace Presentation.Factories
{
    public class BookingModelFactory
    {
        public static BookingViewModel ToBookingViewModel(Booking booking)
        {
            return new BookingViewModel
            {
                Id = booking.Bookingid,
                UserId = booking.Userid,
                EventId = booking.Eventid,
                FirstName = booking.Firstname,
                LastName = booking.Lastname,
                Email = booking.Email,
                PhoneNumber = booking.Phone,
                Address = booking.Address,
                PostalCode = booking.Postalcode,
                City = booking.City,
                EventName = booking.Eventname,
                TicketAmount = booking.Ticketamount,
                TicketPrice = booking.Ticketprice,
                TotalPrice = booking.Totalprice,
                EventDate = booking.Eventdate.ToDateTime(),
                Created = booking.Created.ToDateTime()
            };
        }
    }
}
