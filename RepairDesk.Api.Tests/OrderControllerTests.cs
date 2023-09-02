using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepairDesk.Api.Controllers;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Order;
using RepairDesk.Api.Services.interfaces;
using System.Security.Claims;

namespace RepairDesk.Api.Tests
{
    [TestFixture]
    public class OrderControllerTests
    {
        private Mock<IOrderService> _mockOrderService;
        private Mock<ILogger<OrderController>> _mockLogger;
        private OrderController _controller;
        private ClaimsPrincipal _user;

        [SetUp]
        public void SetUp()
        {
            _mockOrderService = new Mock<IOrderService>();
            _mockLogger = new Mock<ILogger<OrderController>>();
            _controller = new OrderController(_mockLogger.Object, _mockOrderService.Object);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "admin")
            };

            _user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));
            _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = _user };
        }


        // Test 1: GetOrders returns OK when successful
        [Test]
        public async Task GetOrders_ReturnsOk_WhenSuccessful()
        {
            _mockOrderService.Setup(x => x.GetOrdersAsync(It.IsAny<int>())).ReturnsAsync(new PagedResult<OrderListModel>
            {
                Items = new List<OrderListModel>(),
                CurrentPage = 1,
                TotalPages = 1
            });

            var result = await _controller.GetOrders(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }


        // Test 2: GetOrders returns Unauthorized when user is not an admin
        [Test]
        public async Task GetOrders_ReturnsUnauthorized_WhenNotAdmin()
        {
            var userClaims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "1") };
            var user = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "TestAuthentication"));
            _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var result = await _controller.GetOrders(1);

            Assert.That(result, Is.InstanceOf<UnauthorizedResult>());
        }


        // Test 3: GetOrder returns Ok when order exists
        [Test]
        public async Task GetOrder_ReturnsOk_WhenOrderExists()
        {
            _mockOrderService.Setup(x => x.GetOrderAsync(It.IsAny<int>())).ReturnsAsync(new OrderDetailsModel());

            var result = await _controller.GetOrder(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }


        // Test 4: GetOrder returns NotFound when order does not exist
        [Test]
        public async Task GetOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            _mockOrderService.Setup(x => x.GetOrderAsync(It.IsAny<int>())).ReturnsAsync((OrderDetailsModel)null);

            var result = await _controller.GetOrder(1);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }


        // Test 5: DeleteOrder returns NoContent when deletion is successful
        [Test]
        public async Task DeleteOrder_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            _mockOrderService.Setup(x => x.DeleteOrderAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            var result = await _controller.DeleteOrder(1);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }
    }
}