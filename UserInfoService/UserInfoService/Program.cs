using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using UserInfoService.Models;
using UserInfoService.Services.Clients;

namespace UserInfoService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddHttpClient()
                .AddTransient<IUserHttpClient, UserHttpClient>()
                .BuildServiceProvider();

            // Setup variable
            if (Environment.GetEnvironmentVariable("UserInfoEndpoint") == null) { 
                Environment.SetEnvironmentVariable("UserInfoEndpoint", "https://f43qgubfhf.execute-api.ap-southeast-2.amazonaws.com/");
            }

            //Actual work
            var userHttpClient = serviceProvider.GetService<IUserHttpClient>();
            var users = userHttpClient.GetUserInfoAsync().Result.Result;

            // Get User full names by Id
            var userHasId_42 = users.Where(u => u.Id == 42);
            foreach (var user in userHasId_42)
            {
                Console.WriteLine($"{user.First} {user.Last}");
            }

            // Get Users first names by Age
            var userIsAge_23 = users.Where(u => u.Age == 23);

            var stringBuilder = new StringBuilder();
            foreach (var user in userIsAge_23) {
                stringBuilder.Append($"{user.First},");
            }
            if (stringBuilder.Length > 0) stringBuilder.Remove(stringBuilder.Length-1, 1); //Remove the last comma
            Console.WriteLine(stringBuilder.ToString());

            // Get users group by age and gender
            var usersGroupByAge = users.OrderBy(u => u.Age).ToLookup(u => u.Age);
            foreach (var userGroup in usersGroupByAge)
            {
                var usersByGender = userGroup.ToLookup(u => u.Gender);
                Console.WriteLine($"Age:{userGroup.Key} Female:{usersByGender[Gender.F].Count()} Male: {usersByGender[Gender.M].Count()}");
            }
        }
    }
}