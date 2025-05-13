using Authentication.Dtos;
using Authentication.Factories;
using Authentication.Handlers;
using Authentication.Entities;
using Authentication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Services
{
    //Gör interface för detta.
    public class AuthService(UserManager<AppUserEntity> userManager, RoleHandler roleHandler, SignInManager<AppUserEntity> signInManager) : IAuthService
    {
        private readonly UserManager<AppUserEntity> _userManager = userManager;
        private readonly SignInManager<AppUserEntity> _signInManager = signInManager;
        private readonly RoleHandler _roleHandler = roleHandler;

        public async Task<AuthServiceResult> DoesUsernameExistAsync(string email)
        {

            var doesUsernameExist = await _userManager.Users.AnyAsync(u => u.UserName == email);
            return doesUsernameExist
                ? new AuthServiceResult { Succeeded = true }
                : new AuthServiceResult { Succeeded = false };
        }

        public async Task<AuthServiceResult> SignUpAsync(string email, string password)
        {
            var appUser = AccountFactory.ToAppUserEntity(email);

            if (appUser is null)
                return new AuthServiceResult { Succeeded = false, Message = "Could not convert to AppUserEntity" };

            var result = await _userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
                return new AuthServiceResult { Succeeded = false, Message = "Could not create user" };

            var roleResult = await _roleHandler.AddToRoleAsync(appUser, "User");
            return !result.Succeeded
                ? new AuthServiceResult { Succeeded = false, Message = "Could not add user to role" }
                : new AuthServiceResult { Succeeded = true, UserId = appUser.Id };
        }

        public async Task<AuthServiceResult> DeleteAccountAsync(string userId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userId);
            if (userToDelete is null)
                return new AuthServiceResult { Succeeded = false, Message = "Could not find user to delete"};

            var roles = await _userManager.GetRolesAsync(userToDelete);
            if (!roles.Any())
                return new AuthServiceResult { Succeeded = false, Message = "User isn't assigned to any roles" };

            var removeFromRolesResult = await _userManager.RemoveFromRolesAsync(userToDelete, roles);
            if (!removeFromRolesResult.Succeeded)
                return new AuthServiceResult { Succeeded = false, Message = "Couldn't remove user from roles" };

            var deleteUserResult = await _userManager.DeleteAsync(userToDelete);
            if (!deleteUserResult.Succeeded)
                return new AuthServiceResult { Succeeded = false, Message = "Couldn't remove user" };

            return new AuthServiceResult { Succeeded = true, Message = "Successfully deleted user credentials" };
        }
        public async Task<AuthServiceResult> LoginAsync(string email, string password, bool isPersistent)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent, false);
            return result.Succeeded
                ? new AuthServiceResult { Succeeded = true, Message = "Succesfully signed in." }
                : new AuthServiceResult { Succeeded = false, Message = "Invalid email or password." };
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
} 
