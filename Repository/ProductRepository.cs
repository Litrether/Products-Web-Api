using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using System.Linq;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Product>> GetAllProductsAsync(
            ProductParameters productParameters, bool trackChanges, decimal exchangeRate)
        {
            var products = await FindAll(trackChanges)
                .CurrencyChange(exchangeRate)
                .Search(productParameters.SearchTerm)
                .Filter(productParameters)
                .IncludeFields()
                .Sort(productParameters.OrderBy)
                .ToListAsync();

            return PagedList<Product>
                .ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Product> GetProductAsync(int productId, bool trackChanges, decimal exchangeRate) =>
            await FindByCondition(c => c.Id.Equals(productId), trackChanges)
                .IncludeFields()
                .CurrencyChange(exchangeRate)
                .SingleOrDefaultAsync();

        public void CreateProduct(Product product) =>
            Create(product);

        public void DeleteProduct(Product product) =>
            Delete(product);
    }
}