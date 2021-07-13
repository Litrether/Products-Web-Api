using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
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
            ProductParameters productParameters, bool trackChanges)
        {
            var products = await FindAll(trackChanges)
                .Search(productParameters.SearchTerm)
                .Sort(productParameters.OrderBy)
                .IncludeFields()
                .ToListAsync();

            return PagedList<Product>
                .ToPagedList(products, productParameters.PageNumber,
                    productParameters.PageSize);
        }

        public async Task<Product> GetProductAsync(int productId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(productId), trackChanges)
                .IncludeFields()
                .SingleOrDefaultAsync();

        public void CreateProduct(Product product) =>
            Create(product);

        public void DeleteProduct(Product product) =>
            Delete(product);
    }
}