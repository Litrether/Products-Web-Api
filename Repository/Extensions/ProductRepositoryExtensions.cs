﻿using System.Linq;
using System.Linq.Dynamic.Core;
using Entities.Models;
using Entities.RequestFeatures;
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

        public static IQueryable<Product> Filter(this IQueryable<Product> products,
            ProductParameters productParameters)
        {
            var filterByCategoriesString = productParameters.Categories;
            var filterByProvidersString = productParameters.Providers;

            if (string.IsNullOrWhiteSpace(filterByCategoriesString) == false)
            {
                var splitCategoriesString = filterByCategoriesString.Split(',').ToList();
                if (splitCategoriesString != null)
                    products = products.Where(p => splitCategoriesString.Contains(p.Category.Name));
            }

            if (string.IsNullOrWhiteSpace(filterByProvidersString) == false)
            {
                var splitProvidersString = filterByProvidersString.Split(',').ToList();
                if (splitProvidersString != null)
                    products = products.Where(p => splitProvidersString.Contains(p.Provider.Name));
            }

            

            return products;
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