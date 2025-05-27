using Presentation.Models.Bookings;
using System.Globalization;

namespace Presentation.Dtos
{
    public class BookingServiceResult
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public BookingViewModel? Booking { get; set; }
        public List<BookingViewModel>? Bookings { get; set; }
    }
}
