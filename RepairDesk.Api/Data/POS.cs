using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Data
{
    public class POS : BaseEntity
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
