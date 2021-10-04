using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.ControllersTests
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task RegisterUserReturnsBadRequestWhenUserExist()
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

            var content = new StringContent(JsonConvert.SerializeObject(createUser),
                Encoding.UTF8, "application/json");
            var response = await TestFixture.Client.PostAsync($"/api/account/", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RegisterUserReturnsOk()
        {
            var createUser = new UserRegistrationDto()
            {
                FirstName = "test",
                LastName = "test",
                UserName = "test",
                Password = "test",
                Email = "test@test.com",
                Roles = new string[] { "Administrator", "Manager", "User" }
            };

            var content = new StringContent
                (JsonConvert.SerializeObject(createUser), Encoding.UTF8, "application/json");
            var response = await TestFixture.Client.PostAsync(
                $"/api/account/", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AuthenticateUserReturnsValidToken()
        {
            var authUser = new UserValidationDto()
            {
                UserName = "string2",
                Password = "string2",
            };

            var contentAuth = new StringContent(JsonConvert.SerializeObject(authUser),
                Encoding.UTF8, "application/json");
            var responseAuth = await TestFixture.Client.PostAsync($"/api/account/login", contentAuth);
            var responseJson = responseAuth.Content.ReadAsStringAsync().Result;
            var token = JObject.Parse(responseJson).SelectToken("token").ToString();

            TestFixture.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            int id = 20;
            var response = await TestFixture.Client.PostAsync($"api/cart/{id}", null);
            response.EnsureSuccessStatusCode();

            var product = JsonConvert.DeserializeObject<ProductOutgoingDto>(
                await response.Content.ReadAsStringAsync());

            Assert.Equal(id, product.Id);
        }

        [Fact]
        public async Task ChangePasswordReturnsNoContent()
        {
            var createUser = new UserRegistrationDto()
            {
                FirstName = "changePassTest",
                LastName = "changePassTest",
                UserName = "changePassTest",
                Password = "changePassTest",
                Email = "changePassTest@test.com",
                Roles = new string[] { "Administrator", "Manager", "User" }
            };
            var contentCreateUser = new StringContent
                (JsonConvert.SerializeObject(createUser), Encoding.UTF8, "application/json");
            var responseCreateUser = await TestFixture.Client.PostAsync($"/api/account/", contentCreateUser);
            responseCreateUser.EnsureSuccessStatusCode();

            var authUser = new UserValidationDto()
            {
                UserName = "changePassTest",
                Password = "changePassTest",
            };
            var contentAuth = new StringContent(JsonConvert.SerializeObject(authUser), Encoding.UTF8, "application/json");
            var responseAuth = await TestFixture.Client.PostAsync($"/api/account/login", contentAuth);
            responseAuth.EnsureSuccessStatusCode();
            var responseJson = await responseAuth.Content.ReadAsStringAsync();
            var token = JObject.Parse(responseJson).SelectToken("token").ToString();
            TestFixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var changePass = new ChangePasswordDto()
            {
                OldPassword = "changePassTest",
                NewPassword = "newpass",
            };
            var contentChangePass = new StringContent
                (JsonConvert.SerializeObject(changePass), Encoding.UTF8, "application/json");
            var responseChangePass = await TestFixture.Client.PutAsync($"/api/account/password", contentChangePass);
            responseChangePass.EnsureSuccessStatusCode();

            authUser = new UserValidationDto()
            {
                UserName = "changePassTest",
                Password = "newpass",
            };
            contentAuth = new StringContent(JsonConvert.SerializeObject(authUser), Encoding.UTF8, "application/json");
            responseAuth = await TestFixture.Client.PostAsync($"/api/account/login", contentAuth);
            responseAuth.EnsureSuccessStatusCode();
            responseJson = await responseAuth.Content.ReadAsStringAsync();
            token = JObject.Parse(responseJson).SelectToken("token").ToString();
            TestFixture.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            int id = 20;
            var response = await TestFixture.Client.PostAsync($"api/cart/{id}", null);
            response.EnsureSuccessStatusCode();
            var product = JsonConvert.DeserializeObject<ProductOutgoingDto>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, responseCreateUser.StatusCode);
            Assert.Equal(HttpStatusCode.NoContent, responseChangePass.StatusCode);
            Assert.Equal(id, product.Id);
        }
    }
}