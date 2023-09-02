using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.ProductCategory
{
    public class ProductCategoryAddModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }

}
