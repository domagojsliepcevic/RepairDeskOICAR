
using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Product
{
    public class ProductDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
		public int Rating { get; set; }
		public string LongDescription { get; set; }
        public string ImagePath { get; set; }
    }
}
