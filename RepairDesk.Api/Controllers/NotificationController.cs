using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepairDesk.Api.Extensions;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Notification;
using RepairDesk.Api.Services.interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;

namespace RepairDesk.Api.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly INotificationService _notificationService;

        public NotificationController(ILogger<NotificationController> logger, INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet("page/{pageNr}", Name = "GetNotificationsPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<NotificationListModel>))]
        public async Task<IActionResult> GetNotifications(int pageNr = 1)
        {
            try
            {
                var response = await _notificationService.GetNotificationsAsync(pageNr);

                if (response == null || response.Items == null || !response.Items.Any())
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting notifications.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{notificationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationDetailsModel))]
        public async Task<IActionResult> GetNotification([FromRoute][Required] int notificationId)
        {
            try
            {
                var notification = await _notificationService.GetNotificationAsync(notificationId);

                if (notification.IsNull())
                {
                    return NotFound();
                }

                return Ok(notification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting notification with id {notificationId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NotificationDetailsModel))]
        public async Task<IActionResult> AddNotification([FromBody][Required] NotificationAddModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newNotification = await _notificationService.AddNotificationAsync(model);
                return CreatedAtAction(nameof(GetNotification), new { notificationId = newNotification.Id }, newNotification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new notification.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{notificationId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationDetailsModel))]
        public async Task<IActionResult> EditNotification([FromRoute][Required] int notificationId, [FromBody][Required] NotificationEditModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedNotification = await _notificationService.EditNotificationAsync(notificationId, model);

                if (updatedNotification.IsNull())
                {
                    return BadRequest($"Notification with id {notificationId} not found.");
                }

                return Ok(updatedNotification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while editing notification with id {notificationId}.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpDelete("{notificationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteNotification([FromRoute][Required] int notificationId)
        {
            try
            {
                var deleted = await _notificationService.DeleteNotificationAsync(notificationId);

                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest($"Notification with id {notificationId} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting notification with id {notificationId}.");
                return StatusCode(500, "Internal server error.");
            }
        }


    }
}
