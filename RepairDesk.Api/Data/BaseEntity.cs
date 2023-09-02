using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Data
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
