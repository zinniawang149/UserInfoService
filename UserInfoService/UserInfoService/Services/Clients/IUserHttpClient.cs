using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoService.Models;

namespace UserInfoService.Services.Clients
{
    public interface IUserHttpClient
    {
        Task<HttpServiceResult<List<User>>> GetUserInfoAsync();
    }
}
