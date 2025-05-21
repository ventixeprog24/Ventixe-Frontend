using BookingServiceProvider;
using Presentation.Models.Bookings;

namespace Presentation.Factories
{
    public class BookingModelFactory
    {
        public static BookingViewModel CreateBookingViewModel()
        {
            //return new BookingViewModel
            //{
            //    Id = booking.Bookingid,
            //    UserId = booking.Userid,
            //    EventId = booking.Eventid,
            //    FirstName = booking.Firstname,
            //    LastName = booking.Lastname,
            //    Email = booking.Email,
            //    PhoneNumber = booking.Phone,
            //    Address = booking.Address,
            //    PostalCode = booking.Postalcode,
            //    City = booking.City,
            //    EventName = booking.Eventname,
            //    TicketAmount = booking.Ticketamount,
            //    TicketPrice = booking.Ticketprice,
            //    TotalPrice = booking.Totalprice,
            //    EventDate = booking.Eventdate.ToDateTime(),
            //    Created = booking.Created.ToDateTime()
            //};
            BookingViewModel bookingViewModel = new()
            {
                Id = "opkok",
                UserId = "poajsa",
                EventId = "poasjdpo",
                FirstName = "pisjf",
                LastName = "poasd",
                Email = "posjfa",
                PhoneNumber = "ipsjfpaos",
                Address = "poajsd",
                PostalCode = "posjfs",
                City = "psjfa",
                EventName = "opja",
                TicketAmount = 2,
                TicketPrice = 10,
                TotalPrice = 20,
                EventDate = DateTime.Now,
                Created = DateTime.Now
            };
            return bookingViewModel;
        }
    }
}
