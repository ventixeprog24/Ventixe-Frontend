﻿using Authentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers
{
    //Gör interface för detta
    public class RoleHandler(UserManager<AppUserEntity> userManager)
    {
        private readonly UserManager<AppUserEntity> _userManager = userManager;

        public async Task<IdentityResult> AddToRoleAsync(AppUserEntity appUser, string role)
        {
            var result = await _userManager.AddToRoleAsync(appUser, role);
            return result;
        }

        public async Task<string?> GetRoleAsync(AppUserEntity appUser)
        {
            var roleList = await _userManager.GetRolesAsync(appUser);
            if (roleList.Count > 1)
                return null;

            return roleList.FirstOrDefault();
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

        public async Task<IdentityResult?> RemoveFromRoleAsync(AppUserEntity appUser, string role)
        {
            var removeResult = await _userManager.RemoveFromRoleAsync(appUser, role);
            if (removeResult.Succeeded)
                return removeResult;

            return null;
        }
    }
}
