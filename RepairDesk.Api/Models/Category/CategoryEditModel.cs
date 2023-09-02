using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Category
{
    public class CategoryEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

		public bool IsSpecial { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}
