namespace RepairDesk.Api.Models.Cart
{
    public class CartListModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Total { get; set; }
        public int Quantity { get; set; }
    }
}
