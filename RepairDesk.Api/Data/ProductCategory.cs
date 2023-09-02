using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Data
{
    public class ProductCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

		public bool IsSpecial { get; set; }

		public virtual ICollection<Product> Products { get; set; }

        public string ImagePath { get; set; }
    }
}
