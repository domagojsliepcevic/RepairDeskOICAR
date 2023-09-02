using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Data
{
    public class Notification : BaseEntity
    {
        [Required]
        public string Message { get; set; }


        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }

        public virtual NotificationType Type { get; set; }


        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
