using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoService.Models;

namespace UserInfoService.Services.Clients
{
    public interface IUserDataClient
    {
        Task<ServiceResult<List<User>>> GetUserInfoAsync();
    }
}
