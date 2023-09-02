using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairDesk.Api.Extensions;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.POS;
using RepairDesk.Api.Services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RepairDesk.Api.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class POSController : ControllerBase
    {
        private readonly ILogger<POSController> _logger;
        private readonly IPOSService _posService;

        public POSController(ILogger<POSController> logger, IPOSService posService)
        {
            _logger = logger;
            _posService = posService;
        }

        // GET: api/POS
        [HttpGet("page/{pageNr}", Name = "GetPOSesPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<POSListModel>))]
        public async Task<IActionResult> GetPOSes([FromRoute] int pageNr=1)
        {
            try
            {
                var response = await _posService.GetPOSesAsync(pageNr);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error occurred while getting POSes.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting POSes.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // GET: api/POS/5
        [HttpGet("{posId}", Name = "GetPOS")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(POSDetailsModel))]
        public async Task<IActionResult> GetPOS([FromRoute][Required] int posId)
        {
            try
            {
                var pos = await _posService.GetPOSAsync(posId);
                if (pos.IsNull())
                {
                    return NotFound();
                }

                return Ok(pos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting POS with id {posId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // POST: api/POS
        [HttpPost(Name = "AddPOS")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(POSDetailsModel))]
        public async Task<IActionResult> AddPOS([FromBody][Required] POSAddModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pos = await _posService.AddPOSAsync(model);
                if (pos.IsNull())
                {
                    return BadRequest("POS creation failed.");
                }

                return CreatedAtAction(nameof(GetPOS), new { posId = pos.Id }, pos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new POS.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // PUT: api/POS/5
        [HttpPut("{posId}", Name = "EditPOS")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(POSDetailsModel))]
        public async Task<IActionResult> EditPOS([FromRoute][Required] int posId, [FromBody][Required] POSEditModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pos = await _posService.EditPOSAsync(posId, model);
                if (pos.IsNull())
                {
                    return BadRequest($"POS with id {posId} not found.");
                }

                return Ok(pos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while editing POS with id {posId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // DELETE: api/POS/5
        [HttpDelete("{posId}", Name = "DeletePOS")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePOS([FromRoute][Required] int posId)
        {
            try
            {
                var deleted = await _posService.DeletePOSAsync(posId);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest($"POS with id {posId} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting POS with id {posId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}
