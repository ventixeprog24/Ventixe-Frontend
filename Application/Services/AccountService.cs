using Application.Dtos;
using Application.Factories;
using Application.Handlers;
using Authentication.Contexts;
using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AccountService(IdentityUserDbContext context, UserManager<AppUserEntity> userManager, RoleHandler roleHandler)
    {
        private readonly IdentityUserDbContext _context = context;
        private readonly UserManager<AppUserEntity> _userManager = userManager;
        private readonly RoleHandler _roleHandler = roleHandler;

        public async Task<AccountServiceResult> DoesUsernameExistAsync(string email)
        {
            var doesUsernameExist = await _context.Users.AnyAsync(u => u.UserName == email);
            return doesUsernameExist
                ? new AccountServiceResult { Succeeded = true }
                : new AccountServiceResult { Succeeded = false };
        }

        public async Task<AccountServiceResult> SignUpAsync(SignUpFormDto formData)
        {
            var appUser = AccountFactory.ToEntity(formData);

            if (appUser is null)
                return new AccountServiceResult { Succeeded = false, Message = "Could not convert to AppUserEntity"};

            var result = await _userManager.CreateAsync(appUser, formData.Password);
            if (!result.Succeeded)
                return new AccountServiceResult { Succeeded = false, Message = "Could not create user" };

            var roleResult = await _roleHandler.AddToRoleAsync(appUser, formData.Role);
            return !result.Succeeded 
                ? new AccountServiceResult { Succeeded = false, Message = "Could not add user to role" } 
                : new AccountServiceResult { Succeeded = true, UserId = appUser.Id };
        }
    }
}
