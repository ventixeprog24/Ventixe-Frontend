using Authentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers
{
    public class RoleHandler(UserManager<AppUserEntity> userManager)
    {
        private readonly UserManager<AppUserEntity> _userManager = userManager;

        public async Task<IdentityResult> AddToRoleAsync(AppUserEntity appUser, string role)
        {
            var result = await _userManager.AddToRoleAsync(appUser, role);
            return result;
        }
    }
}
