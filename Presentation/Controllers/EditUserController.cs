using Authentication.Entities;
using Authentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.EditUser;
using UserProfileServiceProvider;
using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;


namespace Presentation.Controllers
{
    public class EditUserController(UserProfileServiceClient userProfileServiceClient, UserManager<AppUserEntity> userManager, AuthService authService) : Controller
    {
        private readonly UserProfileServiceClient _userProfileServiceClient = userProfileServiceClient;
        private readonly UserManager<AppUserEntity> _userManager = userManager;
        private readonly AuthService _authService = authService;

        [HttpGet("user/edit")]
        public async Task<IActionResult> Index()
        {
            //string userId = _userManager.GetUserId(User);
            //if (string.IsNullOrWhiteSpace(userId))
            //    return RedirectToAction("Index", "Home");

            //var userProfileReply =
            //    await _userProfileServiceClient.GetUserProfileByIdAsync(new RequestByUserId { UserId = userId });
            //if (userProfileReply is null)
            //    return RedirectToAction("Index", "Home");

            //var appUserProfileDto = AccountFactory.ToAppUserProfileDto(userProfileReply.Profile);
            //if (appUserProfileDto is null)
            //    return RedirectToAction("Index", "Home");

            //UpdateProfileInformationViewModel model = new()
            //{
            //    UserId = userId,
            //    FirstName = appUserProfileDto.FirstName,
            //    LastName = appUserProfileDto.LastName,
            //    Email = appUserProfileDto.Email,
            //    PhoneNumber = appUserProfileDto.PhoneNumber,
            //    Address = appUserProfileDto.Address,
            //    PostalCode = appUserProfileDto.PostalCode,
            //    City = appUserProfileDto.City
            //};

            //return View(model);
            return View();
        }

        [HttpPost("user/edit")]
        public async Task<IActionResult> Index(UpdateProfileInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Something went wrong, try again later.";
                return View(model);
            }

            UserProfile userProfile = new()
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                PostalCode = model.PostalCode,
                City = model.City
            };

            var updateResult = await _userProfileServiceClient.UpdateUserAsync(userProfile);
            if (updateResult.StatusCode != 200)
            {
                ViewBag.ErrorMessage = "Something went wrong, try again later.";
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("user/delete/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                ViewBag.DeleteMessage =
                    "Something went wrong. Could not delete account. Please contact customer service.";
                return RedirectToAction(nameof(Index));
            }

            //Remove account from IdentityDb
            var deleteFromIdentityResult = await _authService.DeleteAccountAsync(userId);
            if (!deleteFromIdentityResult.Succeeded)
            {
                ViewBag.DeleteMessage =
                    "Something went wrong. Could not delete account. Please contact customer service.";
                return RedirectToAction(nameof(Index));
            }

            var removeProfileResult =
                await _userProfileServiceClient.DeleteUserAsync(new RequestByUserId { UserId = userId });
            switch (removeProfileResult.StatusCode)
            {
                case 200:
                    //SignOut function???
                    return RedirectToAction("Index", "Login");
                case 404:
                    ViewBag.DeleteMessage = "Could not find profile to remove";
                    return RedirectToAction(nameof(Index));
                case 500: 
                    ViewBag.DeleteMessage = "Internal server error";
                    return RedirectToAction(nameof(Index));
               default:
                    ViewBag.DeleteMessage = "Bad request";
                    return RedirectToAction(nameof(Index));
            }
        }
    }
}