using Authentication.Dtos;

namespace Authentication.Interfaces;

public interface ICurrentUserService
{
    Task<UserHeaderProfileDto> GetHeaderViewModelAsync();
}