using Entities.Models;
using Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICartRepository
    {
        public Task<PagedList<Product>> GetCartProducts(ProductParameters productParameters, User user,
            bool trackChanges, double exchangeRate = default(double));

        public void CreateCartProduct(Cart cart);

        public void DeleteCartProduct(Cart cart);
    }
}
