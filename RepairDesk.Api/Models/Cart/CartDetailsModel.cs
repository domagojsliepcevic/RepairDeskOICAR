using RepairDesk.Api.Models.CartItem;

namespace RepairDesk.Api.Models.Cart
{
    public class CartDetailsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemListModel> CartItems { get; set; }
        public decimal Total { get; set; }
        public int Quantity { get; set; }

    }
}
