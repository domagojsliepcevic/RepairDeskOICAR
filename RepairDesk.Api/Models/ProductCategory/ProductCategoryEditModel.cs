using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.ProductCategory
{
    public class ProductCategoryEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}


