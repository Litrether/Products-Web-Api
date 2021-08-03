using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<(List<Product>, int)> GetUserCarts(User user)
        {
            var carts = await FindByCondition(c => c.User.UserName == user.UserName, trackChanges: false)
                .IncludeFields()
                .ToListAsync();

            var products = carts.Select(c => c.Product).ToList();

            return (products, products.Count);
        }

        public void CreateCart(Cart cart) =>
            Create(cart);

        public void DeleteCart(Cart cart) =>
            Delete(cart);
    }
}
