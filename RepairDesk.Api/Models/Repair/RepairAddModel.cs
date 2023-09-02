using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Repair
{
    public class RepairAddModel
    {
        [Required]
        public DateTime RequestDate { get; set; }
        public string Description { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int POSId { get; set; }
    }
}
