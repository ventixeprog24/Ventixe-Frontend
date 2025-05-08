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
                Email = email,
                EmailConfirmed = true
            };

            return appUser;
        }

        public static AppUserProfileDto? ToAppUserProfileDto(UserProfile userProfile)
        {
            if (userProfile is null)
                return null;

            AppUserProfileDto appUserProfileDto = new()
            {
                UserId = userProfile.UserId,
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

        public static HeaderUserProfileDto? ToHeaderUserProfileDto(UserProfile userProfile)
        {
            if (userProfile is null)
                return null;

            HeaderUserProfileDto headerViewModel = new()
            {
                UserId = userProfile.UserId,
                FullName = $"{userProfile.FirstName} {userProfile.LastName}"
            };

            return headerViewModel;
        }
    }
}
