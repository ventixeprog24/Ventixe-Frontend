using Presentation.Models.Invoices;

namespace Presentation.Dtos;

public class InvoiceServiceResult
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public InvoiceViewModel? Invoice { get; set; }
    public List<InvoiceViewModel>? AllInvoices { get; set; }
}