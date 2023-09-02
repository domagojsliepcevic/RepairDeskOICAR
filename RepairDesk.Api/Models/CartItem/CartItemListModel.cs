using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.CartItem
{
    public class CartItemListModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
		public string ProductImagePath { get; set; }

		public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
