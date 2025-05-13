using InvoiceServiceProvider;
using Presentation.Models.Invoices;
using Google.Protobuf.WellKnownTypes;

namespace Presentation.Factories;

public class InvoiceFactory
{
    public static InvoiceViewModel? ToInvoiceViewModel(Invoice invoice)
    {
        if (invoice is null)
            return null;

        InvoiceViewModel invoiceViewModel = new()
        {
            InvoiceId = invoice.InvoiceId,
            BookingId = invoice.BookingId,
            FirstName = invoice.FirstName,
            LastName = invoice.LastName,
            PhoneNumber = invoice.PhoneNumber,
            Address = invoice.Address,
            PostalCode = invoice.PostalCode,
            City = invoice.City,
            EventName = invoice.EventName,
            EventDate = TimestampFactory.ToViewModel(invoice.EventDate),
            TicketAmount = invoice.TicketAmount,
            TicketPrice = invoice.TicketPrice,
            TotalPrice = invoice.TotalPrice,
            BookingDate = TimestampFactory.ToViewModel(invoice.BookingDate),
            CreatedDate = TimestampFactory.ToViewModel(invoice.CreatedDate),
            DueDate = TimestampFactory.ToViewModel(invoice.DueDate),
            Paid = invoice.Paid,
            Deleted = invoice.Deleted
        };
        return invoiceViewModel;
    }

    public static Invoice? ToInvoice(InvoiceViewModel invoiceViewModel)
    {
        if (invoiceViewModel is null)
            return null;

        Invoice invoice = new()
        {
            InvoiceId = invoiceViewModel.InvoiceId,
            BookingId = invoiceViewModel.BookingId,
            FirstName = invoiceViewModel.FirstName,
            LastName = invoiceViewModel.LastName,
            PhoneNumber = invoiceViewModel.PhoneNumber,
            Address = invoiceViewModel.Address,
            PostalCode = invoiceViewModel.PostalCode,
            City = invoiceViewModel.City,
            EventName = invoiceViewModel.EventName,
            EventDate = TimestampFactory.FromViewModel(invoiceViewModel.EventDate),
            TicketAmount = invoiceViewModel.TicketAmount,
            TicketPrice = invoiceViewModel.TicketPrice,
            TotalPrice = invoiceViewModel.TotalPrice,
            BookingDate = TimestampFactory.FromViewModel(invoiceViewModel.BookingDate),
            CreatedDate = TimestampFactory.FromViewModel(invoiceViewModel.CreatedDate),
            DueDate = TimestampFactory.FromViewModel(invoiceViewModel.DueDate),
            Paid = invoiceViewModel.Paid,
            Deleted = invoiceViewModel.Deleted
        };
        return invoice;
    }
}