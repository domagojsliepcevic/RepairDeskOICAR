using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairDesk.Api.Extensions;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Order;
using RepairDesk.Api.Models.OrderItem;
using RepairDesk.Api.Services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;

namespace RepairDesk.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost("", Name = "AddOrder")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDetailsModel))]
        public async Task<IActionResult> AddOrder([FromBody][Required] OrderAddModel model)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newModel = await _orderService.AddOrderAsync(userId, model);
                return CreatedAtAction(nameof(GetOrder), new { orderId = newModel.Id }, newModel);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new order.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new order.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("user/page/{pageNr}", Name = "GetOrdersForUserPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<OrderListModel>))]
        public async Task<IActionResult> GetOrdersForUser([FromRoute][Required] int pageNr = 1)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var response = await _orderService.GetOrdersByUserAsync(userId, pageNr);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting orders.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting orders.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("page/{pageNr}", Name = "GetOrdersPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<OrderListModel>))]
        public async Task<IActionResult> GetOrders([FromRoute][Required] int pageNr = 1)
        {
            try
            {
                if (!User.IsInRole("admin"))
                {
                    return Unauthorized();
                }

                var response = await _orderService.GetOrdersAsync(pageNr);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting orders.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting orders.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpGet("user/{orderId}", Name = "GetOrderForUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetailsModel))]
        public async Task<IActionResult> GetOrderForUser([FromRoute][Required] int orderId)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var order = await _orderService.GetOrderByUserAsync(userId, orderId);
                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting order with id {orderId}.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting order with id {orderId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{orderId}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetailsModel))]
        public async Task<IActionResult> GetOrder([FromRoute][Required] int orderId)
        {
            try
            {
                if (!User.IsInRole("admin"))
                {
                    return Unauthorized();
                }

                var order = await _orderService.GetOrderAsync(orderId);
                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting order with id {orderId}.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting order with id {orderId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{orderId}", Name="DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOrder([FromRoute][Required] int orderId)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var deleted = await _orderService.DeleteOrderAsync(userId, orderId);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest($"Order with id {orderId} not found.");
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting order with id {orderId}.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting order with id {orderId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{orderId}/items/page/{pageNr}", Name = "GetOrderItemsPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<OrderItemListModel>))]
        public async Task<IActionResult> GetOrderItems([FromRoute][Required] int orderId, [FromRoute] int pageNr = 1)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var response = await _orderService.GetAllOrderItemsForOrderAsync(orderId, pageNr);
                if (response == null || response.Items == null || !response.Items.Any())
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting order items for order with id {orderId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("items/{orderItemId}", Name = "GetOrderItem")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderItemDetailsModel))]
        public async Task<IActionResult> GetOrderItem([FromRoute][Required] int orderItemId)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var item = await _orderService.GetOrderItemAsync(orderItemId);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting order item with id {orderItemId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("{orderId}/items", Name = "AddItemToOrder")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderItemDetailsModel))]
        public async Task<IActionResult> AddItemToOrder([FromRoute][Required] int orderId, [FromBody][Required] OrderItemAddModel model)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var addedModel = await _orderService.AddItemToOrderAsync(orderId, model);
                return CreatedAtAction(nameof(GetOrderItem), new { orderItemId = addedModel.Id }, addedModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding item to order with id {orderId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("items/{orderItemId}", Name = "EditOrderItem")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderItemDetailsModel))]
        public async Task<IActionResult> EditOrderItem([FromRoute][Required] int orderItemId, [FromBody][Required] OrderItemEditModel model)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedModel = await _orderService.EditOrderItemAsync(orderItemId, model);
                if (updatedModel.IsNull())
                {
                    return BadRequest($"Order item with id {orderItemId} not found.");
                }

                return Ok(updatedModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while editing order item with id {orderItemId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("items/{orderItemId}", Name = "DeleteOrderItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOrderItem([FromRoute][Required] int orderItemId)
        {
            try
            { 
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var deleted = await _orderService.DeleteOrderItemAsync(orderItemId);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest($"Order item with id {orderItemId} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting order item with id {orderItemId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("page/{pageNr}/pos/{posId}", Name = "GetOrdersByPOSPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<OrderListModel>))]
        public async Task<IActionResult> GetOrdersByPOS([FromRoute][Required] int posId, [FromRoute][Required] int pageNr = 1)
        {
            try
            {
                var response = await _orderService.GetOrdersByPOSAsync(posId, pageNr);
                if (response == null || response.Items == null || !response.Items.Any())
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting orders.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting orders.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{orderId}/pos/{posId}", Name = "GetOrderByPOS")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetailsModel))]
        public async Task<IActionResult> GetOrderByPOS([FromRoute][Required] int posId, [FromRoute][Required] int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByPOSAsync(posId, orderId);
                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting order with id {orderId}.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting order with id {orderId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("/admin", Name = "AdminAddOrder")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDetailsModel))]
        public async Task<IActionResult> AdminAddOrder([FromBody][Required] OrderAdminAddModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newModel = await _orderService.AdminAddOrderAsync(model);
                return CreatedAtAction(nameof(GetOrder), new { orderId = newModel.Id }, newModel);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new order.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new order.");
                return StatusCode(500, "Internal server error.");
            }
        }

		[HttpPost("/user", Name = "UserAddOrder")]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDetailsModel))]
		public async Task<IActionResult> UserAddOrder([FromBody][Required] OrderUserAddModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
				{
					return Unauthorized();
				}

				var newModel = await _orderService.UserAddOrderAsync(userId, model);
				return CreatedAtAction(nameof(GetOrder), new { orderId = newModel.Id }, newModel);
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, "Error occurred while adding a new order.");
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while adding a new order.");
				return StatusCode(500, "Internal server error.");
			}
		}
	}
}
