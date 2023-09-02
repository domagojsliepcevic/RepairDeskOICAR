using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Repair;
using RepairDesk.Api.Services;
using RepairDesk.Api.Services.interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;

namespace RepairDesk.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RepairController : ControllerBase
    {
        private readonly ILogger<RepairController> _logger;
        private readonly IRepairService _repairService;

        public RepairController(ILogger<RepairController> logger, IRepairService repairService)
        {
            _logger = logger;
            _repairService = repairService;
        }

        [HttpGet("user/page/{pageNr}", Name = "GetRepairsForUserPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<RepairListModel>))]
        public async Task<IActionResult> GetRepairsForUser([FromRoute] int pageNr = 1)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier),out int userId))
                {
                    return Unauthorized();
                }

                var repairs = await _repairService.GetRepairsByUserAsync(userId, pageNr);
                return Ok(repairs);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error occurred while getting repairs.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting repairs.");
                return StatusCode(500, "Internal server error.");
            }
        }

		[HttpGet("page/{pageNr}", Name = "GetRepairsPage")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<RepairListModel>))]
		public async Task<IActionResult> GetRepairs([FromRoute] int pageNr = 1)
		{
			try
			{
				if (!User.IsInRole("admin"))
				{
					return Unauthorized();
				}

				var repairs = await _repairService.GetRepairsAsync(pageNr);
				return Ok(repairs);
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, "Error occurred while getting repairs.");
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while getting repairs.");
				return StatusCode(500, "Internal server error.");
			}
		}

		[HttpGet("user/{repairId}", Name = "GetRepairForUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RepairDetailsModel))]
        public async Task<IActionResult> GetRepairForUser([FromRoute][Required] int repairId)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var repair = await _repairService.GetRepairByUserAsync(userId, repairId);
                if (repair == null)
                {
                    return NotFound();
                }
                return Ok(repair);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting repair with id {repairId}.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting repair with id {repairId}.");
                return StatusCode(500, "Internal server error.");
            }
        }

		[HttpGet("{repairId}", Name = "GetRepair")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RepairDetailsModel))]
		public async Task<IActionResult> GetRepair([FromRoute][Required] int repairId)
		{
			try
			{
				if (!User.IsInRole("admin"))
				{
					return Unauthorized();
				}

				var repair = await _repairService.GetRepairAsync(repairId);
				if (repair == null)
				{
					return NotFound();
				}
				return Ok(repair);
			}
			catch (ArgumentException ex)
			{
				_logger.LogError(ex, $"Error occurred while getting repair with id {repairId}.");
				return StatusCode(500, ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error occurred while getting repair with id {repairId}.");
				return StatusCode(500, "Internal server error.");
			}
		}

        [Authorize(Roles = "admin")]
        [HttpPost("", Name = "AddRepair")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RepairDetailsModel))]
        public async Task<IActionResult> AddRepairAsync([FromBody][Required] RepairAddModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var repair = await _repairService.AddRepairAsync(model);
                if (repair == null)
                {
                    return BadRequest("Repair creation failed.");
                }

                return CreatedAtAction(nameof(GetRepair), new { repairId = repair.Id }, repair);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new repair.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new repair.");
                return StatusCode(500, "Internal server error.");
            }
        }

        //[HttpPut("{repairId}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RepairDetailsModel))]
        //public async Task<IActionResult> EditRepairAsync(int repairId, [FromBody][Required] RepairEditModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var repair = await _repairService.EditRepairAsync(repairId, model);
        //        if (repair == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(repair);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error occurred while editing repair with id {repairId}.");
        //        return StatusCode(500, "Internal server error.");
        //    }
        //}

        [HttpDelete("{repairId}", Name = "DeleteRepair")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRepairAsync([FromRoute][Required] int repairId)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var deleted = await _repairService.DeleteRepairAsync(userId, repairId);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting repair with id {repairId}.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting repair with id {repairId}.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [Authorize(Roles = "admin")]
        [HttpGet("page/{pageNr}/pos/{posId}", Name = "GetRepairsByPOSPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<RepairListModel>))]
        public async Task<IActionResult> GetReapirsByPOS([FromRoute][Required] int posId, [FromRoute][Required] int pageNr = 1)
        {
            try
            {
                var response = await _repairService.GetRepairsByPOSAsync(posId, pageNr);
                if (response == null || response.Items == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting repairs.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting repairs.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [Authorize(Roles = "admin")]
        [HttpGet("{repairId}/pos/{posId}", Name = "GetRepairByPOS")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RepairDetailsModel))]
        public async Task<IActionResult> GetRepairByPOS([FromRoute][Required] int posId, [FromRoute][Required] int repairId)
        {
            try
            {
                var repair = await _repairService.GetRepairByPOSAsync(posId, repairId);
                if (repair == null)
                {
                    return NotFound();
                }
                return Ok(repair);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting repair with id {repairId}.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting repair with id {repairId}.");
                return StatusCode(500, "Internal server error.");
            }
        }


    }
}
