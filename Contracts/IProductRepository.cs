using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IProductRepository
    {
        public Task<PagedList<Product>> GetAllProductsAsync(ProductParameters productParameters,
             bool trackChanges, double exchangeRate = default(double));

        public Task<Product> GetProductAsync(int productId, bool trackChanges,
             double exchangeRate = default(double));

        public Task<int> GetCountAsync();

        public void CreateProduct(Product product);

        public void DeleteProduct(Product product);
    }
}
