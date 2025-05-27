using BookingServiceProvider;
using Presentation.Models.Bookings;

namespace Presentation.Factories
{
    public class BookingModelFactory
    {
        public static BookingViewModel ToBookingViewModel(/* Booking booking */)
        {
            // UNCOMMENT WHEN REAL DATA IS AVALIABLE
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

            //TESTING WITH DUMMY DATA
            BookingViewModel bookingViewModel = new()
            {
                Id = "447c4f0c-49fb-4d56-abff-fdf650d7cb5b",
                UserId = "3bde95d9-5fa1-4cd6-b644-e137848331cb",
                EventId = "20e6473b-6f7c-4f66-af3e-6d49e05128e9",
                FirstName = "Urban",
                LastName = "Karlsson",
                Email = "urban@live.com",
                PhoneNumber = "0707070707",
                Address = "Danderyd",
                PostalCode = "12345",
                City = "Stockholm",
                EventName = "Nhl - San Jose Sharks vs LA Kings",
                TicketAmount = 0,
                TicketPrice = 10,
                TotalPrice = 20,
                EventDate = DateTime.Now,
                //EventDate = new DateTime(2000,01,01),
                Created = DateTime.Now
            }
            ;
            return bookingViewModel;
        }
    }
}
