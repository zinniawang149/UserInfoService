using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;
using UserInfoService.Models;
using UserInfoService.Services;
using UserInfoService.Services.Clients;

namespace UserInfoService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //setup DI
                var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddHttpClient()
                    .AddTransient<IUserDataClient, UserHttpClient>()
                    .AddTransient<IUserPrinterSvc, UserPrinterSvc>()
                    .AddSingleton<IPrinter, ConsolePrinter>()
                    .BuildServiceProvider();

                //Get users
                var userDataClient = serviceProvider.GetService<IUserDataClient>();
                var users = userDataClient.GetUserInfoAsync().Result.Result;

                //Print user info
                var userPrinterSvc = serviceProvider.GetService<IUserPrinterSvc>();
                userPrinterSvc.PrintUserFullNamesById(users, 42);
                userPrinterSvc.PrintUsersfirstNameByAge(users, 23);
                userPrinterSvc.PrintUsersGenderByAge(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Only do this because it's a console test app, we can write it in logs file in real world
            }
            
        }
    }
}