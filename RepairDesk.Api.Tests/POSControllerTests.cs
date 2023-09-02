using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepairDesk.Api.Controllers;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.POS;
using RepairDesk.Api.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDesk.Api.Tests
{
    [TestFixture]
    public class POSControllerTests
    {
        private Mock<ILogger<POSController>> _loggerMock;
        private Mock<IPOSService> _posServiceMock;
        private POSController _controller;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<POSController>>();
            _posServiceMock = new Mock<IPOSService>();
            _controller = new POSController(_loggerMock.Object, _posServiceMock.Object);
        }

        // Test 1: Check if "GetPOSes" method correctly calls the "GetPOSesAsync" method
        // from the "IPOSService" and returns Ok with the POS list.
        [Test]
        public async Task GetPOSes_ReturnsOk_WhenCalled()
        {
            // Arrange
            int pageNr = 1;
            var posList = new PagedResult<POSListModel>();
            _posServiceMock.Setup(service => service.GetPOSesAsync(pageNr)).ReturnsAsync(posList);

            // Act
            var result = await _controller.GetPOSes(pageNr);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            _posServiceMock.Verify(service => service.GetPOSesAsync(pageNr), Times.Once);
        }

        // Test 2: Check if "GetPOS" method correctly calls the "GetPOSAsync" method
        // from the "IPOSService" and returns Ok with the POS details.
        [Test]
        public async Task GetPOS_ReturnsOk_WhenPOSSuccessfullyFound()
        {
            // Arrange
            int posId = 1;
            var posDetails = new POSDetailsModel();
            _posServiceMock.Setup(service => service.GetPOSAsync(posId)).ReturnsAsync(posDetails);

            // Act
            var result = await _controller.GetPOS(posId);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            _posServiceMock.Verify(service => service.GetPOSAsync(posId), Times.Once);
        }

        // Test 3: Check if "GetPOS" method returns NotFound when POS is not found.
        [Test]
        public async Task GetPOS_ReturnsNotFound_WhenPOSNotFound()
        {
            // Arrange
            int posId = 1;
            _posServiceMock.Setup(service => service.GetPOSAsync(posId)).ReturnsAsync((POSDetailsModel)null);

            // Act
            var result = await _controller.GetPOS(posId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
            _posServiceMock.Verify(service => service.GetPOSAsync(posId), Times.Once);
        }

        // Test 4: Check if "AddPOS" method correctly calls the "AddPOSAsync" method
        // from the "IPOSService" and returns CreatedAtAction.
        [Test]
        public async Task AddPOS_ReturnsCreatedAtAction_WhenSuccessful()
        {
            // Arrange
            var posAddModel = new POSAddModel();
            var posDetails = new POSDetailsModel();
            _posServiceMock.Setup(service => service.AddPOSAsync(posAddModel)).ReturnsAsync(posDetails);

            // Act
            var result = await _controller.AddPOS(posAddModel);

            // Assert
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
            _posServiceMock.Verify(service => service.AddPOSAsync(posAddModel), Times.Once);
        }

        // Test 5: Check if "EditPOS" method correctly calls the "EditPOSAsync" method
        // from the "IPOSService" and returns Ok.
        [Test]
        public async Task EditPOS_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            int posId = 1;
            var posEditModel = new POSEditModel();
            var posDetails = new POSDetailsModel();
            _posServiceMock.Setup(service => service.EditPOSAsync(posId, posEditModel)).ReturnsAsync(posDetails);

            // Act
            var result = await _controller.EditPOS(posId, posEditModel);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            _posServiceMock.Verify(service => service.EditPOSAsync(posId, posEditModel), Times.Once);
        }

        // Test 6: Check if "DeletePOS" method correctly calls the "DeletePOSAsync" method
        // from the "IPOSService" and returns NoContent.
        [Test]
        public async Task DeletePOS_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            int posId = 1;
            _posServiceMock.Setup(service => service.DeletePOSAsync(posId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeletePOS(posId);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _posServiceMock.Verify(service => service.DeletePOSAsync(posId), Times.Once);
        }

    }


}
