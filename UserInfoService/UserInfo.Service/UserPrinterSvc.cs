using UserInfoService.Models;

namespace UserInfoService.Services
{
    public class UserPrinterSvc : IUserPrinterSvc
    {
        private IPrinter _printer;

        public UserPrinterSvc(IPrinter printer)
        {
            _printer = printer;
        }

        public void PrintUsersFullNames(List<User> users, Func<User, bool> predicate)
        {
            var usersCollection = users.Where(predicate).Select(u=> $"{u.First} {u.Last}");
            _printer.PrintByDelimiter(usersCollection, '\n');
            
        }
        public void PrintUsersFirstNames(List<User> users, Func<User, bool> predicate) {
            
            var usersCollection = users.Where(predicate).Select(u=>$"{u.First}");
            _printer.PrintByDelimiter(usersCollection, ',');
        }

        public void PrintUsersStats(List<User> users)
        {
            var ageGenderTable = new Dictionary<int, int[]>();
            foreach (var user in users)
            {
                if (ageGenderTable.ContainsKey(user.Age))
                {
                    var genderArray = ageGenderTable[user.Age];
                    switch (user.Gender)
                    {
                        case Gender.M:
                            genderArray[1] = genderArray[1]+1;
                            break;
                        case Gender.F:
                            genderArray[0] = genderArray[0]+1;
                            break;
                        case Gender.Unknown:
                            break;
                        default:
                            break;
                    }
                }
                else {              
                    //                           index 0: Female, index 1: Male
                    var genderArray = new int[] { 0, 0 };
                    switch (user.Gender)
                    {
                        case Gender.M:
                            genderArray[1] = 1;
                            ageGenderTable.Add(user.Age, genderArray);
                            break;
                        case Gender.F:
                            genderArray[0] = 1;
                            ageGenderTable.Add(user.Age, genderArray);
                            break;
                        case Gender.Unknown:
                            break;
                        default:
                            break;
                    }
                }
            }
            var usersCollection = ageGenderTable.OrderBy(t => t.Key)
                .Select(t=> $"Age:{t.Key} Female:{t.Value[0]} Male:{t.Value[1]}");
            _printer.PrintByDelimiter(usersCollection, '\n');
        }

    }
}
