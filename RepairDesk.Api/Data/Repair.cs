using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Data
{
    public class Repair : BaseEntity
    {
        [Required]
        public string RepairNumber { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }

        public virtual RepairStatus Status { get; set; }

        public string Description { get; set; }

        public DateTime? FinishDate { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey(nameof(POS))]
        public int POSId { get; set; }
        public virtual POS POS { get; set; }

    }
}
