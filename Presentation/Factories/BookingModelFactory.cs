﻿using BookingServiceProvider;
using Presentation.Models.Bookings;

namespace Presentation.Factories
{
    public class BookingModelFactory
    {
        public static BookingViewModel ToBookingViewModel()
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
                FirstName = "Skurre",
                LastName = "Karlsson",
                Email = "Skurban@live.com",
                PhoneNumber = "0707080900",
                Address = "Hökarängen",
                PostalCode = "12345",
                City = "Stockholm",
                EventName = "Nhl - San Jose Shark vs LA Kings",
                TicketAmount = 0,
                TicketPrice = 10,
                TotalPrice = 20,
                EventDate = DateTime.Now,
                //EventDate = new DateTime(2000,01,01),
                Created = DateTime.Now
            };
            return bookingViewModel;
        }
    }
}
