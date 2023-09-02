using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Image
{
    public class ImageAddModel
    {
        [Required]
        public string Path { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
