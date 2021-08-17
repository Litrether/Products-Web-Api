using Entities.Models;
using Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICartRepository
    {
        public Task<PagedList<Product>> GetCartProductsAsync(ProductParameters productParameters, User user,
            bool trackChanges, double exchangeRate = default(double));

        public Task<Cart> GetCartProductAsync(User user, int productId, bool trackChanges);

        public void CreateCartProduct(Cart cart);

        public void DeleteCartProduct(Cart cart);
    }
}
