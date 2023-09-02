using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairDesk.Api.Data
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
        public int Quantity { get; set; }
		public int Rating { get; set; }
		[Required]
		public string LongDescription { get; set; }
        //public virtual ICollection<Image> Images { get; set; }
        public string ImagePath { get; set; }
    }
}
