using Entities.Models;
using Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProviderRepository
    {
        public Task<PagedList<Provider>> GetAllProvidersAsync(ProviderParameters providerParameters,
            bool trackChanges);

        public Task<Provider> GetProviderAsync(int providerId, bool trackChanges);

        public Task<bool> CheckExistByName(string providerName, bool trackChanges);

        public void CreateProvider(Provider provider);

        public void DeleteProvider(Provider provider);
    }
}
