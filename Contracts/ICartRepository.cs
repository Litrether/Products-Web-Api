using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICartRepository
    {
        public Task<(List<Product>, int)> GetUserCarts(User user);

        public void CreateCart(Cart cart);

        public void DeleteCart(Cart cart);
    }
}
