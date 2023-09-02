using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Notification
{
    public class NotificationEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int UserId { get; set; }
    }

}
