using System.Net.NetworkInformation;
using Application.Dtos;
using Authentication.Entities;

namespace Application.Factories
{
    public class AccountFactory
    {
        public static AppUserEntity? ToEntity(SignUpFormDto formData)
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
    }
}
