using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class InvoiceController : Controller
    {
        [Route("Home/invoices")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Invoices";
            return View();
        }
    }
}
