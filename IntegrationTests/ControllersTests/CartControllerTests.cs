using Entities.DataTransferObjects.Outcoming;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.ControllersTests
{
    public class CartControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task CreateCartReturnsProduct(int id)
        {
            var response = await TestFixture.Client.PostAsync($"api/cart/{id}", null);
            response.EnsureSuccessStatusCode();

            var product = JsonConvert.DeserializeObject<CategoryOutgoingDto>(
                await response.Content.ReadAsStringAsync());

            Assert.Equal(id, product.Id);
        }

        [Fact]
        public async Task GetCartProductsReturnsProducts()
        {
            await TestFixture.Client.PostAsync($"api/cart/5", null);
            await TestFixture.Client.PostAsync($"api/cart/6", null);
            await TestFixture.Client.PostAsync($"api/cart/7", null);

            var response = await TestFixture.Client.GetAsync($"api/cart");
            response.EnsureSuccessStatusCode();

            var models = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task DeleteCartProductReturnsProducts()
        {
            await TestFixture.Client.PostAsync($"api/cart/8", null);

            var responseDelete = await TestFixture.Client.DeleteAsync($"api/cart/8");
            responseDelete.EnsureSuccessStatusCode();

            var getResponse = await TestFixture.Client.GetAsync($"api/cart");
            getResponse.EnsureSuccessStatusCode();

            var products = JsonConvert.DeserializeObject<IEnumerable<ProviderOutgoingDto>>(
                await getResponse.Content.ReadAsStringAsync());

            var productWithId = products.ToList().FirstOrDefault(x => x.Id == 8);

            Assert.Null(productWithId);
        }
    }
}
