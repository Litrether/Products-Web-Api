using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICartRepository
    {
        public Task<List<Product>> GetCartProducts(User user);

        public void CreateCartProduct(Cart cart);

        public void DeleteCartProduct(Cart cart);
    }
}
