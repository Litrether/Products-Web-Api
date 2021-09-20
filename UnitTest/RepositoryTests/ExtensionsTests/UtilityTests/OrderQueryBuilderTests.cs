using Entities.Models;
using Repository.Extensions.Utility;
using Xunit;

namespace UnitTestProducts.RepositoryTests.ExtensionsTests.UtilityTest
{
    public class OrderQueryBuilderTests
    {
        [Theory]
        [InlineData("Id,Name desc,ASD,FDS,coST", "Id ascending,Name descending,Cost ascending")]
        [InlineData("Id,Name desc,Cost,Id", "Id ascending,Name descending,Cost ascending,Id ascending")]
        public void CreateOrderQueryTest(string orderByQueryString, string expected)
        {
            var result = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString);
            Assert.Equal(expected, result);
        }
    }
}
