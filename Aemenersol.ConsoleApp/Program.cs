using Aemenersol.Api;
using Aemenersol.Api.Models;
using Aemenersol.Data.DataContext;
using Aemenersol.Data.Synchronizer;
using Aemnersol.Api.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;

namespace Aemenersol.ConsoleApp
{
    internal class Program
    {
        internal static IConfiguration Configuration;

        private static void Main(string[] args)
        {
            // Add appsettings as global Configuration
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args)
                .Build();

            // Initialize Api Settings and DbContext
            var apiSettings = Configuration.GetSection("AemenersoApiSettings").Get<ApiSettings>();
            AemenersolApi.Setup(apiSettings);
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            ApplicationDbContext dbContext = new ApplicationDbContext(builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).Options);

            // Get the bearer token for the user
            var model = GetUsernamePassword();
            var authEndpoint = AemenersolApi.GetAuthEndpoint();
            var accessToken = authEndpoint.GetAccessToken(model.Username, model.Password);

            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("Unauthorized");
                return;
            }

            AemenersolApi.SetBearerToken(accessToken);

            // Get the Platform Well Endpoint API Library and get the platform wells
            var platformWellEndpoint = AemenersolApi.GetPlatformWellEndpoint();
            var platFormWells = platformWellEndpoint.GetPlatformWells(true);

            //Start the Platform Well Synchonization process
            PlatformWellSynchronizer synchronizer = new PlatformWellSynchronizer(dbContext);
            synchronizer.Sync(platFormWells);

            Console.WriteLine("Synchronization Finished");
            Console.ReadLine();
        }

        private static GetBearerTokenRequest GetUsernamePassword()
        {
            Console.WriteLine("~~~~~~Aem Enersol Synchronization~~~~~~~~~~");

            //Prompt the username
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();

            Console.WriteLine("Password: ");
            var password = Console.ReadLine();

            return new GetBearerTokenRequest() { Username = username, Password = password };
        }
    }
}