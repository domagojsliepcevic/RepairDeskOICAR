namespace RepairDesk.Api.Models.OrderItem
{
    public class OrderItemDetailsModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
