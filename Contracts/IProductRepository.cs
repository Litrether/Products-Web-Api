using Entities.Models;
using Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository
    {
        public Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters,
             bool trackChanges, decimal exchangeRate = default(decimal));

        public Task<Product> GetProductAsync(int productId, bool trackChanges,
             decimal exchangeRate = default(decimal));

        public void CreateProduct(Product product);

        public void DeleteProduct(Product product);
    }
}
