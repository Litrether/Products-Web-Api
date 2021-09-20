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
    public class CategoryControllerTests
    {
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        Mock<IMapper> _mapper = new Mock<IMapper>();
        Mock<IRepositoryManager> _repo = new Mock<IRepositoryManager>();
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _controller = new CategoryController(_repo.Object, _logger.Object, _mapper.Object);
        }

        [Fact]
        public async void GetCategoryReturnsOkObjectResultWhenCategoryExist()
        {
            var testCategoryId = 1;
            var testCategory = EntitiesForTests.Categories().First();

            _repo.Setup(repo => repo.Category.GetCategoryAsync(testCategoryId, false).Result)
                .Returns(testCategory);
            var categoryParams = new CategoryParameters();

            var result = await _controller.GetCategory(testCategoryId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetCategoriesReturnsListOfCategories()
        {
            var categoryParams = new CategoryParameters();
            var categories = PagedList<Category>
                .ToPagedList(EntitiesForTests.Categories(), categoryParams.PageNumber, categoryParams.PageSize);
            _repo.Setup(repo => repo.Category.GetAllCategoriesAsync(categoryParams, false).Result)
                .Returns(categories);

            var result = await _controller.GetCategories(categoryParams);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetCategoriesReturnsNotFoundWhenCategoriesEmpty()
        {
            var CategoryParams = new CategoryParameters();
            var emptyCategoryList = new List<Category>();
            var categories = PagedList<Category>
                .ToPagedList(emptyCategoryList, CategoryParams.PageNumber, CategoryParams.PageSize);
            _repo.Setup(repo => repo.Category.GetAllCategoriesAsync(CategoryParams, false).Result)
                .Returns(categories);

            var result = await _controller.GetCategories(CategoryParams);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void AddCategoryReturnsRedirectToRouteResult()
        {
            var category = EntitiesForTests.Categories().First();
            var categoryIncomingDto = MapCategoryToCategoryIncomingDto(category);
            var categoryOutgoingDto = MapCategoryToCategoryOutgoingDto(category);
            _mapper.Setup(map => map.Map<Category>(categoryIncomingDto)).Returns(category);
            _mapper.Setup(map => map.Map<CategoryOutgoingDto>(category)).Returns(categoryOutgoingDto);
            _repo.Setup(repo => repo.Category.CreateCategory(category)).Verifiable();

            var result = await _controller.CreateCategory(categoryIncomingDto);

            var redirectToRouteResult = Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("GetCategory", redirectToRouteResult.RouteName);
            _repo.Verify(repo => repo.Category.CreateCategory(It.IsAny<Category>()));
        }

        [Fact]
        public async void UpdateCategoryReturnsNoContents()
        {
            var testCategoryId = 1;
            var Category = EntitiesForTests.Categories().First();
            var CategoryIncomingDto = MapCategoryToCategoryIncomingDto(Category);
            _repo.Setup(repo => repo.Category.GetCategoryAsync(testCategoryId, true).Result).Returns(Category);

            var result = await _controller.UpdateCategory(testCategoryId, CategoryIncomingDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void UpdateCategoryReturnsBadRequestWhenExceptionSave()
        {
            var testCategoryId = 1;
            var Category = EntitiesForTests.Categories().First();
            var CategoryIncomingDto = MapCategoryToCategoryIncomingDto(Category);
            _repo.Setup(repo => repo.Category.GetCategoryAsync(testCategoryId, true).Result).Returns(Category);
            _repo.Setup(repo => repo.SaveAsync()).Throws(new Exception("Test message", new Exception("Test inner message")));

            var result = await _controller.UpdateCategory(testCategoryId, CategoryIncomingDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void DeleteCategoryReturnsNoContents()
        {
            var testCategoryId = 1;
            var Category = EntitiesForTests.Categories().ToList().First(p => p.Id == testCategoryId);
            _repo.Setup(repo => repo.Category.GetCategoryAsync(testCategoryId, false).Result).Returns(Category);
            _repo.Setup(repo => repo.Category.DeleteCategory(Category)).Verifiable();

            var result = await _controller.DeleteCategory(testCategoryId);

            Assert.IsType<NoContentResult>(result);
            _repo.Verify(repo => repo.Category.DeleteCategory(It.IsAny<Category>()));
        }

        [Fact]
        public async void DeleteCategoryReturnsBadRequestWhenExceptionSave()
        {
            var testCategoryId = 1;
            var Category = EntitiesForTests.Categories().ToList().First(p => p.Id == testCategoryId);
            var CategoryIncomingDto = MapCategoryToCategoryIncomingDto(Category);
            _repo.Setup(repo => repo.Category.GetCategoryAsync(testCategoryId, true).Result).Returns(Category);
            _repo.Setup(repo => repo.SaveAsync()).Throws(new Exception("Test message", new Exception("Test inner message")));

            var result = await _controller.DeleteCategory(testCategoryId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        private CategoryIncomingDto MapCategoryToCategoryIncomingDto(Category Category)
        {
            return new CategoryIncomingDto
            {
                Name = Category.Name,
            };
        }

        private CategoryOutgoingDto MapCategoryToCategoryOutgoingDto(Category Category)
        {
            return new CategoryOutgoingDto
            {
                Id = Category.Id,
                Name = Category.Name,
            };
        }
    }
}
