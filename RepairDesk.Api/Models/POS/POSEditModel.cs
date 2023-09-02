using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.POS
{
    public class POSEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
