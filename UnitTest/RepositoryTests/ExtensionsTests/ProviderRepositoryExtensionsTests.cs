using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Extensions;
using System.Linq;

namespace UnitTestProducts.Tests.RepositoryExtensionsTests
{
    [TestClass]
    public class ProviderRepositoryExtensionsTests
    {
        [TestMethod]
        public void ProviderSearchTest()
        {
            var providers = EntitiesForTests.Providers;
            var foundProviders_EmptySearchTerm = providers.Search("").ToList();
            var expectedFoundProviders_EmptySearchTerm = providers.ToList();
            Assert.AreEqual(expectedFoundProviders_EmptySearchTerm.Count, foundProviders_EmptySearchTerm.Count);

            var foundProviders_LowerCaseSearchTerm = providers.Search("a").ToList();
            var expectedFoundProviders_LowerCaseSearchTerm = providers.Where(c => c.Name.ToLower().Contains("a".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundProviders_LowerCaseSearchTerm.Count, foundProviders_LowerCaseSearchTerm.Count);

            var foundProviders_UpperCaseSearchTerm = providers.Search("A").ToList();
            var expectedFoundProviders_UpperCaseSearchTerm = providers.Where(c => c.Name.ToLower().Contains("A".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundProviders_UpperCaseSearchTerm.Count, foundProviders_UpperCaseSearchTerm.Count);

            var foundProviders_SubstringSearchTerm = providers.Search("ilk Go").ToList();
            var expectedFoundProviders_SubstringSearchTerm = providers.Where(c => c.Name.ToLower().Contains("ilk Go".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundProviders_SubstringSearchTerm.Count, foundProviders_SubstringSearchTerm.Count);

            var foundProviders_NotExistedCategoryName = providers.Search("NoExistedCategoryName").ToList();
            var expectedFoundProviders_NotExistedCategoryName = providers.Where(c => c.Name.ToLower().Contains("NoExistedProviderName".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundProviders_NotExistedCategoryName.Count, foundProviders_NotExistedCategoryName.Count);

            var foundProviders_CheckTrim = providers.Search("   JaVimMAr  ").ToList();
            var expectedFoundProviders_CheckTrim = providers.Where(c => c.Name.ToLower().Contains("   JaVimMAr  ".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundProviders_CheckTrim.Count, foundProviders_CheckTrim.Count);
        }

        [TestMethod]
        public void ProviderSortTest()
        {
            var providers = EntitiesForTests.Providers;

            var sortedProviders_IdAsc = providers.Sort("id").ToList();
            var expectedSortedProviders_IdAsc = providers.OrderBy(c => c.Id).ToList();
            Assert.AreEqual(expectedSortedProviders_IdAsc.First().Id, sortedProviders_IdAsc.First().Id);
            Assert.AreEqual(expectedSortedProviders_IdAsc.Last().Id, sortedProviders_IdAsc.Last().Id);
            Assert.AreEqual(expectedSortedProviders_IdAsc[2].Id, sortedProviders_IdAsc[2].Id);
            Assert.AreEqual(expectedSortedProviders_IdAsc[4].Id, sortedProviders_IdAsc[4].Id);

            var sortedProviders_IdDesc = providers.Sort("id desc").ToList();
            var expectedSortedProviders_IdDesc = providers.OrderByDescending(c => c.Id).ToList();
            Assert.AreEqual(expectedSortedProviders_IdDesc.First().Id, sortedProviders_IdDesc.First().Id);
            Assert.AreEqual(expectedSortedProviders_IdDesc.Last().Id, sortedProviders_IdDesc.Last().Id);
            Assert.AreEqual(expectedSortedProviders_IdDesc[2].Id, sortedProviders_IdDesc[2].Id);
            Assert.AreEqual(expectedSortedProviders_IdDesc[4].Id, sortedProviders_IdDesc[4].Id);

            var sortedProviders_NameAsc = providers.Sort("name").ToList();
            var expectedSortedProviders_NameAsc = providers.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedProviders_NameAsc.First().Id, sortedProviders_NameAsc.First().Id);
            Assert.AreEqual(expectedSortedProviders_NameAsc.Last().Id, sortedProviders_NameAsc.Last().Id);
            Assert.AreEqual(expectedSortedProviders_NameAsc[2].Id, sortedProviders_NameAsc[2].Id);
            Assert.AreEqual(expectedSortedProviders_NameAsc[4].Id, sortedProviders_NameAsc[4].Id);

            var sortedProviders_NameDesc = providers.Sort("name desc").ToList();
            var expectedSortedProviders_NameDesc = providers.OrderByDescending(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedProviders_NameDesc.First().Id, sortedProviders_NameDesc.First().Id);
            Assert.AreEqual(expectedSortedProviders_NameDesc.Last().Id, sortedProviders_NameDesc.Last().Id);
            Assert.AreEqual(expectedSortedProviders_NameDesc[2].Id, sortedProviders_NameDesc[2].Id);
            Assert.AreEqual(expectedSortedProviders_NameDesc[4].Id, sortedProviders_NameDesc[4].Id);

            var sortedProviders_IdAscNameDesc = providers.Sort("id asc, name desc").ToList();
            var expectedSortedProviders_IdAscNameDesc = providers.OrderBy(c => c.Id).ThenBy(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedProviders_IdAscNameDesc.First().Id, sortedProviders_IdAscNameDesc.First().Id);
            Assert.AreEqual(expectedSortedProviders_IdAscNameDesc.Last().Id, sortedProviders_IdAscNameDesc.Last().Id);
            Assert.AreEqual(expectedSortedProviders_IdAscNameDesc[2].Id, sortedProviders_IdAscNameDesc[2].Id);
            Assert.AreEqual(expectedSortedProviders_IdAscNameDesc[4].Id, sortedProviders_IdAscNameDesc[4].Id);

            var sortedProviders_NameDescIdAsc = providers.Sort("name desc, id asc").ToList();
            var expectedSortedProviders_NameDescIdAsc = providers.OrderByDescending(c => c.Name).ThenBy(c => c.Id).ToList();
            Assert.AreEqual(expectedSortedProviders_NameDescIdAsc.First().Id, sortedProviders_NameDescIdAsc.First().Id);
            Assert.AreEqual(expectedSortedProviders_NameDescIdAsc.Last().Id, sortedProviders_NameDescIdAsc.Last().Id);
            Assert.AreEqual(expectedSortedProviders_NameDescIdAsc[2].Id, sortedProviders_NameDescIdAsc[2].Id);
            Assert.AreEqual(expectedSortedProviders_NameDescIdAsc[4].Id, sortedProviders_NameDescIdAsc[4].Id);

            var sortedProviders_Trash = providers.Sort("age desc, username asc").ToList();
            var expectedSortedProviders_Trash = providers.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedProviders_Trash.First().Id, sortedProviders_Trash.First().Id);
            Assert.AreEqual(expectedSortedProviders_Trash.Last().Id, sortedProviders_Trash.Last().Id);
            Assert.AreEqual(expectedSortedProviders_Trash[2].Id, sortedProviders_Trash[2].Id);
            Assert.AreEqual(expectedSortedProviders_Trash[4].Id, sortedProviders_Trash[4].Id);

            var sortedProviders_LocationLong = providers.Sort("locationlong desc").ToList();
            var expectedSortedProviders_LocationLong = providers.OrderByDescending(c => c.LocationLong).ToList();
            Assert.AreEqual(expectedSortedProviders_LocationLong.First().Id, sortedProviders_LocationLong.First().Id);
            Assert.AreEqual(expectedSortedProviders_LocationLong.Last().Id, sortedProviders_LocationLong.Last().Id);
            Assert.AreEqual(expectedSortedProviders_LocationLong[2].Id, sortedProviders_LocationLong[2].Id);
            Assert.AreEqual(expectedSortedProviders_LocationLong[4].Id, sortedProviders_LocationLong[4].Id);

            var sortedProviders_LocationLat = providers.Sort("LocationLat asc").ToList();
            var expectedSortedProviders_LocationLat = providers.OrderBy(c => c.LocationLat).ToList();
            Assert.AreEqual(expectedSortedProviders_LocationLat.First().Id, sortedProviders_LocationLat.First().Id);
            Assert.AreEqual(expectedSortedProviders_LocationLat.Last().Id, sortedProviders_LocationLat.Last().Id);
            Assert.AreEqual(expectedSortedProviders_LocationLat[2].Id, sortedProviders_LocationLat[2].Id);
            Assert.AreEqual(expectedSortedProviders_LocationLat[4].Id, sortedProviders_LocationLat[4].Id);
        }

    }
}
