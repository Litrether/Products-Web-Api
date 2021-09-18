using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProducts.Tests.RepositoryExtensionsTests
{
    [TestClass]
    public class ProductRepositoryExtensionsTests
    {
        [TestMethod]
        public void ProductSearchTest()
        {
            var products = EntitiesForTests.Products;

            var EmptySearchTerm = "";
            var foundProducts_EmptySearchTerm = products.Search(EmptySearchTerm).ToList();
            var expectedFoundProducts_EmptySearchTerm = products.ToList();
            Assert.AreEqual(foundProducts_EmptySearchTerm.Count, expectedFoundProducts_EmptySearchTerm.Count);

            var LowerCaseSearchTerm = "a";
            var foundProducts_LowerCaseSearchTerm = products.Search(LowerCaseSearchTerm).ToList();
            var expectedFoundProducts_LowerCaseSearchTerm = products.Where(p =>
                p.Name.ToLower().Contains(LowerCaseSearchTerm.ToLower().Trim()) ||
                p.Description.ToLower().Contains(LowerCaseSearchTerm.ToLower().Trim()) ||
                p.Category.Name.ToLower().Contains(LowerCaseSearchTerm.ToLower().Trim()) ||
                p.Provider.Name.ToLower().Contains(LowerCaseSearchTerm.ToLower().Trim())).ToList();
            Assert.AreEqual(expectedFoundProducts_LowerCaseSearchTerm.Count, foundProducts_LowerCaseSearchTerm.Count);

            var UpperCaseSearchTerm = "A";
            var foundProducts_UpperCaseSearchTerm = products.Search("A").ToList();
            var expectedFoundProducts_UpperCaseSearchTerm = products.Where(p =>
                p.Name.ToLower().Contains(UpperCaseSearchTerm.ToLower().Trim()) ||
                p.Description.ToLower().Contains(UpperCaseSearchTerm.ToLower().Trim()) ||
                p.Category.Name.ToLower().Contains(UpperCaseSearchTerm.ToLower().Trim()) ||
                p.Provider.Name.ToLower().Contains(UpperCaseSearchTerm.ToLower().Trim())).ToList();
            Assert.AreEqual(expectedFoundProducts_UpperCaseSearchTerm.Count, foundProducts_UpperCaseSearchTerm.Count);

            var SubstringSearchTerm = "do colo";
            var foundProducts_SubstringSearchTerm = products.Search(SubstringSearchTerm).ToList();
            var expectedFoundProducts_SubstringSearchTerm = products.Where(p =>
                p.Name.ToLower().Contains(SubstringSearchTerm.ToLower().Trim()) ||
                p.Description.ToLower().Contains(SubstringSearchTerm.ToLower().Trim()) ||
                p.Category.Name.ToLower().Contains(SubstringSearchTerm.ToLower().Trim()) ||
                p.Provider.Name.ToLower().Contains(SubstringSearchTerm.ToLower().Trim())).ToList();
            Assert.AreEqual(expectedFoundProducts_SubstringSearchTerm.Count, foundProducts_SubstringSearchTerm.Count);

            var NotExistedCategoryName = "NoExistedProductName";
            var foundProducts_NotExistedCategoryName = products.Search(NotExistedCategoryName).ToList();
            var expectedFoundProducts_NotExistedCategoryName = products.Where(p =>
                p.Name.ToLower().Contains(NotExistedCategoryName.ToLower().Trim()) ||
                p.Description.ToLower().Contains(NotExistedCategoryName.ToLower().Trim()) ||
                p.Category.Name.ToLower().Contains(NotExistedCategoryName.ToLower().Trim()) ||
                p.Provider.Name.ToLower().Contains(NotExistedCategoryName.ToLower().Trim())).ToList();
            Assert.AreEqual(expectedFoundProducts_NotExistedCategoryName.Count, foundProducts_NotExistedCategoryName.Count);

            var CheckTrim = "   PoTaTo  ";
            var foundProducts_CheckTrim = products.Search(CheckTrim).ToList();
            var expectedFoundProducts_CheckTrim = products.Where(p =>
                p.Name.ToLower().Contains(CheckTrim.ToLower().Trim()) ||
                p.Description.ToLower().Contains(CheckTrim.ToLower().Trim()) ||
                p.Category.Name.ToLower().Contains(CheckTrim.ToLower().Trim()) ||
                p.Provider.Name.ToLower().Contains(CheckTrim.ToLower().Trim())).ToList();
            Assert.AreEqual(expectedFoundProducts_CheckTrim.Count, foundProducts_CheckTrim.Count);
        }

        [TestMethod]
        public void ProductSortTest()
        {
            var products = EntitiesForTests.Products;

            var sortedProducts_IdAsc = products.Sort("id").ToList();
            var expectedSortedProducts_IdAsc = products.OrderBy(c => c.Id).ToList();
            Assert.AreEqual(expectedSortedProducts_IdAsc.First().Id, sortedProducts_IdAsc.First().Id);
            Assert.AreEqual(expectedSortedProducts_IdAsc.Last().Id, sortedProducts_IdAsc.Last().Id);
            Assert.AreEqual(expectedSortedProducts_IdAsc[2].Id, sortedProducts_IdAsc[2].Id);
            Assert.AreEqual(expectedSortedProducts_IdAsc[4].Id, sortedProducts_IdAsc[4].Id);

            var sortedProducts_IdDesc = products.Sort("id desc").ToList();
            var expectedSortedProducts_IdDesc = products.OrderByDescending(c => c.Id).ToList();
            Assert.AreEqual(expectedSortedProducts_IdDesc.First().Id, sortedProducts_IdDesc.First().Id);
            Assert.AreEqual(expectedSortedProducts_IdDesc.Last().Id, sortedProducts_IdDesc.Last().Id);
            Assert.AreEqual(expectedSortedProducts_IdDesc[2].Id, sortedProducts_IdDesc[2].Id);
            Assert.AreEqual(expectedSortedProducts_IdDesc[4].Id, sortedProducts_IdDesc[4].Id);

            var sortedProducts_NameAsc = products.Sort("name").ToList();
            var expectedSortedProducts_NameAsc = products.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedProducts_NameAsc.First().Id, sortedProducts_NameAsc.First().Id);
            Assert.AreEqual(expectedSortedProducts_NameAsc.Last().Id, sortedProducts_NameAsc.Last().Id);
            Assert.AreEqual(expectedSortedProducts_NameAsc[2].Id, sortedProducts_NameAsc[2].Id);
            Assert.AreEqual(expectedSortedProducts_NameAsc[4].Id, sortedProducts_NameAsc[4].Id);

            var sortedProducts_NameDesc = products.Sort("name desc").ToList();
            var expectedSortedProducts_NameDesc = products.OrderByDescending(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedProducts_NameDesc.First().Id, sortedProducts_NameDesc.First().Id);
            Assert.AreEqual(expectedSortedProducts_NameDesc.Last().Id, sortedProducts_NameDesc.Last().Id);
            Assert.AreEqual(expectedSortedProducts_NameDesc[2].Id, sortedProducts_NameDesc[2].Id);
            Assert.AreEqual(expectedSortedProducts_NameDesc[4].Id, sortedProducts_NameDesc[4].Id);

            var sortedProducts_IdAscNameDesc = products.Sort("id asc, name desc").ToList();
            var expectedSortedProducts_IdAscNameDesc = products.OrderBy(c => c.Id).ThenBy(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedProducts_IdAscNameDesc.First().Id, sortedProducts_IdAscNameDesc.First().Id);
            Assert.AreEqual(expectedSortedProducts_IdAscNameDesc.Last().Id, sortedProducts_IdAscNameDesc.Last().Id);
            Assert.AreEqual(expectedSortedProducts_IdAscNameDesc[2].Id, sortedProducts_IdAscNameDesc[2].Id);
            Assert.AreEqual(expectedSortedProducts_IdAscNameDesc[4].Id, sortedProducts_IdAscNameDesc[4].Id);

            var sortedProducts_NameDescIdAsc = products.Sort("name desc, id asc").ToList();
            var expectedSortedProducts_NameDescIdAsc = products.OrderByDescending(c => c.Name).ThenBy(c => c.Id).ToList();
            Assert.AreEqual(expectedSortedProducts_NameDescIdAsc.First().Id, sortedProducts_NameDescIdAsc.First().Id);
            Assert.AreEqual(expectedSortedProducts_NameDescIdAsc.Last().Id, sortedProducts_NameDescIdAsc.Last().Id);
            Assert.AreEqual(expectedSortedProducts_NameDescIdAsc[2].Id, sortedProducts_NameDescIdAsc[2].Id);
            Assert.AreEqual(expectedSortedProducts_NameDescIdAsc[4].Id, sortedProducts_NameDescIdAsc[4].Id);

            var sortedProducts_Trash = products.Sort("age desc, username asc").ToList();
            var expectedSortedProducts_Trash = products.OrderBy(c => c.Name).ToList();
            Assert.AreEqual(expectedSortedProducts_Trash.First().Id, sortedProducts_Trash.First().Id);
            Assert.AreEqual(expectedSortedProducts_Trash.Last().Id, sortedProducts_Trash.Last().Id);
            Assert.AreEqual(expectedSortedProducts_Trash[2].Id, sortedProducts_Trash[2].Id);
            Assert.AreEqual(expectedSortedProducts_Trash[4].Id, sortedProducts_Trash[4].Id);
        }

        [TestMethod]
        public void FilterByProperties()
        {
            var products = EntitiesForTests.Products;

            var CategoriesAndProviders = new ProductParameters()
            {
                Categories = "Meat,Vegetables",
                Providers = "MiLida,Archeda,Underdog"
            };
            var splitedCategories = CategoriesAndProviders.Categories.Split(',');
            var splitedProviders = CategoriesAndProviders.Providers.Split(',');
            var filteredProducts_ByCategoriesAndProviders = products.FilterByProperties(CategoriesAndProviders).ToList();
            var expectedFilteredProducts_CategoriesAndProviders = products.Where(p =>
                splitedCategories.Contains(p.Category.Name) &&
                splitedProviders.Contains(p.Provider.Name))
                .ToList();
            Assert.AreEqual(expectedFilteredProducts_CategoriesAndProviders.Count, filteredProducts_ByCategoriesAndProviders.Count);

            var сategories = new ProductParameters()
            {
                Categories = "Meat,Vegetables",
            };
            splitedCategories = сategories.Categories.Split(',');
            var filteredProducts_ByCategories = products.FilterByProperties(сategories).ToList();
            var expectedFilteredProducts_ByCategories = products.Where(p =>
                splitedCategories.Contains(p.Category.Name))
                .ToList();
            Assert.AreEqual(expectedFilteredProducts_ByCategories.Count, filteredProducts_ByCategories.Count);

            var providers = new ProductParameters()
            {
                Providers = "MiLida,Archeda,Underdog",
            };
            splitedProviders = providers.Providers.Split(',');
            var filteredProducts_ByProviders = products.FilterByProperties(providers).ToList();
            var expectedFilteredProducts_ByProviders = products.Where(p =>
                splitedProviders.Contains(p.Provider.Name))
                .ToList();
            Assert.AreEqual(filteredProducts_ByProviders.Count, expectedFilteredProducts_ByProviders.Count);
        }

        [TestMethod]
        public void FilterByCost()
        {
            var products = EntitiesForTests.Products;

            var MinMaxCost = new ProductParameters() { MinCost = 1, MaxCost = 20, };
            var filteredProducts_MinMaxCost = products.FilterByCost(MinMaxCost).ToList();
            var expectedFilteredProducts_MinMaxCost = products.Where(p =>
             MinMaxCost.MinCost <= p.Cost && p.Cost <= MinMaxCost.MaxCost).ToList();
            Assert.AreEqual(expectedFilteredProducts_MinMaxCost.Count, filteredProducts_MinMaxCost.Count);

            var MinMaxCostFail = new ProductParameters() { MinCost = 10, MaxCost = 5, };
            var filteredProductsMinMaxCostFail = products.FilterByCost(MinMaxCostFail).ToList();
            var expectedFilteredProducts_MinMaxCostFail = products.Where(p =>
             MinMaxCostFail.MinCost <= p.Cost && p.Cost <= MinMaxCostFail.MaxCost).ToList();
            Assert.AreEqual(expectedFilteredProducts_MinMaxCostFail.Count, filteredProductsMinMaxCostFail.Count);

            var MinCost = new ProductParameters() { MinCost = 1 };
            var filteredProducts_MinCost = products.FilterByCost(MinCost).ToList();
            var expectedFilteredProducts_MinCost = products.Where(p =>
             MinCost.MinCost <= p.Cost).ToList();
            Assert.AreEqual(expectedFilteredProducts_MinCost.Count, filteredProducts_MinCost.Count);

            var MaxCost = new ProductParameters() { MaxCost = 1 };
            var filteredProducts_MaxCost = products.FilterByCost(MaxCost).ToList();
            var expectedFilteredProducts_MaxCost = products.Where(p =>
             p.Cost <= MaxCost.MaxCost).ToList();
            Assert.AreEqual(expectedFilteredProducts_MaxCost.Count, filteredProducts_MaxCost.Count);
        }

        [TestMethod]
        public void ConvertCurrency()
        {
            var product = EntitiesForTests.Products.First();

            var exchangeRate = 0.9;
            var expectedConveredProducts = product.Cost / exchangeRate;
            var converеedProducts = product.ConvertCurrencyForEntities(exchangeRate);
            Assert.AreEqual(converеedProducts.Cost, expectedConveredProducts);
        }
    }
}
