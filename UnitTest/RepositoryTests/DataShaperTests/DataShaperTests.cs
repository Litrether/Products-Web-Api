using Entities.Models;
using Repository.DataShaping;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Xunit;

namespace UnitTestProducts.RepositoryTests.DataShaperTests
{
    public class DataShaperTests
    {
        [Fact]
        public void EntityShapeDataExistPropertyTest()
        {
            var dataShaper = new DataShaper<Product>();
            var product = EntitiesForTests.Products().First();
            IDictionary<string, object> shapedProduct = dataShaper.ShapeData(product, "id,cost,providerId");
            Assert.Equal(product.Id, shapedProduct["Id"]);
            Assert.Equal(product.Cost, shapedProduct["Cost"]);
            Assert.Equal(product.ProviderId, shapedProduct["ProviderId"]);
        }
        [Fact]
        public void EntityShapeDataNotExistPropertyTest()
        {
            var dataShaper = new DataShaper<Product>();
            var product = EntitiesForTests.Products().First();
            IDictionary<string, object> shapedProduct = dataShaper.ShapeData(product, "id,cost,providerId");
            Assert.False(shapedProduct.ContainsKey("Name"));
        }

        [Fact]
        public void ShapeDataExistPropertyTest()
        {
            var dataShaper = new DataShaper<Product>();
            var products = EntitiesForTests.Products().ToList();
            var randIndex = new Random().Next(0, products.Count);

            var expectedProduct = products[randIndex];
            List<ExpandoObject> shapedProducts = dataShaper.ShapeData(products, "id,cost,providerId").ToList();
            IDictionary<string, object> shapedProduct = shapedProducts[randIndex];

            Assert.Equal(expectedProduct.Id, shapedProduct["Id"]);
            Assert.Equal(expectedProduct.Cost, shapedProduct["Cost"]);
            Assert.Equal(expectedProduct.ProviderId, shapedProduct["ProviderId"]);
        }

        [Fact]
        public void ShapeDataNotExistPropertyTest()
        {
            var dataShaper = new DataShaper<Product>();
            var products = EntitiesForTests.Products().ToList();
            var randIndex = new Random().Next(0, products.Count);

            List<ExpandoObject> shapedProducts = dataShaper.ShapeData(products, "id,cost,providerId").ToList();
            IDictionary<string, object> shapedProduct = shapedProducts[randIndex];

            Assert.False(shapedProduct.ContainsKey("Name"));
        }
    }
}
