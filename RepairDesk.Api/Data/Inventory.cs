using System.ComponentModel.DataAnnotations.Schema;

namespace RepairDesk.Api.Data
{
    public class Inventory : BaseEntity
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }

        [ForeignKey(nameof(POS))]
        public int POSId { get; set; }
        public POS POS { get; set; }
    }
}
