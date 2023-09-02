using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.CartItem
{
    public class CartItemAddModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }
    }
}
