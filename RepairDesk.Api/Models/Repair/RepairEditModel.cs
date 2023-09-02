using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Repair
{
    public class RepairEditModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public int StatusId { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime EstimatedRepairFinishDate { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
