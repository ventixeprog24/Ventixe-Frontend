namespace Presentation.Models.Bookings
{
    public class BookingViewModel
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string EventId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;    
        public string Address { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string EventName { get; set; } = null!;
        public string ?LocationName { get; set; }
        public int SeatCount { get; set; }
        public int TicketAmount { get; set; }
        public double TicketPrice { get; set; }
        public double TotalPrice { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime Created { get; set; }
    }
}
