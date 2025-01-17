﻿using Contracts;
using Entities;
using Repository;
using System.Threading.Tasks;

namespace Products.Managers
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IProviderRepository _providerRepository;
        private ICartRepository _cartRepository;

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

        public ICartRepository Cart
        {
            get
            {
                if (_cartRepository == null)
                    _cartRepository = new CartRepository(_repositoryContext);

                return _cartRepository;
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
