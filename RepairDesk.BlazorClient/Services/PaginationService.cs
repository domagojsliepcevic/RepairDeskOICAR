namespace RepairDesk.BlazorClient.Services
{
    public class PaginationService
    {
        public int? ProductInventoryCurrentPage { get; set; } // todo - remove
        public int? AdminProductsCurrentPage { get; set; }
        public int? AdminCategoriesCurrentPage { get; set; }
        public int? AdminPOSesCurrentPage { get; set; }
        public int? AdminRepairsCurrentPage { get; set; }
        public int? AdminOrdersCurrentPage { get; set; }
    }
}
