using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserInfoService.Models;

namespace UserInfoService.Services
{
    public interface IUserPrinterSvc
    {
        public void PrintUsersFullNames(List<User> users, Func<User, bool> predicate);
        public void PrintUsersFirstNames(List<User> users, Func<User, bool> predicate);
        public void PrintUsersStats(List<User> users);
        
    }
}
