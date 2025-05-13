using Google.Protobuf.WellKnownTypes;
using InvoiceServiceProvider;
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

    public async Task<InvoiceServiceResult> GetInvoiceByInvoiceId(string invoiceId)
    {
        try
        {
            var serviceResult = await _invoiceServiceContractClient.GetInvoiceByInvoiceIdAsync(
                new RequestInvoiceById { Id = invoiceId });
            if  (serviceResult is null || !serviceResult.Succeeded)
                return new InvoiceServiceResult { Succeeded = false, Message = "Something went wrong" };
            
            var returnInvoice = InvoiceFactory.ToInvoiceViewModel(serviceResult.Invoice);
            return (returnInvoice is not null)
                ? new InvoiceServiceResult { Succeeded = true , Invoice = returnInvoice }
                : new InvoiceServiceResult { Succeeded = false, Message = "Something went wrong when converting to view model" };
        }
        catch (Exception ex)
        {
            return new InvoiceServiceResult { Succeeded = false, Message = ex.Message };
        }
    }
    
    public async Task<InvoiceServiceResult> GetInvoiceByBookingId(string invoiceId)
    {
        try
        {
            var serviceResult = await _invoiceServiceContractClient.GetInvoiceByBookingIdAsync(
                new RequestInvoiceById { Id = invoiceId });
            if  (serviceResult is null || !serviceResult.Succeeded)
                return new InvoiceServiceResult { Succeeded = false, Message = "Something went wrong" };
            
            var returnInvoice = InvoiceFactory.ToInvoiceViewModel(serviceResult.Invoice);
            return (returnInvoice is not null)
                ? new InvoiceServiceResult { Succeeded = true , Invoice = returnInvoice }
                : new InvoiceServiceResult { Succeeded = false, Message = "Something went wrong when converting to view model" };
        }
        catch (Exception ex)
        {
            return new InvoiceServiceResult { Succeeded = false, Message = ex.Message };
        }
    }

    public async Task<InvoiceServiceResult> UpdatePaymentStatus(string invoiceId, bool paymentStatus)
    {
        try
        {
            var serviceResult = await _invoiceServiceContractClient.UpdateInvoiceAsync(
                new UpdatePaymentStatusRequest {InvoiceId = invoiceId, NewPaymentStatus = paymentStatus});
            if (serviceResult is null || !serviceResult.Succeeded)
                return new InvoiceServiceResult { Succeeded = false, Message = "Something went wrong" };
            
            return new InvoiceServiceResult { Succeeded = true };
        }
        catch (Exception ex)
        {
            return new InvoiceServiceResult { Succeeded = false, Message = ex.Message };
        }
    }

    public async Task<InvoiceServiceResult> DeleteInvoiceByInvoiceId(string invoiceId)
    {
        try
        {
            var serviceResult =
                await _invoiceServiceContractClient.DeleteInvoiceAsync(new DeleteInvoiceByIdRequest
                    { InvoiceId = invoiceId });
            if  (serviceResult is null || !serviceResult.Succeeded)
                return new InvoiceServiceResult { Succeeded = false, Message = "Something went wrong" };
            
            return new InvoiceServiceResult { Succeeded = true };
        }
        catch (Exception ex)
        {
            return new InvoiceServiceResult { Succeeded = false, Message = ex.Message };
        }
    }
}