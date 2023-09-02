using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Data
{
    public class NotificationType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
