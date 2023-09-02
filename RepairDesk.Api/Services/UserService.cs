using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RepairDesk.Api.Data;
using RepairDesk.Api.Extensions;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.User;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services.interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepairDesk.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<PagedResult<UserListModel>> GetUsersAsync(int pageNr = 1)
        {
            return await _userRepository.GetUsersAsync(pageNr);
        }

        public async Task<UserDetailsModel> GetUserAsync(int userId)
        {
            return await _userRepository.GetUserAsync(userId);
        }

        public async Task<UserDetailsModel> AddUserAsync(UserAddModel model)
        {
            return await _userRepository.AddUserAsync(model);
        }

        public async Task<UserDetailsModel> EditUserAsync(int userId, UserEditModel model)
        {
            return await _userRepository.EditUserAsync(userId, model);
        }

        public async Task<bool> ChangeUserPasswordAsync(int userId, UserChangePasswordModel model)
        {
            return await _userRepository.ChangeUserPasswordAsync(userId, model);
        }

        public async Task<bool> CloseAccountAsync(int userId, CloseAccountModel model)
        {
            return await _userRepository.CloseAccountAsync(userId, model);
        }

        public async Task<UserDetailsModel> UserRegistrationAsync(UserRegistrationModel model)
        {
            return await _userRepository.UserRegistrationAsync(model);
        }

        public async Task<TokenModel> UserLoginAsync(UserLoginModel model)
        {
            var user = await _userRepository.UserLoginAsync(model);
            if (user.IsNotNull())
            {
                return GenerateJwtTokenForUser(user);
            }

            return null;
        }

        private TokenModel GenerateJwtTokenForUser(UserDetailsModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtSettings:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, $"{model.Id}"),
                        new Claim(ClaimTypes.Name, $"{model.FirstName} {model.LastName}"),
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.Role, model.UserTypeName.ToLower().Trim())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenModel
            {
                Token = tokenHandler.WriteToken(token)
            };
        }

    }

}
