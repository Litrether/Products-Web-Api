using Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProducts.Tests.RepositoryExtensionsTests
{
    [TestClass]
    public class CategoryRepositoryExtensionsTests
    {
        [TestMethod]
        public void CategorySearchTest()
        {
            var categories = GetCategories();
            var foundCategories_EmptySearchTerm = categories.Search("").ToList();
            var expectedFoundCategories_EmptySearchTerm = categories.ToList();
            Assert.AreEqual(expectedFoundCategories_EmptySearchTerm.Count, foundCategories_EmptySearchTerm.Count);

            var foundCategories_LowerCaseSearchTerm = categories.Search("a").ToList();
            var expectedFoundCategories_LowerCaseSearchTerm = categories.Where(c => c.Name.ToLower().Contains("a".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundCategories_LowerCaseSearchTerm.Count, foundCategories_LowerCaseSearchTerm.Count);

            var foundCategories_UpperCaseSearchTerm = categories.Search("A").ToList();
            var expectedFoundCategories_UpperCaseSearchTerm = categories.Where(c => c.Name.ToLower().Contains("A".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundCategories_UpperCaseSearchTerm.Count, foundCategories_UpperCaseSearchTerm.Count);

            var foundCategories_SubstringSearchTerm = categories.Search("iry Pro").ToList();
            var expectedFoundCategories_SubstringSearchTerm = categories.Where(c => c.Name.ToLower().Contains("iry Pro".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundCategories_SubstringSearchTerm.Count, foundCategories_SubstringSearchTerm.Count);

            var foundCategories_NotExistedCategoryName = categories.Search("NoExistedCategoryName").ToList();
            var expectedFoundCategories_NotExistedCategoryName = categories.Where(c => c.Name.ToLower().Contains("NoExistedCategoryName".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundCategories_NotExistedCategoryName.Count, foundCategories_NotExistedCategoryName.Count);

            var foundCategories_CheckTrim = categories.Search("   MeAt  ").ToList();
            var expectedFoundCategories_CheckTrim = categories.Where(c => c.Name.ToLower().Contains("   MeAt  ".Trim().ToLower())).ToList();
            Assert.AreEqual(expectedFoundCategories_CheckTrim.Count, foundCategories_CheckTrim.Count);
        }

        [TestMethod]
        public void CategorySortTest()
        {
            var categories = GetCategories();

            var sortedCategories_IdAsc = categories.Sort("id").ToList();
            var expectedSortedCategories_IdAsc = categories.OrderBy(c => c.Id).ToList();
            Assert.AreEqual(expectedSortedCategories_IdAsc.First().Id, sortedCategories_IdAsc.First().Id);
            Assert.AreEqual(expectedSortedCategories_IdAsc.Last().Id, sortedCategories_IdAsc.Last().Id);
            Assert.AreEqual(expectedSortedCategories_IdAsc[2].Id, sortedCategories_IdAsc[2].Id);
            Assert.AreEqual(expectedSortedCategories_IdAsc[4].Id, sortedCategories_IdAsc[4].Id);

            var sortedCategories_IdDesc = categories.Sort("id desc").ToList();
            var expectedSortedCategories_IdDesc = categories.OrderByDescending(c => c.Id).ToList();
            Assert.AreEqual(expectedSortedCategories_IdDesc.First().Id, sortedCategories_IdDesc.First().Id);
            Assert.AreEqual(expectedSortedCategories_IdDesc.Last().Id, sortedCategories_IdDesc.Last().Id);
            Assert.AreEqual(expectedSortedCategories_IdDesc[2].Id, sortedCategories_IdDesc[2].Id);
            Assert.AreEqual(expectedSortedCategories_IdDesc[4].Id, sortedCategories_IdDesc[4].Id);

            var sortedCategories_NameAsc = categories.Sort("name").ToList();
            var expectedSortedCategories_NameAsc = categories.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedCategories_NameAsc.First().Id, sortedCategories_NameAsc.First().Id);
            Assert.AreEqual(expectedSortedCategories_NameAsc.Last().Id, sortedCategories_NameAsc.Last().Id);
            Assert.AreEqual(expectedSortedCategories_NameAsc[2].Id, sortedCategories_NameAsc[2].Id);
            Assert.AreEqual(expectedSortedCategories_NameAsc[4].Id, sortedCategories_NameAsc[4].Id);

            var sortedCategories_NameDesc = categories.Sort("name desc").ToList();
            var expectedSortedCategories_NameDesc = categories.OrderByDescending(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedCategories_NameDesc.First().Id, sortedCategories_NameDesc.First().Id);
            Assert.AreEqual(expectedSortedCategories_NameDesc.Last().Id, sortedCategories_NameDesc.Last().Id);
            Assert.AreEqual(expectedSortedCategories_NameDesc[2].Id, sortedCategories_NameDesc[2].Id);
            Assert.AreEqual(expectedSortedCategories_NameDesc[4].Id, sortedCategories_NameDesc[4].Id);

            var sortedCategories_IdAscNameDesc = categories.Sort("id asc, name desc").ToList();
            var expectedSortedCategories_IdAscNameDesc = categories.OrderBy(c => c.Id).ThenBy(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedCategories_IdAscNameDesc.First().Id, sortedCategories_IdAscNameDesc.First().Id);
            Assert.AreEqual(expectedSortedCategories_IdAscNameDesc.Last().Id, sortedCategories_IdAscNameDesc.Last().Id);
            Assert.AreEqual(expectedSortedCategories_IdAscNameDesc[2].Id, sortedCategories_IdAscNameDesc[2].Id);
            Assert.AreEqual(expectedSortedCategories_IdAscNameDesc[4].Id, sortedCategories_IdAscNameDesc[4].Id);

            var sortedCategories_NameDescIdAsc = categories.Sort("name desc, id asc").ToList();
            var expectedSortedCategories_NameDescIdAsc  = categories.OrderByDescending(c => c.Name).ThenBy(c => c.Id).ToList();
            Assert.AreEqual(expectedSortedCategories_NameDescIdAsc.First().Id, sortedCategories_NameDescIdAsc.First().Id);
            Assert.AreEqual(expectedSortedCategories_NameDescIdAsc.Last().Id, sortedCategories_NameDescIdAsc.Last().Id);
            Assert.AreEqual(expectedSortedCategories_NameDescIdAsc[2].Id, sortedCategories_NameDescIdAsc[2].Id);
            Assert.AreEqual(expectedSortedCategories_NameDescIdAsc[4].Id, sortedCategories_NameDescIdAsc[4].Id);

            var sortedCategories_Trash = categories.Sort("age desc, username asc").ToList();
            var expectedSortedCategories_Trash  = categories.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedCategories_Trash.First().Id, sortedCategories_Trash.First().Id);
            Assert.AreEqual(expectedSortedCategories_Trash.Last().Id, sortedCategories_Trash.Last().Id);
            Assert.AreEqual(expectedSortedCategories_Trash[2].Id, sortedCategories_Trash[2].Id);
            Assert.AreEqual(expectedSortedCategories_Trash[4].Id, sortedCategories_Trash[4].Id);
        }

        public IQueryable<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category{Id = 1, Name = "Vegetables"},
                new Category{Id = 2, Name = "Fruits"},
                new Category{Id = 3, Name = "Grocery"},
                new Category{Id = 4, Name = "Meat"},
                new Category{Id = 5, Name = "Dairy Products"},
                new Category{Id = 6, Name = "Confectionery"}
            }.AsQueryable();

        }
    }
}
