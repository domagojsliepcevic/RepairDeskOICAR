using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairDesk.Api.Models.Stats;
using RepairDesk.Api.Services.interfaces;
using System.Security.Claims;

namespace RepairDesk.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IStatsService _statsService;

        public StatsController(ILogger<CartController> logger, IStatsService statsService)
        {
            _logger = logger;
            _statsService = statsService;
        }


        [Authorize(Roles = "admin")]
        [HttpGet("", Name = "GetStats")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StatsModel))]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var stats = await _statsService.GetStatsAsync();
                return Ok(stats);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting stats data.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting stats data.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
