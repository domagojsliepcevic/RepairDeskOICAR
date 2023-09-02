using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Notification
{
    public class NotificationAddModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int UserId { get; set; }
    }

}
