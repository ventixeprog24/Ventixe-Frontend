//using Application.Services;
//using Microsoft.AspNetCore.Mvc;
//using Presentation.Models.SignUp;
//using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;

//namespace Presentation.Controllers
//{
//    public class SignUpController(UserProfileServiceClient userProfileService, AccountService accountService) : Controller
//    {
//        private readonly UserProfileServiceClient _userProfileService = userProfileService;
//        private readonly AccountService _accountService = accountService;

//        #region Set email
//        [HttpGet("signup")]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost("signup")]
//        public async Task<IActionResult> Index(SignUpViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                ViewBag.ErrorMessage = "Invalid email address";
//                return View(model);
//            }

//            var doesUsernameExist = await _accountService.DoesUsernameExistAsync(model.Email);
//            if (doesUsernameExist.Succeeded)
//            {
//                ViewBag.ErrorMessage = "User already exists";
//                return View(model);
//            }

//            //This will send the verification code
//            var verificationResponse = await _verificationService.SendVerificationCodeAsync(model.Email);
//            if (!verificationResponse.Succeeded)
//            {
//                ViewBag.ErrorMessage = verificationResponse.Error;
//                return View(model);
//            }

//            TempData["Email"] = model.Email;
//            return RedirectToAction("AccountVerification");
//        }
//        #endregion Set email

//        #region Account verification
//        [HttpGet("account-verification")]
//        public IActionResult AccountVerification()
//        {
//            return View();
//        }

//        [HttpPost("account-verification")]
//        public async Task<IActionResult> AccountVerification(AccountVerificationViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var email = TempData["Email"].ToString();
//            if (string.IsNullOrWhiteSpace(email))
//                return RedirectToAction("Index");

//            var validateResponse = await _verificationService.ValidateCodeAsync(email, model.VerificationCode);
//            if (!validateResponse.Succeeded)
//            {
//                ViewBag.ErrorMessage = validateResponse.Error;
//                TempData.Keep("Email");
//                return View(model);
//            }

//            TempData["Email"] = email;
//            return RedirectToAction("SetPassword");
//        }
//        #endregion Account verification

//        #region Set Password

//        [HttpGet("set-password")]
//        public IActionResult SetPassword()
//        {
//            return View();
//        }

//        #endregion Set Password
//    }
//}
