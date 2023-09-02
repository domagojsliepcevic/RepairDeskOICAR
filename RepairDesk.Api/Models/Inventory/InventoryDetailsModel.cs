namespace RepairDesk.Api.Models.Inventory
{
    public class InventoryDetailsModel
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int POSId { get; set; }
        public string POSName { get; set; }
    }

}
