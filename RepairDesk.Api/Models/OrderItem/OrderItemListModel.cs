using System;

namespace RepairDesk.Api.Models.OrderItem
{
    public class OrderItemListModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
