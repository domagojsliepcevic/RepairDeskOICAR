using System.Globalization;

namespace RepairDesk.BlazorClient.Services
{
    public class ProductSearchService
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int? CategoryId { get; set; }
        public string Brand { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

        public void Reset()
        {
            Name = null;
            Description = null;
            Text = null;
            CategoryId = null;
            Brand = null;
            PriceFrom = null;
            PriceTo = null;
        }

        public string GenerateQueryString()
        {
            string categoryId = (CategoryId.HasValue && CategoryId > 0) ? CategoryId.Value.ToString() : "";
            string brand = !string.IsNullOrWhiteSpace(Brand) ? Uri.EscapeDataString(Brand) : "";
            string priceFrom = PriceFrom.HasValue ? PriceFrom.Value.ToString("0.00", CultureInfo.InvariantCulture) : "";
            string priceTo = PriceTo.HasValue ? PriceTo.Value.ToString("0.00", CultureInfo.InvariantCulture) : "";
            string queryString = $"categoryid={categoryId}&brand={brand}&pricefrom={priceFrom}&priceto={priceTo}";

            return queryString;
        }
    }
}
