using Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.DataShaping;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace UnitTestProducts.RepositoryTests.DataShaperTests
{
    [TestClass]
    public class DataShaperTests
    {
        [TestMethod]
        public void EntityShapeDataTests()
        {
            var dataShaper = new DataShaper<Product>();
            var product = GetProduct().First();
            IDictionary<string, object> shapedProduct = dataShaper.ShapeData(product, "id,cost,providerId");
            Assert.AreEqual(product.Id, shapedProduct["Id"]);
            Assert.AreEqual(product.Cost, shapedProduct["Cost"]);
            Assert.AreEqual(product.ProviderId, shapedProduct["ProviderId"]);
            Assert.IsFalse(shapedProduct.ContainsKey("Name"));
        }

        [TestMethod]
        public void ShapeDataTests()
        {
            var dataShaper = new DataShaper<Product>();
            var products = GetProduct().ToList();
            var randIndex = new Random().Next(0, products.Count);

            var expectedProduct = products[randIndex];
            List<ExpandoObject> shapedProducts = dataShaper.ShapeData(products, "id,cost,providerId").ToList();
            IDictionary<string, object> shapedProduct = shapedProducts[randIndex];

            Assert.AreEqual(expectedProduct.Id, shapedProduct["Id"]);
            Assert.AreEqual(expectedProduct.Cost, shapedProduct["Cost"]);
            Assert.AreEqual(expectedProduct.ProviderId, shapedProduct["ProviderId"]);
            Assert.IsFalse(shapedProduct.ContainsKey("Name"));
        }

        public IEnumerable<Product> GetProduct() =>
            new List<Product> {
                new Product
                {
                    Id = 1,
                    Name = "Yoghurt",
                    Description = "Contains useful trace elements",
                    Cost = 1.53,
                    CategoryId = 5,
                    ProviderId = 4,
                    Category = new Category { Id = 5, Name = "Dairy Products" },
                    Provider = new Provider { Id = 4, Name = "Milk Gorki", LocationLat = 54.26659741177842m, LocationLong = 30.98771355605172m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/9f/19/09/9f19090f916c43dae8fa2d5e4f4298bd.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Butter",
                    Description = "From the freshest milk",
                    Cost = 2.25,
                    CategoryId = 5,
                    ProviderId = 4,
                    Category = new Category{ Id = 5, Name = "Dairy Products"},
                    Provider = new Provider { Id = 4, Name = "Milk Gorki", LocationLat = 54.26659741177842m, LocationLong = 30.98771355605172m, Products = { }},
                    ImageUrl = "https://i.pinimg.com/564x/d1/4a/ac/d14aac685c7d492d34d5b1c06f9e57ad.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Milk",
                    Description = "From the healthiest cows",
                    Cost = 1.17,
                    CategoryId = 5,
                    ProviderId = 2,
                    Category = new Category { Id = 5, Name = "Dairy Products" },
                    Provider = new Provider { Id = 2, Name = "Atha Makina", LocationLat = 38.54213540495325m, LocationLong = 27.033468297986936m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/11/37/9a/11379a0588f57f9e18f9e4fae9f3b6ed.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Cake",
                    Description = "Baked with love",
                    Cost = 4.5,
                    CategoryId = 6,
                    ProviderId = 5,
                    Category = new Category { Id = 6, Name = "Confectionery" },
                    Provider = new Provider { Id = 5, Name = "Archeda", LocationLat = 49.764727469041816m, LocationLong = 43.65468679640968m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/ec/5f/08/ec5f08f5c24077ba5a892e39105cc066.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "Sausage",
                    Description = "Fresh",
                    Cost = 3.85,
                    CategoryId = 4,
                    ProviderId = 1,
                    Category = new Category { Id = 4, Name = "Meat" },
                    Provider = new Provider { Id = 1, Name = "Underdog", LocationLat = 53.89019010647972m, LocationLong = 27.575736202063215m, Products = { } },
                    ImageUrl = "https://i.pinimg.com/564x/99/ee/3c/99ee3cc80018401e8f92a794ce4d5102.jpg"
                }
            };
    }
}
