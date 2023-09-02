using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Product
{
    public class ProductSearchModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public string Brand { get; set; }

        public int? CategoryId { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }
    }
}
