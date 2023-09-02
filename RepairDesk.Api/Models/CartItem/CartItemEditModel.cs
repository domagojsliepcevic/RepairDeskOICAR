using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.CartItem
{

    public class CartItemEditModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be equal or greater than zero.")]
        public int Quantity { get; set; }
    }
}
