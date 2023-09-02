using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Inventory
{
    public class InventoryEditModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to zero.")]
        public decimal Quantity { get; set; }
        [Required]
        public int POSId { get; set; }
    }

}
