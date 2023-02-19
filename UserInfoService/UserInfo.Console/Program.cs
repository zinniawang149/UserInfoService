using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using System.Reflection;
using System.Text;
using UserInfoService.Models;
using UserInfoService.Services;
using UserInfoService.Services.Clients;

namespace UserInfoService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var logger = LoggerFactory.Create(config =>
            {
                config.AddConsole();
            }).CreateLogger("Main");
            try
            {
                logger.LogInformation($"------Service Start------");
                var host = CreateHostBuilder(args).Build();
                
                //Get users
                var userDataClient = host.Services.GetService<IUserDataClient>();

                var response = await userDataClient.GetUserInfoAsync();
                if (!response.IsSuccess)
                {
                    throw new Exception($"{response.Error.Code} { response.Error.Message}");
                }

                var users = response.Result;

                //Print user info
                var userPrinterSvc = host.Services.GetService<IUserPrinterSvc>();
                userPrinterSvc.PrintUsersFullNames(users, u => u.Id == 31);
                userPrinterSvc.PrintUsersFirstNames(users, u => u.Age == 23);
                userPrinterSvc.PrintUsersStats(users);
                
            }
            catch (Exception ex)
            {
                logger.LogError($"Errors:{ex.Message}");
            }
            finally
            {
                logger.LogInformation($"------Service End------");
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddHttpClient("Default")
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.WaitAndRetryAsync(3, retryAttempt =>
                    {
                        Random jitterer = new Random();
                        return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 1000));
                    }
                    , onRetryAsync: async (outcome, timespan, retryCount, context) =>
                    {
                        Console.WriteLine($"Retrying...{retryCount}");
                    }))
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)))
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(5));

            services.AddTransient<IUserDataClient, UserHttpClient>()
                .AddTransient<IUserPrinterSvc, UserPrinterSvc>()
                .AddSingleton<IPrinter, ConsolePrinter>();
        })
        .ConfigureLogging((_, logging) =>
        {
            logging.ClearProviders();
            logging.AddSimpleConsole(options => options.IncludeScopes = true);
        });
    }
}