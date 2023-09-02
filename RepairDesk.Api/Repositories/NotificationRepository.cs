using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Data;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Notification;
using RepairDesk.Api.Repositories.interfaces;

namespace RepairDesk.Api.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public NotificationRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> NotificationExistsAsync(int notificationId)
        {
            return await _context.Notifications.AnyAsync(n => n.Id == notificationId);
        }

        public async Task<PagedResult<NotificationListModel>> GetNotificationsAsync(int pageNr = 1)
        {
            int pageSize = int.Parse(_configuration["Pages:Size"]);
            int totalItems = await _context.Notifications.CountAsync();
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            if (pageNr < 1) pageNr = 1;
            if (pageNr > totalPages && pageNr > 1) pageNr = totalPages;

            var notifications = await _context.Notifications
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NotificationListModel
                {
                    Id = n.Id,
                    Message = n.Message,
                    TypeId = n.TypeId,
                    TypeName = n.Type.Name,
                    UserId = n.UserId,
                    UserName = $"{n.User.FirstName} {n.User.LastName}"
                })
                .ToListAsync();

            return new PagedResult<NotificationListModel>
            {
                Items = notifications,
                CurrentPage = pageNr,
                TotalPages = totalPages
            };
        }


        public async Task<NotificationDetailsModel> GetNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications
                .Include(n => n.User)
                .Include(n => n.Type)
                .FirstOrDefaultAsync(n => n.Id == notificationId);
            if (notification == null)
            {
                throw new ArgumentException($"Notification with id {notificationId} not found.");
            }

            return new NotificationDetailsModel
            {
                Id = notification.Id,
                Message = notification.Message,
                TypeId = notification.TypeId,
                TypeName = notification.Type.Name,
                UserId = notification.UserId,
                UserName = $"{notification.User.FirstName} {notification.User.LastName}"
            };
        }

        public async Task<NotificationDetailsModel> AddNotificationAsync(NotificationAddModel model)
        {
            var entity = new Notification
            {
                Message = model.Message,
                TypeId = model.TypeId,
                UserId = model.UserId
            };

            _context.Notifications.Add(entity);
            await _context.SaveChangesAsync();

            return await GetNotificationAsync(entity.Id);
        }

        public async Task<NotificationDetailsModel> EditNotificationAsync(int notificationId, NotificationEditModel model)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null)
            {
                throw new ArgumentException($"Notification with id {notificationId} not found.");
            }
            if (notificationId != model.Id)
            {
                throw new ArgumentException($"Id mismatch.");
            }

            // update data
            notification.Message = model.Message;
            notification.TypeId = model.TypeId;
            notification.UserId = model.UserId;

            _context.Entry(notification).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await GetNotificationAsync(notification.Id);
        }

        public async Task<bool> DeleteNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null)
            {
                throw new ArgumentException($"Notification with id {notificationId} not found.");
            }

            _context.Notifications.Remove(notification);

            return await _context.SaveChangesAsync() > 0;
        }
    }

}
