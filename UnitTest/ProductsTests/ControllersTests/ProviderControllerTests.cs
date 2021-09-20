using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Products.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestProducts;
using Xunit;

namespace UnitTests.ProductsTests.ControllersTests
{
    public class ProviderControllerTests
    {
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        Mock<IMapper> _mapper = new Mock<IMapper>();
        Mock<IRepositoryManager> _repo = new Mock<IRepositoryManager>();
        private readonly ProviderController _controller;

        public ProviderControllerTests()
        {
            _controller = new ProviderController(_repo.Object, _logger.Object, _mapper.Object);

        }

        [Fact]
        public async void GetProviderReturnsOkObjectResultWhenProviderExist()
        {
            var testProviderId = 1;
            var testProvider = EntitiesForTests.Providers().First();

            _repo.Setup(repo => repo.Provider.GetProviderAsync(testProviderId, false).Result)
                .Returns(testProvider);
            var providerParams = new ProviderParameters();

            var result = await _controller.GetProvider(testProviderId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetProvidersReturnsListOfProviders()
        {
            var providerParams = new ProviderParameters();
            var providers = PagedList<Provider>
                .ToPagedList(EntitiesForTests.Providers(), providerParams.PageNumber, providerParams.PageSize);
            _repo.Setup(repo => repo.Provider.GetAllProvidersAsync(providerParams, false).Result)
                .Returns(providers);

            var result = await _controller.GetProviders(providerParams);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetProvidersReturnsNotFoundWhenProvidersEmpty()
        {
            var providerParams = new ProviderParameters();
            var emptyProviderList = new List<Provider>();
            var providers = PagedList<Provider>
                .ToPagedList(emptyProviderList, providerParams.PageNumber, providerParams.PageSize);
            _repo.Setup(repo => repo.Provider.GetAllProvidersAsync(providerParams, false).Result)
                .Returns(providers);

            var result = await _controller.GetProviders(providerParams);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void AddProviderReturnsRedirectToRouteResult()
        {
            var provider = EntitiesForTests.Providers().First();
            var providerIncomingDto = MapProviderToProviderIncomingDto(provider);
            var providerOutgoingDto = MapProviderToProviderOutgoingDto(provider);
            _mapper.Setup(map => map.Map<Provider>(providerIncomingDto)).Returns(provider);
            _mapper.Setup(map => map.Map<ProviderOutgoingDto>(provider)).Returns(providerOutgoingDto);
            _repo.Setup(repo => repo.Provider.CreateProvider(provider)).Verifiable();

            var result = await _controller.CreateProvider(providerIncomingDto);

            var redirectToRouteResult = Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("GetProvider", redirectToRouteResult.RouteName);
            _repo.Verify(repo => repo.Provider.CreateProvider(It.IsAny<Provider>()));
        }

        [Fact]
        public async void UpdateProviderReturnsNoContents()
        {
            var testProviderId = 1;
            var Provider = EntitiesForTests.Providers().First();
            var ProviderIncomingDto = MapProviderToProviderIncomingDto(Provider);
            _repo.Setup(repo => repo.Provider.GetProviderAsync(testProviderId, true).Result).Returns(Provider);

            var result = await _controller.UpdateProvider(testProviderId, ProviderIncomingDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void UpdateProviderReturnsBadRequestWhenExceptionSave()
        {
            var testProviderId = 1;
            var Provider = EntitiesForTests.Providers().First();
            var ProviderIncomingDto = MapProviderToProviderIncomingDto(Provider);
            _repo.Setup(repo => repo.Provider.GetProviderAsync(testProviderId, true).Result).Returns(Provider);
            _repo.Setup(repo => repo.SaveAsync()).Throws(new Exception("Test message", new Exception("Test inner message")));

            var result = await _controller.UpdateProvider(testProviderId, ProviderIncomingDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void DeleteProviderReturnsNoContents()
        {
            var testProviderId = 1;
            var Provider = EntitiesForTests.Providers().ToList().First(p => p.Id == testProviderId);
            _repo.Setup(repo => repo.Provider.GetProviderAsync(testProviderId, false).Result).Returns(Provider);
            _repo.Setup(repo => repo.Provider.DeleteProvider(Provider)).Verifiable();

            var result = await _controller.DeleteProvider(testProviderId);

            Assert.IsType<NoContentResult>(result);
            _repo.Verify(repo => repo.Provider.DeleteProvider(It.IsAny<Provider>()));
        }

        [Fact]
        public async void DeleteProviderReturnsBadRequestWhenExceptionSave()
        {
            var testProviderId = 1;
            var Provider = EntitiesForTests.Providers().ToList().First(p => p.Id == testProviderId);
            var ProviderIncomingDto = MapProviderToProviderIncomingDto(Provider);
            _repo.Setup(repo => repo.Provider.GetProviderAsync(testProviderId, true).Result).Returns(Provider);
            _repo.Setup(repo => repo.SaveAsync()).Throws(new Exception("Test message", new Exception("Test inner message")));

            var result = await _controller.DeleteProvider(testProviderId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        private ProviderIncomingDto MapProviderToProviderIncomingDto(Provider provider)
        {
            return new ProviderIncomingDto
            {
                Name = provider.Name,
                LocationLat = provider.LocationLat,
                LocationLong = provider.LocationLong,
            };
        }

        private ProviderOutgoingDto MapProviderToProviderOutgoingDto(Provider provider)
        {
            return new ProviderOutgoingDto
            {
                Id = provider.Id,
                Name = provider.Name,
                LocationLat = provider.LocationLat,
                LocationLong = provider.LocationLong,
            };
        }
    }
}
