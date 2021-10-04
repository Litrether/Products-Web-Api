using Entities;
using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Products;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UnitTestProducts;

namespace IntegrationTests
{
    public static class TestFixture
    {
        public static TestServer TestServer { get; }
        public static RepositoryContext Repository { get; }
        public static HttpClient Client { get; }
        private static RoleManager<IdentityRole> _roleManager;

        static TestFixture()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"testsettings.json");

            var webBuilder = new WebHostBuilder()
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            TestServer = new TestServer(webBuilder);
            Client = TestServer.CreateClient();
            Repository = TestServer.Host.Services.GetService(typeof(RepositoryContext)) as RepositoryContext;
            _userManager = TestServer.Host.Services.GetService(typeof(UserManager<User>)) as UserManager<User>;
            _roleManager = TestServer.Host.Services.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;

            FillingDatabase();
            SetToken();
        }

        public static void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }

        private static void SetToken()
        {
            var createUser = new UserRegistrationDto()
            {
                FirstName = "string",
                LastName = "string",
                UserName = "string2",
                Password = "string2",
                Email = "string@string.com",
                Roles = new string[] { "Administrator", "Manager", "User" }
            };

            var contentCreateUser = new StringContent(JsonConvert.SerializeObject(createUser), Encoding.UTF8, "application/json");
            var responseCreateUser = Client.PostAsync($"/api/account/", contentCreateUser).Result;

            var authUser = new UserValidationDto()
            {
                UserName = "string2",
                Password = "string2",
            };

            var contentAuth = new StringContent(JsonConvert.SerializeObject(authUser), Encoding.UTF8, "application/json");
            var responseAuth = Client.PostAsync($"/api/account/login", contentAuth).Result;
            var responseJson = responseAuth.Content.ReadAsStringAsync().Result;
            var token = JObject.Parse(responseJson).SelectToken("token").ToString();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private static void FillingDatabase()
        {
            _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            _roleManager.CreateAsync(new IdentityRole { Name = "Manager" });
            _roleManager.CreateAsync(new IdentityRole { Name = "Administrator" });

            Repository.Categories.AddRange(EntitiesForDb.Categories());
            Repository.Providers.AddRange(EntitiesForDb.Providers());
            Repository.Products.AddRange(EntitiesForDb.Products());
            Repository.SaveChanges();
        }
    }
}
