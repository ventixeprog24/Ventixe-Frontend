using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.SignUp;
using UserProfileServiceClient = UserProfileService.UserProfileService.UserProfileServiceClient;

namespace Presentation.Controllers
{
    public class SignUpController(UserProfileServiceClient userProfileService, AccountService accountService) : Controller
    {
        private readonly UserProfileServiceClient _userProfileService = userProfileService;
        private readonly AccountService _accountService = accountService;

        [HttpGet("signup")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Index(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Invalid email address";
                return View(model);
            }

            var doesUsernameExist = await _accountService.DoesUsernameExistAsync(model.Email);
            if (doesUsernameExist.Succeeded)
            {
                ViewBag.ErrorMessage = "User already exists";
                return View(model);
            }

            //This will send the verification code
            var verificationResponse = await _verificationService.SendVerificationCodeAsync(model.Email);
            if (!verificationResponse.Succeeded)
            {
                ViewBag.ErrorMessage = verificationResponse.Error;
                return View(model);
            }

            TempData["Email"] = model.Email;
            return RedirectToAction("AccountVerification");
        }
    }
}
