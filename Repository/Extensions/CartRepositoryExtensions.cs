using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repository.Extensions
{
    public static class CartRepositoryExtensions
    {
        public static IQueryable<Cart> IncludeFields(this IQueryable<Cart> carts) =>
            carts.Include(c => c.Product)
                 .Include(c => c.Product.Category)
                 .Include(c => c.Product.Provider);
    }
}
