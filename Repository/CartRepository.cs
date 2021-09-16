using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
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

        public async Task<PagedList<Product>> GetCartProductsAsync(ProductParameters productParameters, User user,
            bool trackChanges, double exchangeRate = default(double))
        {
            var products = await FindByCondition(c => c.User.UserName == user.UserName, trackChanges)
                .Include(c => c.Product.Category)
                .Include(c => c.Product.Provider)
                .Select(c => c.Product)
                .Search(productParameters.SearchTerm)
                .Sort(productParameters.OrderBy)
                .FilterByProperties(productParameters)
                .ToListAsync();

            products?.ConvertCurrency(exchangeRate);
            var filteredProducts = products?.FilterByCost(productParameters);

            return PagedList<Product>
                .ToPagedList(filteredProducts, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Cart> GetCartProductAsync(User user, int productId, bool trackChanges) =>
            await FindByCondition(c => c.User.UserName == user.UserName && c.Product.Id == productId, trackChanges)
            .SingleOrDefaultAsync();

        public void CreateCartProduct(Cart cart) =>
            Create(cart);

        public void DeleteCartProduct(Cart cart) =>
            Delete(cart);
    }
}
