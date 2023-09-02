using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Category
{
    public class CategoryAddModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

		public bool IsSpecial { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}
