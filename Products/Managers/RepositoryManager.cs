using System;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Repository;

namespace Products.Managers
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IProviderRepository _providerRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);

                return _productRepository;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_repositoryContext);

                return _categoryRepository;
            }
        }

        public IProviderRepository Provider
        {
            get
            {
                if (_providerRepository == null)
                    _providerRepository = new ProviderRepository(_repositoryContext);

                return _providerRepository;
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _repositoryContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
