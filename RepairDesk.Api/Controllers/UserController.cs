using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairDesk.Api.Models.User;
using RepairDesk.Api.Services.interfaces;
using RepairDesk.Api.Extensions;
using System.ComponentModel.DataAnnotations;
using RepairDesk.Api.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace RepairDesk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("page/{pageNr}", Name="GetUsersPage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<UserListModel>))]
        public async Task<IActionResult> GetUsers([FromRoute] int pageNr = 1)
        {
            try
            {
                var response = await _userService.GetUsersAsync(pageNr);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting users.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize]
        [HttpGet("", Name="GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDetailsModel))]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var user = await _userService.GetUserAsync(userId);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while getting user data.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting user data.");
                return StatusCode(500, "Internal server error.");
            }
        }

        //[HttpPost(Name = "AddUser")]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDetailsModel))]
        //public async Task<IActionResult> AddUser([FromBody][Required] UserAddModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var user = await _userService.AddUserAsync(model);
        //        if (user.IsNull())
        //        {
        //            return BadRequest("User creation failed.");
        //        }

        //        return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while adding a new user.");
        //        return StatusCode(500, "Internal server error.");
        //    }
        //    //return await _userService.AddUserAsync(model);
        //}

        [Authorize]
        [HttpPut("", Name = "EditUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDetailsModel))]
        public async Task<IActionResult> EditUser([FromBody][Required] UserEditModel model)
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

                var user = await _userService.EditUserAsync(userId, model);
                if (user.IsNull())
                {
                    return BadRequest($"User with id {userId} not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while editing user.");
                return StatusCode(500, "Internal server error.");
            }
        }

        //[HttpDelete("{userId}", Name = "DeleteUser")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> DeleteUser([FromRoute][Required] int userId)
        //{
        //    try
        //    {
        //        var deleted = await _userService.DeleteUserAsync(userId);
        //        if (deleted)
        //        {
        //            return NoContent();
        //        }
        //        else
        //        {
        //            return BadRequest($"User with id {userId} not found.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error occurred while deleting user with id {userId}.");
        //        return StatusCode(500, "Internal server error.");
        //    }
        //}

        [Authorize]
        [HttpDelete("", Name = "CloseAccount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CloseAccount([FromBody][Required] CloseAccountModel model)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return Unauthorized();
                }

                var success = await _userService.CloseAccountAsync(userId, model);
                if (!success)
                {
                    return BadRequest($"User with id {userId} not found.");
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting user.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting user.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize]
        [HttpPut("password/change", Name = "ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> ChangePassword([FromBody][Required] UserChangePasswordModel model)
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

                var success = await _userService.ChangeUserPasswordAsync(userId, model);
                if (!success)
                {
                    return BadRequest($"Password was not changed.");
                }

                return Ok(success);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Error occurred while changing password for user.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while changing password for user.");
                return StatusCode(500, $"Error occurred while changing password for user.");
            }
        }


        [HttpPost("login", Name = "UserLogin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenModel))]
        public async Task<IActionResult> Login([FromBody][Required] UserLoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var token = await _userService.UserLoginAsync(model);
                if (token.IsNull())
                {
                    return Unauthorized();
                }
                return Ok(token);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error occurred while logging user.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging user.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost("registration", Name = "UserRegistration")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDetailsModel))]
        public async Task<IActionResult> Registration([FromBody][Required] UserRegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userService.UserRegistrationAsync(model);
                if (user.IsNull())
                {
                    return BadRequest("User registration failed.");
                }

                return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error occurred while registering user.");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user.");
                return StatusCode(500, "Internal server error.");
            }
        }



    }
}
