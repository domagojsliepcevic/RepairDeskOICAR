using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepairDesk.Api.Controllers;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Repair;
using RepairDesk.Api.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepairDesk.Api.Tests
{
    [TestFixture]
    public class RepairControllerTests
    {
        private Mock<ILogger<RepairController>> _loggerMock;
        private Mock<IRepairService> _repairServiceMock;
        private RepairController _controller;
        private ClaimsPrincipal _user;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<RepairController>>();
            _repairServiceMock = new Mock<IRepairService>();
            _controller = new RepairController(_loggerMock.Object, _repairServiceMock.Object);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "admin")
            };

            _user = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));
            _controller.ControllerContext.HttpContext = new DefaultHttpContext { User = _user };
        }

        // Test 1: Check if "GetRepairsForUser" method returns OK when service returns a valid result.
        [Test]
        public async Task GetRepairsForUser_ReturnsOk_WhenServiceReturnsValidResult()
        {
            // Arrange
            int pageNr = 1;
            int userId = 1;
            var repairs = new PagedResult<RepairListModel>();
            _repairServiceMock.Setup(service => service.GetRepairsByUserAsync(userId, pageNr)).ReturnsAsync(repairs);

            // Act
            var result = await _controller.GetRepairsForUser(pageNr);

            // Assert
            Assert.That(result, Is.InstanceOf<ObjectResult>());
            _repairServiceMock.Verify(service => service.GetRepairsByUserAsync(userId, pageNr), Times.Once);
        }

        // Test 2: Check if "GetRepairsForUser" method returns StatusCode 500 when ArgumentException is thrown by the service.
        [Test]
        public async Task GetRepairsForUser_ReturnsStatusCode500_WhenArgumentExceptionThrown()
        {
            // Arrange
            int pageNr = 1;
            int userId = 1;
            _repairServiceMock.Setup(service => service.GetRepairsByUserAsync(userId, pageNr))
                .ThrowsAsync(new ArgumentException());

            // Act
            var result = await _controller.GetRepairsForUser(pageNr);

            // Assert
            Assert.That(result, Is.InstanceOf<ObjectResult>().And.Property("StatusCode").EqualTo(500));
            _repairServiceMock.Verify(service => service.GetRepairsByUserAsync(userId, pageNr), Times.Once);
        }

        // Test 3: Check if "GetRepairForUser" method returns Ok when repair is found.
        [Test]
        public async Task GetRepairForUser_ReturnsOk_WhenRepairIsFound()
        {
            // Arrange
            int repairId = 1;
            int userId = 1;
            var repair = new RepairDetailsModel();
            _repairServiceMock.Setup(service => service.GetRepairByUserAsync(userId, repairId)).ReturnsAsync(repair);

            // Act
            var result = await _controller.GetRepairForUser(repairId);

            // Assert
            Assert.That(result, Is.InstanceOf<ObjectResult>());
            _repairServiceMock.Verify(service => service.GetRepairByUserAsync(userId, repairId), Times.Once);
        }

        // Test 4: Check if "GetRepairForUser" method returns NotFound when no repair is found.
        [Test]
        public async Task GetRepairForUser_ReturnsNotFound_WhenNoRepairIsFound()
        {
            // Arrange
            int repairId = 1;
            int userId = 1;
            _repairServiceMock.Setup(service => service.GetRepairByUserAsync(userId, repairId)).ReturnsAsync((RepairDetailsModel)null);

            // Act
            var result = await _controller.GetRepairForUser(repairId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
            _repairServiceMock.Verify(service => service.GetRepairByUserAsync(userId, repairId), Times.Once);
        }

        // Test 5: Check if "DeleteRepairAsync" method returns NoContent when repair is successfully deleted.
        [Test]
        public async Task DeleteRepairAsync_ReturnsNoContent_WhenRepairIsSuccessfullyDeleted()
        {
            // Arrange
            int repairId = 1;
            int userId = 1;
            _repairServiceMock.Setup(service => service.DeleteRepairAsync(userId, repairId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteRepairAsync(repairId);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _repairServiceMock.Verify(service => service.DeleteRepairAsync(userId, repairId), Times.Once);
        }

        // Test 6: Check if "AddRepairAsync" method returns CreatedAtAction when repair is successfully added.
        [Test]
        public async Task AddRepairAsync_ReturnsCreatedAtAction_WhenRepairIsSuccessfullyAdded()
        {
            // Arrange
            var repairModel = new RepairAddModel();
            var repairDetailsModel = new RepairDetailsModel() { Id = 1 };
            _repairServiceMock.Setup(service => service.AddRepairAsync(repairModel)).ReturnsAsync(repairDetailsModel);

            // Act
            var result = await _controller.AddRepairAsync(repairModel);

            // Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
            _repairServiceMock.Verify(service => service.AddRepairAsync(repairModel), Times.Once);
        }

        // Test 7: Check if "GetRepairs" method returns OK when service returns a valid result.
        [Test]
        public async Task GetRepairs_ReturnsOk_WhenServiceReturnsValidResult()
        {
            // Arrange
            int pageNr = 1;
            var repairs = new PagedResult<RepairListModel>();
            _repairServiceMock.Setup(service => service.GetRepairsAsync(pageNr)).ReturnsAsync(repairs);

            // Act
            var result = await _controller.GetRepairs(pageNr);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            _repairServiceMock.Verify(service => service.GetRepairsAsync(pageNr), Times.Once);
        }

        // Test 8: Check if "GetRepairs" method returns StatusCode 500 when ArgumentException is thrown by the service.
        [Test]
        public async Task GetRepairs_ReturnsStatusCode500_WhenArgumentExceptionThrown()
        {
            // Arrange
            int pageNr = 1;
            _repairServiceMock.Setup(service => service.GetRepairsAsync(pageNr)).ThrowsAsync(new ArgumentException());

            // Act
            var result = await _controller.GetRepairs(pageNr);

            // Assert
            Assert.That(result, Is.InstanceOf<ObjectResult>().And.Property("StatusCode").EqualTo(500));
            _repairServiceMock.Verify(service => service.GetRepairsAsync(pageNr), Times.Once);
        }

    }
}
