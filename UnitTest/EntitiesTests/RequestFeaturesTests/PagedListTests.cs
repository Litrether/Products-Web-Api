using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestProducts.EntitiesTests.RequestFeaturesTests
{
    [TestClass]
    public class PagedListTests
    {
        [TestMethod]
        public void ToPagedListTest()
        {
            var products = EntitiesForTests.Products.ToList();

            var rndPageSize = new Random().Next(1, products.Count / 3);
            var rndPageNumber = new Random().Next(1, (int)Math.Ceiling(products.Count / (double)rndPageSize));

            var pagedProducts = PagedList<Product>.ToPagedList(products, rndPageNumber, rndPageSize);
            var resultFirstProduct = pagedProducts.First();
            var exceptedFirstProduct = products[rndPageSize * (rndPageNumber - 1)];

            Assert.AreEqual(exceptedFirstProduct.Id, resultFirstProduct.Id);
        }
    }
}
