using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Xunit;
using System.Text;
using System.Net.Http.Headers;

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
        public async Task Get_ShouldReturnListResult(string path)
        {
            var response = await _fixture.Client.GetAsync($"/api/{path}/");

           var models = JsonConvert.DeserializeObject<IEnumerable<Category>>(await response.Content.ReadAsStringAsync());

            Assert.NotEmpty(models);
        }

        //[Theory]
        //[InlineData("categories")]
        //public async Task Post_ShouldReturnListResult(string path)
        //{
        //    var category = new CategoryIncomingDto
        //    {
        //        Name = "asdas",
        //    };

        //    var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
        //    var response = await _fixture.Client.PostAsync($"/api/{path}/", content);

        //    response.EnsureSuccessStatusCode();
        //    var models = JsonConvert.DeserializeObject<IEnumerable<Category>>(await response.Content.ReadAsStringAsync());

        //    Assert.NotEmpty(models);
        //}
    }
}
