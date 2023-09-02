using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Data
{
    public class UserType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
