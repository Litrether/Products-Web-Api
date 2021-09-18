using Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Extensions.Utility;

namespace UnitTestProducts.RepositoryTests.ExtensionsTests.UtilityTest
{
    [TestClass]
    public class OrderQueryBuilderTest
    {
        [TestMethod]
        public void CreateOrderQueryTest()
        {
            var builtQuery_Product = OrderQueryBuilder.CreateOrderQuery<Product>("Id,Name desc,ASD,FDS,coST");
            var exceptedBuiltQuery_Product = "Id ascending,Name descending,Cost ascending";
            Assert.AreEqual(exceptedBuiltQuery_Product, builtQuery_Product);

            var builtQuery_Category = OrderQueryBuilder.CreateOrderQuery<Category>("Id,Name desc,Cost,Id");
            var exceptedBuiltQuery_Category = "Id ascending,Name descending,Id ascending";
            Assert.AreEqual(exceptedBuiltQuery_Category, builtQuery_Category);

        }
    }
}
