namespace Presentation.Models.Bookings
{
    public class CreateBookingViewModel
    {
        public string UserId { get; set; } = null!;
        public string EventId { get; set; } = null!;
        public int TicketAmount { get; set; }
    }
}
