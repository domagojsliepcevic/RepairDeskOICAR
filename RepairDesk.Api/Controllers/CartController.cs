using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Cart;
using RepairDesk.Api.Models.CartItem;
using RepairDesk.Api.Services.interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;

namespace RepairDesk.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartService _cartService;


        public CartController(ILogger<CartController> logger, ICartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        [HttpGet("", Name ="GetCart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartDetailsModel))]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var cart = await _cartService.GetCartAsync(userId);
                if (cart == null)
                {
                    return NotFound();
                }

                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting cart.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("head", Name="GetCartHead")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartHeadModel))]
        public async Task<IActionResult> GetCartHead()
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var cart = await _cartService.GetCartHeadAsync(userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting cart.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("", Name="ClearCart")]
        [SwaggerResponse("204", typeof(void))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                await _cartService.ClearCartAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while clearing cart.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("items/page/{pageNr}", Name = "GetCartItemsPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<CartItemListModel>))]
        public async Task<IActionResult> GetCartItems([FromRoute] int pageNr = 1)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var response = await _cartService.GetCartItemsForCartAsync(userId, pageNr);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting cart items.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("items/{cartItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartItemDetailsModel))]
        public async Task<IActionResult> GetCartItem([FromRoute][Required] int cartItemId)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var item = await _cartService.GetCartItemAsync(cartItemId);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting cart item with id {cartItemId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("items", Name="AddItemToCart")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CartItemDetailsModel))]
        public async Task<IActionResult> AddItemToCart([FromBody][Required] CartItemAddModel model)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var addedItem = await _cartService.AddItemToCartAsync(userId, model);
                return CreatedAtAction(nameof(GetCartItem), new { cartItemId = addedItem.Id }, addedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding an item to cart.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("items", Name="EditCartItem")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartItemDetailsModel))]
        public async Task<IActionResult> EditCartItem([FromBody][Required] CartItemEditModel model)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var updatedItem = await _cartService.EditCartItemAsync(userId, model);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while editing cart item.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("items/{productId}", Name="DeleteCartItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCartItem([FromRoute][Required] int productId)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                await _cartService.DeleteCartItemAsync(userId, productId);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting cart item.");
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}

