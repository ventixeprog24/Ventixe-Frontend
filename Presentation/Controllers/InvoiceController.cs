using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

namespace Presentation.Controllers
{
    public class InvoiceController(InvoiceService invoiceService) : Controller
    {
        private readonly InvoiceService _invoiceService = invoiceService;
        
        [Route("home/invoices")]
        public async Task<IActionResult> Index()
        {
            var getInvoicesReply = await _invoiceService.GetAllInvoices();
            if (!getInvoicesReply.Succeeded)
                return RedirectToAction("Index",  "Home");
            
            return View(getInvoicesReply.AllInvoices);
        }
    }
}
