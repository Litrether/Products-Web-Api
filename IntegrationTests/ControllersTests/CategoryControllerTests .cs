using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnitTestProducts;
using Xunit;

namespace IntegrationTests.ControllerTests
{
    public class CategoryControllerTests : IClassFixture<BaseTestServerFixture>
    {
        private readonly BaseTestServerFixture _fixture;

        public CategoryControllerTests(BaseTestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetCategoriesReturnsCategories()
        {
            var response = await _fixture.Client.GetAsync($"api/categories");

            response.EnsureSuccessStatusCode();
            var models = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(models);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetCategoryReturnsCategory(int id)
        {
            var response = await _fixture.Client.GetAsync($"api/categories/{id}");

            response.EnsureSuccessStatusCode();
            var models = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(models);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task GetCategoryReturnsNotFoundWhenCategoryNotExists(int id)
        {
            var response = await _fixture.Client.GetAsync($"api/categories/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public async Task DeleteCategoryReturnsNoContentAndDeleteCategory(int id)
        {
            var response = await _fixture.Client.DeleteAsync($"api/categories/{id}");
            response.EnsureSuccessStatusCode();

            var checkResponse = await _fixture.Client.GetAsync($"api/categories/{id}");
            Assert.Equal(HttpStatusCode.NotFound, checkResponse.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task DeleteCategoryReturnsNotFoundWhenCategoryNotExists(int id)
        {
            var response = await _fixture.Client.DeleteAsync($"api/categories/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(0, "test")]
        [InlineData(100, "test")]
        public async Task UpdateCategoryReturnsNotFoundWhenCategoryNotExists(int id, string newName)
        {
            var updateCategory = new CategoryIncomingDto()
            {
                Name = newName
            };

            var contentUpdateCategory = new StringContent(JsonConvert.SerializeObject(updateCategory), Encoding.UTF8, "application/json");
            var response = await _fixture.Client.PutAsync($"api/categories/{id}", contentUpdateCategory);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(4, "test1")]
        [InlineData(5, "test2")]
        public async Task UpdateCategoryReturnsNoContentAndChangeCategory(int id, string newName)
        {
            var updateCategory = new CategoryIncomingDto()
            {
                Name = newName
            };

            var updateResponse = new StringContent(JsonConvert.SerializeObject(updateCategory), Encoding.UTF8, "application/json");
            var response = await _fixture.Client.PutAsync($"api/categories/{id}", updateResponse);
            response.EnsureSuccessStatusCode();


            var getResponse = await _fixture.Client.GetAsync($"api/categories/{id}");
            getResponse.EnsureSuccessStatusCode();

            var category = JsonConvert.DeserializeObject<CategoryOutgoingDto>(
                await getResponse.Content.ReadAsStringAsync());

            Assert.Equal(newName, category.Name);
        }
        
        [Fact]
        public async Task CreateCategoryRedirectToRouteGetCategory()
        {
            var category = new CategoryIncomingDto
            {
                Name = "test",
            };

            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            var response = await _fixture.Client.PostAsync($"/api/categories/", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
