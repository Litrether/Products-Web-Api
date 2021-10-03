using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.ControllerTests
{
    public class CategoryControllerTests
    {
        [Fact]
        public async Task GetCategoriesReturnsCategories()
        {
            var response = await TestFixture.Client.GetAsync($"api/categories");

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
            var response = await TestFixture.Client.GetAsync($"api/categories/{id}");

            response.EnsureSuccessStatusCode();
            var models = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(models);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task GetCategoryReturnsNotFoundWhenCategoryNotExists(int id)
        {
            var response = await TestFixture.Client.GetAsync($"api/categories/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        public async Task DeleteCategoryReturnsNoContentAndDeleteCategory(int id)
        {
            var response = await TestFixture.Client.DeleteAsync($"api/categories/{id}");
            response.EnsureSuccessStatusCode();

            var checkResponse = await TestFixture.Client.GetAsync($"api/categories/{id}");
            Assert.Equal(HttpStatusCode.NotFound, checkResponse.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task DeleteCategoryReturnsNotFoundWhenCategoryNotExists(int id)
        {
            var response = await TestFixture.Client.DeleteAsync($"api/categories/{id}");
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
            var response = await TestFixture.Client.PutAsync($"api/categories/{id}", contentUpdateCategory);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(1, "test1")]
        [InlineData(2, "test2")]
        public async Task UpdateCategoryReturnsNoContentAndChangeCategory(int id, string newName)
        {
            var updateCategory = new CategoryIncomingDto()
            {
                Name = newName
            };

            var updateResponse = new StringContent(JsonConvert.SerializeObject(updateCategory), Encoding.UTF8, "application/json");
            var response = await TestFixture.Client.PutAsync($"api/categories/{id}", updateResponse);
            response.EnsureSuccessStatusCode();


            var getResponse = await TestFixture.Client.GetAsync($"api/categories/{id}");
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
            var response = await TestFixture.Client.PostAsync($"/api/categories/", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
