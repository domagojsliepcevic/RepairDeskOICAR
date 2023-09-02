using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Image
{
    public class ImageRemoveModel
    {
        [Required]
        public string Path { get; set; }
    }
}
