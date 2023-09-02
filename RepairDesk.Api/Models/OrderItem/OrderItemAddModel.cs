using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.OrderItem
{
    public class OrderItemAddModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public decimal Quantity { get; set; }
    }
}
