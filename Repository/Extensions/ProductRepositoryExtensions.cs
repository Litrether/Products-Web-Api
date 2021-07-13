using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class ProductRepositoryExtensions
    {
        public static IQueryable<Product> Search(this IQueryable<Product> products,
                 string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return products;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return products.Where(c =>
                c.Name.ToLower().Contains(lowerCaseTerm) ||
                c.Description.ToLower().Contains(lowerCaseTerm) ||
                c.Category.Name.ToLower().Contains(lowerCaseTerm) ||
                c.Provider.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Product> Sort(this IQueryable<Product> products,
             string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return products.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return products.OrderBy(e => e.Name);

            return products.OrderBy(orderQuery);
        }

        public static IQueryable<Product> IncludeFields(this IQueryable<Product> products) =>
            products.Include(p => p.Category) 
                    .Include(p => p.Provider);
    }
}
