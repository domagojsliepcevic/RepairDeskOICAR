using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.User;
using RepairDesk.Api.Repositories.interfaces;

namespace RepairDesk.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private static readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();


        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId && u.IsActive == true);
        }

        public async Task<PagedResult<UserListModel>> GetUsersAsync(int pageNr = 1)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalItems = await _context.Users.Where(u => u.IsActive == true).CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var users = await _context.Users
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Where(u => u.IsActive == true)
                .Select(user => new UserListModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    City = user.City,
                    ZipCode = user.ZipCode,
                    Country = user.Country,
                    UserTypeId = user.UserTypeId,
                    UserTypeName = user.UserType.Name
                })
                .ToListAsync();

            return new PagedResult<UserListModel>
            {
                Items = users,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }


        public async Task<UserDetailsModel> GetUserAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserType)
                .Where(u => u.Id == userId && u.IsActive == true)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException($"User with id {userId} not found.");
            }

            return new UserDetailsModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                ZipCode = user.ZipCode,
                Country = user.Country,
                UserTypeId = user.UserTypeId,
                UserTypeName = user.UserType.Name
            };
        }

        public async Task<UserDetailsModel> AddUserAsync(UserAddModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                throw new ArgumentException("User with the same email already exists.");
            }
            // Password validation -> TODO
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                throw new ArgumentException("Password cannot be empty or whitespace.");
            }

            var entity = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                City = model.City,
                ZipCode = model.ZipCode,
                Country = model.Country,
                UserTypeId = model.UserTypeId,
                IsActive = true
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return await GetUserAsync(entity.Id);
        }

        public async Task<UserDetailsModel> EditUserAsync(int userId, UserEditModel model)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentException($"User with id {userId} not found.");
            }
            if (userId != model.Id)
            {
                throw new ArgumentException($"Id missmatch.");
            }


            if (await _context.Users.AnyAsync(u => u.Email == model.Email && u.Id != userId))
            {
                throw new ArgumentException("User with the same email already exists.");
            }

            // update data
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.City = model.City;
            user.ZipCode = model.ZipCode;
            user.Country = model.Country;
            //user.IsActive = model.IsActive;
            //user.UserTypeId = model.UserTypeId;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await GetUserAsync(user.Id);
        }

        //public async Task<bool> DeleteUserAsync(int userId)
        //{
        //    var user = await _context.Users.FindAsync(userId);
        //    if (user == null)
        //    {
        //        throw new ArgumentException($"User with id {userId} not found.");
        //    }

        //    _context.Users.Remove(user);

        //    return await _context.SaveChangesAsync() > 0;
        //}

        public async Task<bool> ChangeUserPasswordAsync(int userId, UserChangePasswordModel model)
        {
            if (string.IsNullOrWhiteSpace(model.OldPassword) || string.IsNullOrWhiteSpace(model.NewPassword))
            {
                throw new ArgumentException("Password cannot be empty or whitespace.");
            }

            var user = await _context.Users
                .Where(u => u.Id == userId && u.IsActive == true)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException("User does not exist");
            }
            if (_passwordHasher.VerifyHashedPassword(null, user.Password, model.OldPassword) == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Invalid password");
            }

            user.Password = _passwordHasher.HashPassword(null, model.NewPassword);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CloseAccountAsync(int userId, CloseAccountModel model)
        {
            var user = await _context.Users
                .Include(u => u.UserType)
                .Where(u => u.Id == userId && u.IsActive == true)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException($"User with id {userId} not found.");
            }

            if (_passwordHasher.VerifyHashedPassword(null, user.Password, model.Password) == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Invalid password");
            }

            user.IsActive = false;
            user.ClosedAt = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserDetailsModel> UserRegistrationAsync(UserRegistrationModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                throw new ArgumentException("User with the same email already exists.");
            }
            // Password validation -> TODO
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                throw new ArgumentException("Password cannot be empty or whitespace.");
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = _passwordHasher.HashPassword(null, model.Password),
                IsActive = true,
                UserTypeId = (int)UserTypes.BUYER
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await GetUserAsync(user.Id);
        }

        public async Task<UserDetailsModel> UserLoginAsync(UserLoginModel model)
        {
            var user = await _context.Users
                .Where(u => u.Email == model.Email && u.IsActive == true)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException($"User not found.");
            }

            if (_passwordHasher.VerifyHashedPassword(null, user.Password, model.Password) == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Invalid credentials");
            }

            return await GetUserAsync(user.Id);
        }
    }

}
