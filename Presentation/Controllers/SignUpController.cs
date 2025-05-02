using Microsoft.AspNetCore.Mvc;
using UserProfileServiceClient = UserProfileService.UserProfileService.UserProfileServiceClient;

namespace Presentation.Controllers
{
    public class SignUpController(UserProfileServiceClient userProfileService) : Controller
    {
        private readonly UserProfileServiceClient _userProfileService = userProfileService;

        public IActionResult Index()
        {
            return View();
        }
    }
}
