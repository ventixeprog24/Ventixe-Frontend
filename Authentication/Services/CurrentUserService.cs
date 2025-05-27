using Authentication.Dtos;
using Authentication.Factories;
using Authentication.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UserProfileServiceProvider;
using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;


namespace Authentication.Services
{
    public class CurrentUserService(IHttpContextAccessor accessor, UserProfileServiceClient userProfileService) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly UserProfileServiceClient _userProfileService = userProfileService;

        public async Task<HeaderUserProfileDto> GetHeaderViewModelAsync()
        {
            HeaderUserProfileDto headerViewModel = new()
            {
                UserId = string.Empty,
                FullName = ""
            };

            var httpContext = _accessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(userId))
            {
                var userProfilereply =
                    await _userProfileService.GetUserProfileByIdAsync(new RequestByUserId { UserId = userId });

                if (userProfilereply.StatusCode != 200) 
                    return headerViewModel;

                headerViewModel = AccountFactory.ToHeaderUserProfileDto(userProfilereply.Profile);
                if (headerViewModel is not null) 
                    return headerViewModel;
            }

            return headerViewModel;
        }
    }
}
