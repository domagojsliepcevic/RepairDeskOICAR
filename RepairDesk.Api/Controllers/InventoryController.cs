using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairDesk.Api.Extensions;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.CartItem;
using RepairDesk.Api.Models.Inventory;
using RepairDesk.Api.Services;
using RepairDesk.Api.Services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RepairDesk.Api.Controllers
{
    //[Authorize(Roles = "admin")]
    //[ApiController]
    //[Route("api/[controller]")]
    //public class InventoryController : ControllerBase
    //{
    //    private readonly ILogger<InventoryController> _logger;
    //    private readonly IInventoryService _inventoryService;


    //    public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
    //    {
    //        _logger = logger;
    //        _inventoryService = inventoryService;
    //    }

    //    [HttpGet("{inventoryId}")]
    //    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventoryDetailsModel))]
    //    public async Task<IActionResult> GetInventory([FromRoute][Required] int inventoryId)
    //    {
    //        try
    //        {
    //            var inventory = await _inventoryService.GetInventoryAsync(inventoryId);

    //            if (inventory == null)
    //            {
    //                return NotFound();
    //            }

    //            return Ok(inventory);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, $"Error occurred while getting inventory with id {inventoryId}.");
    //            return StatusCode(500, "Internal server error.");
    //        }
    //    }

    //    [HttpGet("page/{pageNr}", Name = "GetInventoriesPage")]
    //    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<InventoryListModel>))]
    //    public async Task<IActionResult> GetInventories(int pageNr = 1)
    //    {
    //        try
    //        {
    //            var response = await _inventoryService.GetInventoriesAsync(pageNr);

    //            if (response == null || response.Items == null || !response.Items.Any())
    //            {
    //                return NotFound();
    //            }

    //            return Ok(response);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error occurred while getting inventories.");
    //            return StatusCode(500, "Internal server error.");
    //        }
    //    }

    //    [HttpPost]
    //    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InventoryDetailsModel))]
    //    public async Task<IActionResult> AddInventory([FromBody][Required] InventoryAddModel model)
    //    {
    //        try
    //        {
    //            if (!ModelState.IsValid)
    //            {
    //                return BadRequest(ModelState);
    //            }

    //            var newInventory = await _inventoryService.AddInventoryAsync(model);

    //            return CreatedAtAction(nameof(GetInventory), new { inventoryId = newInventory.Id }, newInventory);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Error occurred while adding a new inventory.");
    //            return StatusCode(500, "Internal server error.");
    //        }
    //    }

    //    [HttpPut("{inventoryId}")]
    //    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InventoryDetailsModel))]
    //    public async Task<IActionResult> EditInventory([FromRoute][Required] int inventoryId, [FromBody][Required] InventoryEditModel model)
    //    {
    //        try
    //        {
    //            if (!ModelState.IsValid || inventoryId != model.Id)
    //            {
    //                return BadRequest(ModelState);
    //            }

    //            var updatedInventory = await _inventoryService.EditInventoryAsync(inventoryId, model);
    //            if (updatedInventory == null)
    //            {
    //                return NotFound();
    //            }

    //            return Ok(updatedInventory);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, $"Error occurred while editing inventory with id {inventoryId}.");
    //            return StatusCode(500, "Internal server error.");
    //        }
    //    }

    //    [HttpDelete("{inventoryId}")]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    public async Task<IActionResult> DeleteInventory([FromRoute][Required] int inventoryId)
    //    {
    //        try
    //        {
    //            var inventory = await _inventoryService.GetInventoryAsync(inventoryId);

    //            if (inventory == null)
    //            {
    //                return NotFound();
    //            }

    //            await _inventoryService.DeleteInventoryAsync(inventoryId);

    //            return NoContent();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, $"Error occurred while deleting inventory with id {inventoryId}.");
    //            return StatusCode(500, "Internal server error.");
    //        }

    //    }


    //}
}
