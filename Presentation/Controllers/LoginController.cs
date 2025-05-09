using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Login;


namespace Presentation.Controllers;

public class LoginController(AuthService authService) : Controller
{
    private readonly AuthService _authService = authService;

    [HttpGet("auth/login")]
    public IActionResult Index(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost("auth/login")]
    public async Task<IActionResult> Index(LoginViewModel model, string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;
        if (ModelState.IsValid)
        {
            var response = await _authService.LoginAsync(model.Email, model.Password, model.IsPersistent);
            if (response.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
        }

        ViewBag.ErrorMessage = "Invalid email or password";
        return View(model);

    }
}
