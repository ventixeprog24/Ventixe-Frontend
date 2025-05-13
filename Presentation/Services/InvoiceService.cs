using Google.Protobuf.WellKnownTypes;
using Presentation.Dtos;
using Presentation.Factories;
using Presentation.Models.Invoices;
using InvoiceServiceContractClient = InvoiceServiceProvider.InvoiceServiceContract.InvoiceServiceContractClient;

namespace Presentation.Services;

public class InvoiceService(InvoiceServiceContractClient invoiceServiceContractClient)
{
    private readonly InvoiceServiceContractClient _invoiceServiceContractClient =  invoiceServiceContractClient;

    public async Task<InvoiceServiceResult> GetAllInvoices()
    {
        try
        { 
            var serviceResult = await _invoiceServiceContractClient.GetAllInvoicesAsync(new Empty());
            if (serviceResult is null || !serviceResult.Succeeded)
                return new InvoiceServiceResult { Succeeded = false, Message = "Something went wrong" };
            
            var returnList = serviceResult.AllInvoices.Select(InvoiceFactory.ToInvoiceViewModel).ToList();
            return (returnList is not null)
                ? new InvoiceServiceResult { Succeeded = true , AllInvoices = returnList }
                : new InvoiceServiceResult { Succeeded = false, Message = "Something went wrong when converting to view models" };
        }
        catch (Exception ex)
        {
            return new InvoiceServiceResult { Succeeded = false, Message = ex.Message };
        }
        
    }
}