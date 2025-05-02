using Application.Dtos;
using Application.Factories;
using Application.Handlers;
using Authentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AccountService(UserManager<AppUserEntity> userManager, RoleHandler roleHandler)
    {
        private readonly UserManager<AppUserEntity> _userManager = userManager;
        private readonly RoleHandler _roleHandler = roleHandler;

        public async Task<ServiceResult> SignUpAsync(SignUpFormDto formData)
        {
            var appUser = AccountFactory.ToEntity(formData);

            if (appUser is null)
                return new ServiceResult { Succeeded = false, Message = "Could not convert to AppUserEntity"};

            var result = await _userManager.CreateAsync(appUser, formData.Password);
            if (!result.Succeeded)
                return new ServiceResult { Succeeded = false, Message = "Could not create user" };

            var roleResult = await _roleHandler.AddToRoleAsync(appUser, formData.Role);
            return !result.Succeeded 
                ? new ServiceResult { Succeeded = false, Message = "Could not add user to role" } 
                : new ServiceResult { Succeeded = true, UserId = appUser.Id };
        }
    }
}
