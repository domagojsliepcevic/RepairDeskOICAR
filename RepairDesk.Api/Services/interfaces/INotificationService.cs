using RepairDesk.Api.Models;
using RepairDesk.Api.Models.Notification;

namespace RepairDesk.Api.Services.interfaces
{
    public interface INotificationService
    {
        Task<PagedResult<NotificationListModel>> GetNotificationsAsync(int pageNr);
        Task<NotificationDetailsModel> GetNotificationAsync(int notificationId);
        Task<NotificationDetailsModel> AddNotificationAsync(NotificationAddModel model);
        Task<NotificationDetailsModel> EditNotificationAsync(int notificationId, NotificationEditModel model);
        Task<bool> DeleteNotificationAsync(int notificationId);
    }
}
