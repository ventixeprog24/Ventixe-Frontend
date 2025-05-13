using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Models.Invoices;

public class InvoiceViewModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Invoice Id")]
    public string InvoiceId { get; set; } = Guid.NewGuid().ToString();
    
    [DataType(DataType.Text)]
    [Display(Name = "Booking Id")]
    public string BookingId { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Address")]
    public string Address { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "City")]
    public string City { get; set; } = null!;
    
    [DataType(DataType.Text)]
    [Display(Name = "Event Name")]
    public string EventName { get; set; } = null!;
    
    [DataType(DataType.DateTime)]
    [Display(Name = "Event Date")]
    public DateTime EventDate { get; set; }
    
    [Display(Name = "Amount of Tickets")]
    public double TicketAmount { get; set; }
    
    [Display(Name = "Ticket price")]
    public double TicketPrice { get; set; }
    
    [Display(Name = "Total amount")]
    public double TotalPrice { get; set; }
    
    [DataType(DataType.DateTime)]
    [Display(Name = "Booking Date")]
    public DateTime BookingDate { get; set; }
    
    [DataType(DataType.Text)]
    [Display(Name = "Invoice Date")]
    public string CreatedDate { get; set; }
    
    [DataType(DataType.DateTime)]
    [Display(Name = "Due Date")]
    public string DueDate { get; set; }
    
    [Display(Name = "Amount of Tickets")]
    public bool Paid { get; set; } = false;
    
    [HiddenInput(DisplayValue = false)]
    public bool Deleted { get; set; } = false;
}