using Authentication.Dtos;
using Authentication.Entities;
using UserProfileServiceProvider;

namespace Authentication.Factories
{
    public class AccountFactory
    {
        public static AppUserEntity? ToAppUserEntity(SignUpFormDto formData)
        {
            if (formData is null)
                return null;

            AppUserEntity appUser = new()
            {
                UserName = formData.Email,
                Email = formData.Email
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
