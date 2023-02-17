using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfoService.Models;
using UserInfoService.Services.Clients;

namespace UserInfoService.Services
{
    public class UserPrinterSvc : IUserPrinterSvc
    {
        private IPrinter _printer;

        public UserPrinterSvc(IPrinter printer)
        {
            _printer = printer;
        }

        public void PrintUserFullNamesById(List<User> users, int id)
        {
            var userHasId_42 = users.Where(u => u.Id == 42);
            foreach (var user in userHasId_42)
            {
                _printer.Print($"{user.First} {user.Last}");
            }
        }

        public void PrintUsersfirstNameByAge(List<User> users, int age) {
            
            var userIsAge_23 = users.Where(u => u.Age == 23);

            var stringBuilder = new StringBuilder();
            foreach (var user in userIsAge_23)
            {
                stringBuilder.Append($"{user.First},");
            }
            if (stringBuilder.Length > 0) stringBuilder.Remove(stringBuilder.Length - 1, 1); //Remove the last comma
            _printer.Print(stringBuilder.ToString());
        }

        public void PrintUsersGenderByAge(List<User> users)
        {
            var usersGroupByAge = users.OrderBy(u => u.Age).ToLookup(u => u.Age);
            foreach (var userGroup in usersGroupByAge)
            {
                var usersByGender = userGroup.ToLookup(u => u.Gender);
                _printer.Print($"Age:{userGroup.Key} Female:{usersByGender[Gender.F].Count()} Male: {usersByGender[Gender.M].Count()}");
            }

        }

    }
}
