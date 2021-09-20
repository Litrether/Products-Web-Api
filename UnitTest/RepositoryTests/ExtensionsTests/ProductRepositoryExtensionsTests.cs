using Entities.RequestFeatures;
using Repository.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using Xunit;

namespace UnitTestProducts.Tests.RepositoryExtensionsTests
{
    public class ProductRepositoryExtensionsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("A")]
        [InlineData("iry Pro")]
        [InlineData("NoExistedName")]
        [InlineData("   MeAt  ")]
        public void ProductSearchTest(string searchTerm)
        {
            var products = EntitiesForTests.Products();

            var foundProducts_CheckTrim = products.Search(searchTerm).ToList();
            var expectedFoundProducts_CheckTrim = products.Where(p =>
                p.Name.ToLower().Contains(searchTerm.ToLower().Trim()) ||
                p.Description.ToLower().Contains(searchTerm.ToLower().Trim()) ||
                p.Category.Name.ToLower().Contains(searchTerm.ToLower().Trim()) ||
                p.Provider.Name.ToLower().Contains(searchTerm.ToLower().Trim())).ToList();
            Assert.Equal(expectedFoundProducts_CheckTrim.Count, foundProducts_CheckTrim.Count);
        }

        [Theory]
        [InlineData("id asc")]
        [InlineData("id desc")]
        [InlineData("name asc")]
        [InlineData("name desc")]
        [InlineData("id asc, name desc")]
        [InlineData("name desc, id asc")]
        public void ProductSortTest(string OrderByQuery)
        {
            var products = EntitiesForTests.Products();

            var sortedProducts_IdAsc = products.Sort(OrderByQuery).ToList();
            var expectedSortedProducts_IdAsc = products.OrderBy(OrderByQuery).ToList();
            Assert.Equal(expectedSortedProducts_IdAsc.First().Id, sortedProducts_IdAsc.First().Id);
            Assert.Equal(expectedSortedProducts_IdAsc.Last().Id, sortedProducts_IdAsc.Last().Id);
            Assert.Equal(expectedSortedProducts_IdAsc[2].Id, sortedProducts_IdAsc[2].Id);
            Assert.Equal(expectedSortedProducts_IdAsc[4].Id, sortedProducts_IdAsc[4].Id);
        }

        [Theory]
        [InlineData("Meat,Vegetables", "MiLida,Archeda,Underdog")]
        [InlineData("", "MiLida,Archeda,Underdog,NotExisted")]
        [InlineData("Meat,Vegetables,NotExisted", "")]
        public void FilterByPropertiesTest(string providers, string categories)
        {
            var products = EntitiesForTests.Products();

            var parameters = new ProductParameters()
            {
                Providers = providers,
                Categories = categories,
            };
            var splitedCategories = categories.Split(',');
            var splitedProviders = providers.Split(',');
            var result = products.FilterByProperties(parameters).ToList();
            var expected = products.Where(p =>
                (splitedCategories != null && splitedCategories.Contains(p.Category.Name)) &&
                (splitedProviders != null && splitedProviders.Contains(p.Provider.Name)))
                .ToList();
            Assert.Equal(expected.Count, result.Count);
        }

        [Theory]
        [InlineData(1, 20)]
        [InlineData(10, 5)]
        [InlineData(1, int.MaxValue)]
        [InlineData(int.MinValue, 1)]
        public void FilterByCostTest(int minCost, int maxCost)
        {
            var products = EntitiesForTests.Products();

            var parameters = new ProductParameters() { MinCost = minCost, MaxCost = maxCost, };
            var result = products.FilterByCost(parameters).ToList();
            var expected = products.Where(p =>
             parameters.MinCost <= p.Cost && p.Cost <= parameters.MaxCost).ToList();
            Assert.Equal(expected.Count, result.Count);
        }

        [Theory]
        [InlineData(0.9)]
        [InlineData(14.3)]
        [InlineData(200.0)]
        [InlineData(0.1)]
        [InlineData(0.0001)]
        [InlineData(int.MinValue)]
        public void ConvertCurrencyTest(double exchangeRate)
        {
            var product = EntitiesForTests.Products().First();

            var expectedConveredProducts = product.Cost / exchangeRate;
            var converеedProducts = product.ConvertCurrencyForEntities(exchangeRate);
            Assert.Equal(converеedProducts.Cost, expectedConveredProducts);
        }
    }
}
