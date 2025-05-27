using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("unauthorized")]
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}
