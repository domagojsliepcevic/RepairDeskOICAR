using Microsoft.EntityFrameworkCore;
using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Notification;
using RepairDesk.Api.Repositories.interfaces;
using RepairDesk.Api.Services.interfaces;

namespace RepairDesk.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;


        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }


        public async Task<PagedResult<NotificationListModel>> GetNotificationsAsync(int pageNr = 1)
        {
            return await _notificationRepository.GetNotificationsAsync(pageNr);
        }

        public async Task<NotificationDetailsModel> GetNotificationAsync(int id)
        {
            return await _notificationRepository.GetNotificationAsync(id);
        }

        public async Task<NotificationDetailsModel> AddNotificationAsync(NotificationAddModel model)
        {
            return await _notificationRepository.AddNotificationAsync(model);
        }

        public async Task<NotificationDetailsModel> EditNotificationAsync(int id, NotificationEditModel model)
        {
            return await _notificationRepository.EditNotificationAsync(id, model);
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            return await _notificationRepository.DeleteNotificationAsync(id);
        }
    }

}
