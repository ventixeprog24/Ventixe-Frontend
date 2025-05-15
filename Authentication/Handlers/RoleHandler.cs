using Authentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Handlers
{
    //Gör interface för detta
    public class RoleHandler(UserManager<AppUserEntity> userManager, RoleManager<IdentityRole> roleManager)
    {
        private readonly UserManager<AppUserEntity> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<IdentityResult> AddToRoleAsync(AppUserEntity appUser, string role)
        {
            var doesRoleExist = _roleManager.Roles.Select(role => role.Name).ToList();
            var roleCheck = doesRoleExist.Any(r => r == role);
            if (!roleCheck)
                return IdentityResult.Failed();
            
            var result = await _userManager.AddToRoleAsync(appUser, role);
            return result;
        }
        
        public async Task<IdentityResult?> RemoveFromRoleAsync(AppUserEntity appUser)
        {
            var roleResult = await GetRoleAsync(appUser);
            if (roleResult is null)
                return null;

            var removeResult = await _userManager.RemoveFromRoleAsync(appUser, roleResult);
            if (removeResult.Succeeded)
                return removeResult;

            return null;
        }
        
        public async Task<string?> GetRoleAsync(AppUserEntity appUser)
        {
            var roleList = await _userManager.GetRolesAsync(appUser);
            if (roleList.Count > 1)
                return null;

            return roleList.FirstOrDefault();
        }

        public async Task<IdentityResult?> RemoveFromRoleAsync(AppUserEntity appUser, string role)
        {
            var doesRoleExist = _roleManager.Roles.Select(role => role.Name).ToList();
            var roleCheck = doesRoleExist.Any(r => r == role);
            if (!roleCheck)
                return null;
            
            var removeResult = await _userManager.RemoveFromRoleAsync(appUser, role);
            if (removeResult.Succeeded)
                return removeResult;

            return null;
        }
    }
}
