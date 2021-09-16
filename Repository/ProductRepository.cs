using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters,
            bool trackChanges, double exchangeRate = default(double))
        {
            var products = await FindAll(trackChanges)
                .Search(productParameters.SearchTerm)
                .FilterByProperties(productParameters)
                .IncludeFields()
                .Sort(productParameters.OrderBy)
                .ToListAsync();

            products?.ConvertCurrency(exchangeRate);
            var filteredProducts = products?.FilterByCost(productParameters);

            return PagedList<Product>
                .ToPagedList(filteredProducts, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Product> GetProductAsync(int productId, bool trackChanges, double exchangeRate = default(double))
        {
            var product = await FindByCondition(c => c.Id.Equals(productId), trackChanges)
                .IncludeFields()
                .SingleOrDefaultAsync();

            product?.ConvertCurrencyForEntities(exchangeRate);

            return product;
        }

        public void CreateProduct(Product product) =>
            Create(product);

        public void DeleteProduct(Product product) =>
            Delete(product);
    }
}