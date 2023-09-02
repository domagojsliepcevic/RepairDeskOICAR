using RepairDesk.Api.Models;
using RepairDesk.Api.Models.User;

namespace RepairDesk.Api.Services.interfaces
{
    public interface IUserService
    {
        Task<PagedResult<UserListModel>> GetUsersAsync(int pageNr);
        Task<UserDetailsModel> GetUserAsync(int userId);
        Task<UserDetailsModel> AddUserAsync(UserAddModel model);
        Task<UserDetailsModel> EditUserAsync(int userId, UserEditModel model);
        Task<bool> ChangeUserPasswordAsync(int userId, UserChangePasswordModel model);
        Task<bool> CloseAccountAsync(int userId, CloseAccountModel model);
        Task<UserDetailsModel> UserRegistrationAsync(UserRegistrationModel model);
        Task<TokenModel> UserLoginAsync(UserLoginModel model);
    }
}
