using Authentication.Entities;
using Authentication.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.EditUser;
using UserProfileServiceProvider;
using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;


namespace Presentation.Controllers
{
    public class EditUserController(UserProfileServiceClient userProfileServiceClient, UserManager<AppUserEntity> userManager) : Controller
    {
        private readonly UserProfileServiceClient _userProfileServiceClient = userProfileServiceClient;
        private readonly UserManager<AppUserEntity> _userManager = userManager;

        [HttpGet("user/edit")]
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Index", "Home");

            var userProfileReply =
                await _userProfileServiceClient.GetUserProfileByIdAsync(new RequestByUserId { UserId = userId });
            if (userProfileReply is null)
                return RedirectToAction("Index", "Home");

            var appUserProfileDto = AccountFactory.ToAppUserProfileDto(userProfileReply.Profile);
            if (appUserProfileDto is null)
                return RedirectToAction("Index", "Home");

            UpdateProfileInformationViewModel model = new()
            {
                UserId = userId,
                FirstName = appUserProfileDto.FirstName,
                LastName = appUserProfileDto.LastName,
                Email = appUserProfileDto.Email,
                PhoneNumber = appUserProfileDto.PhoneNumber,
                Address = appUserProfileDto.Address,
                PostalCode = appUserProfileDto.PostalCode,
                City = appUserProfileDto.City
            };

            return View(model);
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
    }
}