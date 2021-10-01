using Entities;
using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Products;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UnitTestProducts;

namespace IntegrationTests
{
    public class BaseTestServerFixture
    {
        public TestServer TestServer { get; }
        public RepositoryContext DbContext { get; }
        public HttpClient Client { get; }
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public BaseTestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
            DbContext = TestServer.Host.Services.GetService(typeof(RepositoryContext)) as RepositoryContext;
            _userManager = TestServer.Host.Services.GetService(typeof(UserManager<User>)) as UserManager<User>;
            _roleManager = TestServer.Host.Services.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;

            _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            _roleManager.CreateAsync(new IdentityRole { Name = "Manager" });
            _roleManager.CreateAsync(new IdentityRole { Name = "Administrator" });

            var createUser = new UserRegistrationDto()
            {
                FirstName = "string",
                LastName = "string",
                UserName = "string2",
                Password = "string2",
                Email = "string@string.com",
                Roles = new string[] { "User" }
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

            DbContext.Categories.AddRange(EntitiesForDb.Categories());
            DbContext.Providers.AddRange(EntitiesForDb.Providers());

            var models = DbContext.Providers.ToList();

        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}
