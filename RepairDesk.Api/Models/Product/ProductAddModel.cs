using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Product
{
    public class ProductAddModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [Range(minimum: 0.01, maximum: (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int CategoryId { get; set; }

		[Required]
        [Range(minimum: 1, maximum: 5)]
		public int Rating { get; set; }

		[Required]
		public string LongDescription { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}
