using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoService.Models;

namespace UserInfoService.Services
{
    public interface IUserInfoService
    {
        Task<List<User>> GetUsersAsync(int id);
        Task<List<User>> GetUsersAsync();
    }
}
