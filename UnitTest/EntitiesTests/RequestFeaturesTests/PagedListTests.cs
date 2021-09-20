using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Linq;
using Xunit;

namespace UnitTestProducts.EntitiesTests.RequestFeaturesTests
{
    public class PagedListTests
    {
        [Fact]
        public void ToPagedListTest()
        {
            var products = EntitiesForTests.Products().ToList();

            var rndPageSize = new Random().Next(1, products.Count / 3);
            var rndPageNumber = new Random().Next(1, (int)Math.Ceiling(products.Count / (double)rndPageSize));

            var pagedProducts = PagedList<Product>.ToPagedList(products, rndPageNumber, rndPageSize);
            var resultFirstProduct = pagedProducts.First();
            var exceptedFirstProduct = products[rndPageSize * (rndPageNumber - 1)];

            Assert.Equal(exceptedFirstProduct.Id, resultFirstProduct.Id);
        }
    }
}
