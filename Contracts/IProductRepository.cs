using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Contracts
{
    public interface IProductRepository
    {

        public Task<PagedList<Product>> GetAllProductsAsync(
            ProductParameters productParameters, bool trackChanges);

        public Task<Product> GetProductAsync(int productId, bool trackChanges);

        public void CreateProduct(Product product);

        public void DeleteProduct(Product product);
    }
}
