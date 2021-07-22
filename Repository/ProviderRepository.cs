using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System.Threading.Tasks;

namespace Repository
{
    public class ProviderRepository : RepositoryBase<Provider>, IProviderRepository
    {
        public ProviderRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Provider>> GetAllProvidersAsync(
            ProviderParameters providerParameters, bool trackChanges)
        {
            var providers = await FindAll(trackChanges)
                .Search(providerParameters.SearchTerm)
                .Sort(providerParameters.OrderBy)
                .ToListAsync();

            return PagedList<Provider>
                .ToPagedList(providers, providerParameters.PageNumber,
                    providerParameters.PageSize);
        }

        public async Task<Provider> GetProviderAsync(int providerId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(providerId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateProvider(Provider provider) =>
            Create(provider);

        public void DeleteProvider(Provider provider) =>
            Delete(provider);

    }
}

