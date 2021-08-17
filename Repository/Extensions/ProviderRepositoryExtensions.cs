using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class ProviderRepositoryExtensions
    {
        public static IQueryable<Provider> Search(this IQueryable<Provider> providers,
               string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return providers;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return providers.Where(c => c.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Provider> Sort(this IQueryable<Provider> providers,
             string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return providers.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Provider>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return providers.OrderBy(e => e.Name);

            return providers.OrderBy(orderQuery);
        }
    }
}
