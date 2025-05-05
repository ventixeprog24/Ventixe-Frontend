using Authentication.Dtos;
using Authentication.Entities;
using UserProfileServiceProvider;

namespace Authentication.Factories
{
    public class AccountFactory
    {
        public static AppUserEntity? ToAppUserEntity(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            AppUserEntity appUser = new()
            {
                UserName = email,
                Email = email
            };

            return appUser;
        }

        public static AppUserProfileDto? ToAppUserProfileDto(UserProfile userProfile, string role)
        {
            if (userProfile is null || string.IsNullOrWhiteSpace(role))
                return null;

            AppUserProfileDto appUserProfileDto = new()
            {
                UserId = userProfile.UserId,
                Role = role,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Email = userProfile.Email,
                PhoneNumber = userProfile.PhoneNumber,
                Address = userProfile.Address,
                PostalCode = userProfile.PostalCode,
                City = userProfile.City
            };

            return appUserProfileDto;
        }
    }
}
