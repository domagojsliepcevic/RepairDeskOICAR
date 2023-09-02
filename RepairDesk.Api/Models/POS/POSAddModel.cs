using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.POS
{
    public class POSAddModel
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
