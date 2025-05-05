using Authentication.Dtos;
using Authentication.Factories;
using Authentication.Handlers;
using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Services
{
    //Gör interface för detta.
    public class AuthService(UserManager<AppUserEntity> userManager, RoleHandler roleHandler)
    {
        private readonly UserManager<AppUserEntity> _userManager = userManager;
        private readonly RoleHandler _roleHandler = roleHandler;

        public async Task<AuthServiceResult> DoesUsernameExistAsync(string email)
        {

            var doesUsernameExist = await _userManager.Users.AnyAsync(u => u.UserName == email);
            return doesUsernameExist
                ? new AuthServiceResult { Succeeded = true }
                : new AuthServiceResult { Succeeded = false };
        }

        public async Task<AuthServiceResult> SignUpAsync(SignUpFormDto formData)
        {
            var appUser = AccountFactory.ToAppUserEntity(formData);

            if (appUser is null)
                return new AuthServiceResult { Succeeded = false, Message = "Could not convert to AppUserEntity"};

            var result = await _userManager.CreateAsync(appUser, formData.Password);
            if (!result.Succeeded)
                return new AuthServiceResult { Succeeded = false, Message = "Could not create user" };

            var roleResult = await _roleHandler.AddToRoleAsync(appUser, formData.Role);
            return !result.Succeeded 
                ? new AuthServiceResult { Succeeded = false, Message = "Could not add user to role" } 
                : new AuthServiceResult { Succeeded = true, UserId = appUser.Id };
        }
    }
}
