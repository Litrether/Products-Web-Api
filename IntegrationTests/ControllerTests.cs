using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class ControllerTests : IClassFixture<BaseTestServerFixture>
    {
        private readonly BaseTestServerFixture _fixture;

        public ControllerTests(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("categories")]
        [InlineData("providers")]
        [InlineData("products")]
        public async Task GetReturnsModels(string path)
        {
            var response = await _fixture.Client.GetAsync($"api/{path}");

            response.EnsureSuccessStatusCode();
            var models = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(models);
        }

        [Theory]
        [InlineData("cart")]
        public async Task GetReturnsNotFound(string path)
        {
            var response = await _fixture.Client.GetAsync($"api/{path}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("categories")]
        public async Task Post_ShouldReturnListResult(string path)
        {
            var category = new CategoryIncomingDto
            {
                Name = "asdas",
            };

            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            var response = await _fixture.Client.PostAsync($"/api/{path}/", content);

          fresponse.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<Category>>(await response.Content.ReadAsStringAsync());

            Assert.NotEmpty(models);
        }
    }
}
