using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IProviderRepository
    {
        public Task<PagedList<Provider>> GetAllProvidersAsync(ProviderParameters providerParameters,
            bool trackChanges);

        public Task<Provider> GetProviderAsync(int providerId, bool trackChanges);

        public void CreateProvider(Provider provider);

        public void DeleteProvider(Provider provider);
    }
}
