using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepairDesk.Api.Controllers;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Order;
using RepairDesk.Api.Models.Product;
using RepairDesk.Api.Services.interfaces;
using System.Security.Claims;

namespace RepairDesk.Api.Tests
{
    public class ProductsControllerTests
    {
        private Mock<IProductService> _mockProductService;
        private ProductsController _controller;
        private Mock<ILogger<ProductsController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockProductService = new Mock<IProductService>();
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(_mockLogger.Object, _mockProductService.Object);
        }

        // Test 6: GetProducts returns OK when successful
        [Test]
        public async Task GetProducts_ReturnsOk_WhenSuccessful()
        {
            _mockProductService.Setup(x => x.GetProductsAsync(It.IsAny<int>()))
                .ReturnsAsync(new PagedResult<ProductListModel>());

            var result = await _controller.GetProducts(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        // Test 7: GetProductsByCategoryId returns OK when successful
        [Test]
        public async Task GetProductsByCategoryId_ReturnsOk_WhenSuccessful()
        {
            _mockProductService.Setup(x => x.GetProductsByCategoryIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PagedResult<ProductListModel>());

            var result = await _controller.GetProductsByCategoryId(1, 1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        // Test 8: GetProduct returns OK when successful
        [Test]
        public async Task GetProduct_ReturnsOk_WhenSuccessful()
        {
            _mockProductService.Setup(x => x.GetProductAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductDetailsModel());

            var result = await _controller.GetProduct(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        // Test 9: GetProduct returns 500 when an exception is thrown
        [Test]
        public async Task GetProduct_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            _mockProductService.Setup(x => x.GetProductAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            var result = await _controller.GetProduct(1);

            Assert.That(result, Is.InstanceOf<ObjectResult>());
            Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(500));
        }

        // Test 10: DeleteProduct returns NoContent when successful
        [Test]
        public async Task DeleteProduct_ReturnsNoContent_WhenSuccessful()
        {
            _mockProductService.Setup(x => x.DeleteProductAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var result = await _controller.DeleteProduct(1);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }
    }

}
