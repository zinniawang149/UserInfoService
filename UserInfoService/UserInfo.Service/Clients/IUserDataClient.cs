using UserInfoService.Models;

namespace UserInfoService.Services.Clients
{
    public interface IUserDataClient
    {
        Task<ServiceResult<List<User>>> GetUserInfoAsync();
    }
}
