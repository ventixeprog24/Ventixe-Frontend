using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("not-allowed")]
        public IActionResult NotAllowed()
        {
            return View();
        }
    }
}
