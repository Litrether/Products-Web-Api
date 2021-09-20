using Repository.Extensions;
using System.Linq;
using Xunit;
using System.Linq.Dynamic.Core;

namespace UnitTestProducts.Tests.RepositoryExtensionsTests
{
    public class CategoryRepositoryExtensionsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("A")]
        [InlineData("iry Pro")]
        [InlineData("NoExistedName")]
        [InlineData("   MeAt  ")]
        public void CategorySearchTest(string searchTerm)
        {
            var categories = EntitiesForTests.Categories;

            var result = categories.Search(searchTerm).ToList();
            var expected = categories.Where(c => c.Name.ToLower().Contains(searchTerm.Trim().ToLower())).ToList();
            Assert.Equal(expected.Count, result.Count);
        }

        [Theory]
        [InlineData("id asc")]
        [InlineData("name asc")]
        [InlineData("name desc")]
        [InlineData("id asc,name desc")]
        public void CategorySortTest(string orderByQuery)
        {
            var categories = EntitiesForTests.Categories;

            var result = categories.Sort(orderByQuery).ToList();
            var expected= categories.OrderBy(orderByQuery).ToList();
            Assert.Equal(expected.First().Id, result.First().Id);
            Assert.Equal(expected.Last().Id, result.Last().Id);
            Assert.Equal(expected[2].Id, result[2].Id);
            Assert.Equal(expected[4].Id, result[4].Id);
        }
    }
}
