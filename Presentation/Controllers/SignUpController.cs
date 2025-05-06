using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using Presentation.Models.SignUp;
using VerificationServiceProvider;
using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;
using VerificationServiceClient = VerificationServiceProvider.VerificationContract.VerificationContractClient;

namespace Presentation.Controllers
{
    public class SignUpController(UserProfileServiceClient userProfileService, VerificationServiceClient verificationServiceClient, AuthService authService) : Controller
    {
        private readonly UserProfileServiceClient _userProfileService = userProfileService;
        private readonly VerificationServiceClient _verificationService = verificationServiceClient;
        private readonly AuthService _authService = authService;

        #region Set email
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

            var doesUsernameExist = await _authService.DoesUsernameExistAsync(model.Email);
            if (doesUsernameExist.Succeeded)
            {
                ViewBag.ErrorMessage = "User already exists";
                return View(model);
            }

            //This will send the verification code
            var verificationResponse = await _verificationService.SendVerificationCodeAsync(
                new SendVerificationCodeRequest { Email = model.Email });
            if (!verificationResponse.Succeeded)
            {
                ViewBag.ErrorMessage = verificationResponse.Error;
                return View(model);
            }

            TempData["Email"] = model.Email;
            return RedirectToAction("AccountVerification");
        }
        #endregion Set email

        #region Account verification
        [HttpGet("account-verification")]
        public IActionResult AccountVerification(string? email, string? token)
        {
            var tempEmail = TempData["Email"]?.ToString();
            bool emailInQuery = !string.IsNullOrWhiteSpace(email);
            bool tokenInQuery = !string.IsNullOrWhiteSpace(token);

            if (!emailInQuery && !string.IsNullOrWhiteSpace(tempEmail))
                email = tempEmail;

            if (!emailInQuery && !tokenInQuery)
                return RedirectToAction("Index");

            if (tokenInQuery)
            {
                var validateResponse = _verificationService.ValidateVerificationToken(
                    new ValidateVerificationTokenRequest { Email = email, Token = token });
                if (!validateResponse.Succeeded)
                {
                    ViewBag.ErrorMessage = "Invalid or expired verification link";
                    return RedirectToAction("Index");
                }

                TempData["Email"] = email;
            }

            ViewBag.Email = EmailFormatter.MaskEmail(email!);
            TempData.Keep("Email");

            return View();
        }

        [HttpPost("account-verification")]
        public IActionResult AccountVerification(AccountVerificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Invalid or expired verification code.";
                return View(model);
            }

            var email = TempData["Email"]?.ToString();
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction("Index");

            var validateResponse = _verificationService.ValidateVerificationCode(
                new ValidateVerificationCodeRequest { Email = email, Code = model.VerificationCode });
            if (!validateResponse.Succeeded)
            {
                ViewBag.ErrorMessage = validateResponse.Error;
                TempData.Keep("Email");
                return View(model);
            }

            TempData["Email"] = email;
            return RedirectToAction("SetPassword");
        }

        #endregion Account verification

        #region Set Password

        [HttpGet("set-password")]
        public IActionResult SetPassword()
        {
            return View();
        }

        #endregion Set Password
    }
}