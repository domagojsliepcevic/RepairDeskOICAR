using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepairDesk.Api.Controllers;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Category;
using RepairDesk.Api.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDesk.Api.Tests
{
    [TestFixture]
    public class CategoryControllerTests
    {
        private Mock<ILogger<CategoryController>> _loggerMock;
        private Mock<ICategoryService> _categoryServiceMock;
        private CategoryController _controller;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CategoryController>>();
            _categoryServiceMock = new Mock<ICategoryService>();
            _controller = new CategoryController(_loggerMock.Object, _categoryServiceMock.Object);
        }

        // Test 1: Check if the "GetCategories" method correctly calls the "GetCategoriesAsync" method from the "ICategoryService" and returns the result.
        [Test]
        public async Task GetCategories_ReturnsCategories()
        {
            // Arrange
            var pageNr = 1;
            var categories = new PagedResult<CategoryListModel> {Items = new List<CategoryListModel> { new CategoryListModel() } };
            _categoryServiceMock.Setup(service => service.GetCategoriesAsync(pageNr)).ReturnsAsync(categories);

            // Act
            var result = await _controller.GetCategories(pageNr);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(categories));
            _categoryServiceMock.Verify(service => service.GetCategoriesAsync(pageNr), Times.Once);
        }

        // Test 2: Check if the "GetCategory" method correctly calls the "GetCategoryAsync" method from the "ICategoryService" and returns the result.
        [Test]
        public async Task GetCategory_ReturnsCategory_WhenExists()
        {
            // Arrange
            var categoryId = 1;
            var category = new CategoryDetailsModel();
            _categoryServiceMock.Setup(service => service.GetCategoryAsync(categoryId)).ReturnsAsync(category);

            // Act
            var result = await _controller.GetCategory(categoryId);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(category));
            _categoryServiceMock.Verify(service => service.GetCategoryAsync(categoryId), Times.Once);
        }

        // Test 3: Check if the "GetCategory" method correctly returns NotFound when the category doesn't exist.
        [Test]
        public async Task GetCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryId = 1;
            _categoryServiceMock.Setup(service => service.GetCategoryAsync(categoryId)).ReturnsAsync((CategoryDetailsModel)null);

            // Act
            var result = await _controller.GetCategory(categoryId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
            _categoryServiceMock.Verify(service => service.GetCategoryAsync(categoryId), Times.Once);
        }

        // Test 4: Check if the "AddCategory" method correctly calls the "AddCategoryAsync" method from the "ICategoryService" and returns the result.
        [Test]
        public async Task AddCategory_ReturnsCreated_WhenCategoryIsAddedSuccessfully()
        {
            // Arrange
            var model = new CategoryAddModel();
            var category = new CategoryDetailsModel { Id = 1 };
            _categoryServiceMock.Setup(service => service.AddCategoryAsync(model)).ReturnsAsync(category);

            // Act
            var result = await _controller.AddCategory(model);

            // Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
            var createdAtResult = result as CreatedAtActionResult;
            Assert.That(createdAtResult.Value, Is.EqualTo(category));
            _categoryServiceMock.Verify(service => service.AddCategoryAsync(model), Times.Once);
        }

        // Test 5: Check if the "EditCategory" method correctly calls the "EditCategoryAsync" method from the "ICategoryService" and returns the result.
        [Test]
        public async Task EditCategory_ReturnsOk_WhenCategoryIsEditedSuccessfully()
        {
            // Arrange
            var categoryId = 1;
            var model = new CategoryEditModel();
            var category = new CategoryDetailsModel();
            _categoryServiceMock.Setup(service => service.EditCategoryAsync(categoryId, model)).ReturnsAsync(category);

            // Act
            var result = await _controller.EditCategory(categoryId, model);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(category));
            _categoryServiceMock.Verify(service => service.EditCategoryAsync(categoryId, model), Times.Once);
        }

        // Test 6: Check if the "DeleteCategory" method correctly calls the "DeleteCategoryAsync"
        // method from the "ICategoryService" and returns NoContent.
        [Test]
        public async Task DeleteCategory_ReturnsNoContent_WhenCategoryIsDeletedSuccessfully()
        {
            // Arrange
            var categoryId = 1;
            _categoryServiceMock.Setup(service => service.DeleteCategoryAsync(categoryId)).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.DeleteCategory(categoryId);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _categoryServiceMock.Verify(service => service.DeleteCategoryAsync(categoryId), Times.Once);
        }

    }
}
