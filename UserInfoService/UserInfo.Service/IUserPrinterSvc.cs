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
        public void PrintUserFullNamesById(List<User> users, int id);
        public void PrintUsersfirstNameByAge(List<User> users, int age);
        public void PrintUsersGenderByAge(List<User> users);
        
    }
}
