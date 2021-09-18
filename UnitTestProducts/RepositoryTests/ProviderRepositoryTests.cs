using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Products.Managers;
using Repository;
using System.Linq;

namespace UnitTestProducts.RepositoryTests
{
    [TestClass]
    public class ProviderRepositoryTests
    {

        public ProviderRepositoryTests()
        { }

        [TestMethod]
        public void GetAllProvidersTest()
        {
            var providers = EntitiesForTests.Providers.ToList();

            var providers = await FindAll(trackChanges)
                .Search(providerParameters.SearchTerm)
                .Sort(providerParameters.OrderBy)
                .ToListAsync();

            return PagedList<Provider>
                .ToPagedList(providers, providerParameters.PageNumber,
                    providerParameters.PageSize);
        }

    }
}
