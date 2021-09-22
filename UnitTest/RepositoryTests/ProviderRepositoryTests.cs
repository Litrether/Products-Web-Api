using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Repository;
using Xunit;

namespace UnitTestProducts.RepositoryTests
{
    public class ProviderRepositoryTests
    {
        Mock<RepositoryContext> _repositoryContext;
        private readonly ProviderRepository _providerRepository;

        public ProviderRepositoryTests()
        {
            var options = new Mock<DbContextOptions>(null);
            _repositoryContext =  new Mock<RepositoryContext>(options.Object);
            _providerRepository = new ProviderRepository(_repositoryContext.Object);
        }

        [Fact]
        public void CreateProvider()
        {
            _providerRepository.CreateProvider(new Provider());

            _repositoryContext.Verify(x => x.Providers.Add(It.IsAny<Provider>()));
        }
    }
}
