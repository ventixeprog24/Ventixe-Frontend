using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Models.Invoices;

public class InvoiceViewModel
{
    public string InvoiceId { get; set; } = null!;
    public string BookingId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string EventName { get; set; } = null!;
    public string EventDate { get; set; } = null!;
    public double TicketAmount { get; set; }
    public double TicketPrice { get; set; }
    public double TotalPrice { get; set; }
    public string BookingDate { get; set; } = null!;
    public string CreatedDate { get; set; } = null!;
    public string DueDate { get; set; } = null!;
    public bool Paid { get; set; }
    public bool Deleted { get; set; }
}