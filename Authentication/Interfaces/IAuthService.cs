using Authentication.Dtos;

namespace Authentication.Interfaces;

public interface IAuthService
{
    Task<AuthServiceResult> DoesUsernameExistAsync(string email);
    Task<AuthServiceResult> SignUpAsync(string email, string password);
    Task<AuthServiceResult> DeleteAccountAsync(string userId);
    Task<AuthServiceResult> LoginAsync(string email, string password, bool isPersistent);
    Task LogOutAsync();
}