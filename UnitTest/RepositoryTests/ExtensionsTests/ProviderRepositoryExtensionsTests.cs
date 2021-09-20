using Repository.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using Xunit;

namespace UnitTestProducts.Tests.RepositoryExtensionsTests
{
    public class ProviderRepositoryExtensionsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("A")]
        [InlineData("ilk Go")]
        [InlineData("NoExistedName")]
        [InlineData("   JaVimMAr  ")]
        public void ProviderSearchTest(string searchTerm)
        {
            var providers = EntitiesForTests.Providers;

            var result = providers.Search(searchTerm).ToList();
            var expected = providers.Where(c => c.Name.ToLower().Contains(searchTerm.Trim().ToLower())).ToList();
            Assert.Equal(expected.Count, result.Count);
        }

        [Theory]
        [InlineData("id asc")]
        [InlineData("id desc")]
        [InlineData("name asc")]
        [InlineData("name desc")]
        [InlineData("id asc, name desc")]
        [InlineData("name desc, id asc")]
        [InlineData("locationLong desc")]
        [InlineData("locationLat asc")]
        public void ProductSortTest(string OrderByQuery)
        {
            var providers = EntitiesForTests.Providers;

            var sortedProducts_IdAsc = providers.Sort(OrderByQuery).ToList();
            var expectedSortedProducts_IdAsc = providers.OrderBy(OrderByQuery).ToList();
            Assert.Equal(expectedSortedProducts_IdAsc.First().Id, sortedProducts_IdAsc.First().Id);
            Assert.Equal(expectedSortedProducts_IdAsc.Last().Id, sortedProducts_IdAsc.Last().Id);
            Assert.Equal(expectedSortedProducts_IdAsc[2].Id, sortedProducts_IdAsc[2].Id);
            Assert.Equal(expectedSortedProducts_IdAsc[4].Id, sortedProducts_IdAsc[4].Id);
        }
    }
}
