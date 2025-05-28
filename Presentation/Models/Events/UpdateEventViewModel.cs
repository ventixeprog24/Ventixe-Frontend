namespace Presentation.Models.Events
{
    public class UpdateEventViewModel
    {
        public string EventId { get; set; } = null!;
        public string EventTitle { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? Date { get; set; }
        public int Price { get; set; }

        public string SelectedCategoryId { get; set; } = null!;
        public string SelectedStatusId { get; set; } = null!;
        public string SelectedLocationId { get; set; } = null!;

        public int TotalTickets { get; set; }
        public int TicketsSold { get; set; }

    }
}
