using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.ControllersTests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task GetProductsReturnsProducts()
        {
            var response = await TestFixture.Client.GetAsync($"api/products");

            response.EnsureSuccessStatusCode();
            var models = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(models);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetProductReturnsProduct(int id)
        {
            var response = await TestFixture.Client.GetAsync($"api/products/{id}");

            response.EnsureSuccessStatusCode();
            var models = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(models);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task GetProductReturnsNotFoundWhenProductNotExists(int id)
        {
            var response = await TestFixture.Client.GetAsync($"api/products/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public async Task DeleteProductReturnsNoContentAndDeleteProduct(int id)
        {
            var response = await TestFixture.Client.DeleteAsync($"api/products/{id}");
            response.EnsureSuccessStatusCode();

            var checkResponse = await TestFixture.Client.GetAsync($"api/products/{id}");
            Assert.Equal(HttpStatusCode.NotFound, checkResponse.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task DeleteProductReturnsNotFoundWhenProductNotExists(int id)
        {
            var response = await TestFixture.Client.DeleteAsync($"api/products/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(0, "test")]
        [InlineData(100, "test")]
        public async Task UpdateProductReturnsNotFoundWhenProductNotExists(int id, string newName)
        {
            var updateProduct = new ProductIncomingDto()
            {
                Name = "test",
                Description = "test",
                CategoryId = 1,
                ProviderId = 1,
                Cost = 1,
                ImageUrl = " ",
            };

            var contentUpdateProduct = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");
            var response = await TestFixture.Client.PutAsync($"api/products/{id}", contentUpdateProduct);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task UpdateProductReturnsNoContentAndChangeProduct(int id)
        {
            var updateProduct = new ProductIncomingDto()
            {
                Name = "test",
                Description = "test",
                CategoryId = 1,
                ProviderId = 1,
                Cost = 1,
                ImageUrl = " ",
            };

            var updateResponse = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");
            var response = await TestFixture.Client.PutAsync($"api/products/{id}", updateResponse);
            response.EnsureSuccessStatusCode();


            var getResponse = await TestFixture.Client.GetAsync($"api/products/{id}");
            getResponse.EnsureSuccessStatusCode();

            var Product = JsonConvert.DeserializeObject<ProductOutgoingDto>(
                await getResponse.Content.ReadAsStringAsync());

            Assert.Equal(updateProduct.Name, Product.Name);
        }

        [Fact]
        public async Task CreateProductRedirectToRouteGetProduct()
        {
            var product = new ProductIncomingDto()
            {
                Name = "test",
                Description = "test",
                CategoryId = 1,
                ProviderId = 1,
                Cost = 1,
                ImageUrl = " ",
            };

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await TestFixture.Client.PostAsync($"/api/products/", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
