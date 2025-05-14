using Authentication.Interfaces;
using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using Presentation.Models.SignUp;
using UserProfileServiceProvider;
using VerificationServiceProvider;
using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;
using VerificationServiceClient = VerificationServiceProvider.VerificationContract.VerificationContractClient;

namespace Presentation.Controllers
{
    public class SignUpController(UserProfileServiceClient userProfileService, VerificationServiceClient verificationServiceClient, IAuthService authService) : Controller
    {
        private readonly UserProfileServiceClient _userProfileService = userProfileService;
        private readonly VerificationServiceClient _verificationService = verificationServiceClient;
        private readonly IAuthService _authService = authService;

        #region Set email
        [HttpGet("auth/signup")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("auth/signup")]
        public async Task<IActionResult> Index(SetEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var doesUsernameExist = await _authService.DoesUsernameExistAsync(model.Email);
            if (doesUsernameExist.Succeeded)
            {
                ViewBag.ErrorMessage = "User already exists";
                return View(model);
            } 

            //This will send the verification code
            var verificationResponse =await _verificationService.SendVerificationCodeAsync(
                new SendVerificationCodeRequest { Email = model.Email });
            if (!verificationResponse.Succeeded)
            {
                ViewBag.ErrorMessage = verificationResponse.Error;
                return View(model);
            }

            TempData["Email"] = model.Email;
            return RedirectToAction(nameof(AccountVerification));
        }
        #endregion Set email

        #region Account verification
        [HttpGet("auth/account-verification")]
        public IActionResult AccountVerification(string? email, string? token)
        {
            var tempEmail = TempData["Email"]?.ToString();
            bool emailInQuery = !string.IsNullOrWhiteSpace(email);
            bool tokenInQuery = !string.IsNullOrWhiteSpace(token);

            if (!emailInQuery && !string.IsNullOrWhiteSpace(tempEmail))
                email = tempEmail;
            else if (!emailInQuery && !tokenInQuery)
                return RedirectToAction(nameof(Index));

            if (tokenInQuery)
            {
                var validateResponse = _verificationService.ValidateVerificationToken(
                    new ValidateVerificationTokenRequest { Token = token });
                if (!validateResponse.Succeeded)
                {
                    ViewBag.ErrorMessage = "Invalid or expired verification link";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Email"] = email;
            }

            ViewBag.Email = EmailFormatter.MaskEmail(email!);
            TempData.Keep("Email");

            return View();
        }

        [HttpPost("auth/account-verification")]
        public IActionResult AccountVerification(AccountVerificationViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.VerificationCode))
            {
                ViewBag.ErrorMessage = "Invalid verification code.";
                TempData.Keep("Email");
                return View(model);
            }
                
            var email = TempData["Email"]?.ToString();
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction(nameof(Index));

            var validateResponse = _verificationService.ValidateVerificationCode(
                new ValidateVerificationCodeRequest { Email = email, Code = model.VerificationCode });
            if (!validateResponse.Succeeded)
            {
                ViewBag.ErrorMessage = validateResponse.Error;
                TempData.Keep("Email");
                return View(model);
            }

            TempData["Email"] = email;
            return RedirectToAction(nameof(SetPassword));
        }
        #endregion Account verification

        #region Set Password
        [HttpGet("auth/set-password")]
        public IActionResult SetPassword()
        {
            return View();
        }

        [HttpPost("auth/set-password")]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData.Keep("Email");
                return View(model);
            }

            var email = TempData["Email"]?.ToString();
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction(nameof(Index));

            var result = await _authService.SignUpAsync(email, model.Password);
            if (!result.Succeeded)
            {
                TempData.Keep("Email");
                return View(model);
            }

            TempData.Keep("Email");
            TempData["UserId"] = result.UserId;
            return RedirectToAction(nameof(ProfileInformation));
        }
        #endregion Set Password

        #region Set Profile Information
        [HttpGet("auth/profile-information")]
        public IActionResult ProfileInformation()
        {
            return View();
        }

        [HttpPost("auth/profile-information")]
        public async Task<IActionResult> ProfileInformation(SetProfileInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData.Keep("Email");
                TempData.Keep("UserId");
                return View(model);
            }

            var email = TempData["Email"]?.ToString();
            var userId = TempData["UserId"]?.ToString();

            UserProfile userProfile = new()
            {
                UserId = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PostalCode = model.PostalCode,
                City = model.City
            };

            var result = await _userProfileService.CreateUserProfileAsync(userProfile);
            switch (result.StatusCode)
            {
                case 201:
                    return RedirectToAction("Index", "Login");
                case 500:
                    ViewBag.ErrorMessage = "Internal server error.";
                    TempData.Keep("Email");
                    TempData.Keep("UserId");
                    return View(model);
                default:
                    ViewBag.ErrorMessage = "Bad Request. Try again.";
                    TempData.Keep("Email");
                    TempData.Keep("UserId");
                    return View(model);
            }
        }
        #endregion Set Profile Information
    }
}
