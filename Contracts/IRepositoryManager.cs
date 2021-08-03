using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        public IProductRepository Product { get; }

        public IProviderRepository Provider { get; }

        public ICategoryRepository Category { get; }

        public ICartRepository Cart { get; }

        public Task SaveAsync();
    }
}
