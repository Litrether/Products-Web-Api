using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnitTestProducts;
using Xunit;

namespace IntegrationTests.ControllersTests
{
    public class ProviderControllerTests 
    {
        [Fact]
        public async Task GetProvidersReturnsProviders()
        {
            var response = await TestFixture.Client.GetAsync($"api/providers");

            response.EnsureSuccessStatusCode();
            var models = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(models);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetProviderReturnsProvider(int id)
        {
            var response = await TestFixture.Client.GetAsync($"api/providers/{id}");

            response.EnsureSuccessStatusCode();
            var models = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(models);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task GetProviderReturnsNotFoundWhenProviderNotExists(int id)
        {
            var response = await TestFixture.Client.GetAsync($"api/providers/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public async Task DeleteProviderReturnsNoContentAndDeleteProvider(int id)
        {
            var response = await TestFixture.Client.DeleteAsync($"api/providers/{id}");
            response.EnsureSuccessStatusCode();

            var checkResponse = await TestFixture.Client.GetAsync($"api/providers/{id}");
            Assert.Equal(HttpStatusCode.NotFound, checkResponse.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task DeleteProviderReturnsNotFoundWhenProviderNotExists(int id)
        {
            var response = await TestFixture.Client.DeleteAsync($"api/providers/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(0, "test")]
        [InlineData(100, "test")]
        public async Task UpdateProviderReturnsNotFoundWhenProviderNotExists(int id, string newName)
        {
            var updateProvider = new ProviderIncomingDto()
            {
                Name = newName
            };

            var contentUpdateProvider = new StringContent(JsonConvert.SerializeObject(updateProvider), Encoding.UTF8, "application/json");
            var response = await TestFixture.Client.PutAsync($"api/providers/{id}", contentUpdateProvider);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(1, "test1")]
        [InlineData(2, "test2")]
        public async Task UpdateProviderReturnsNoContentAndChangeProvider(int id, string newName)
        {
            var updateProvider = new ProviderIncomingDto()
            {
                Name = newName
            };

            var updateResponse = new StringContent(JsonConvert.SerializeObject(updateProvider), Encoding.UTF8, "application/json");
            var response = await TestFixture.Client.PutAsync($"api/providers/{id}", updateResponse);
            response.EnsureSuccessStatusCode();


            var getResponse = await TestFixture.Client.GetAsync($"api/providers/{id}");
            getResponse.EnsureSuccessStatusCode();

            var provider = JsonConvert.DeserializeObject<ProviderOutgoingDto>(
                await getResponse.Content.ReadAsStringAsync());

            Assert.Equal(newName, provider.Name);
        }

        [Fact]
        public async Task CreateProviderRedirectToRouteGetProvider()
        {
            var provider = new ProviderIncomingDto()
            {
                Name = "test",
            };

            var content = new StringContent(JsonConvert.SerializeObject(provider), Encoding.UTF8, "application/json");
            var response = await TestFixture.Client.PostAsync($"/api/providers/", content);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
