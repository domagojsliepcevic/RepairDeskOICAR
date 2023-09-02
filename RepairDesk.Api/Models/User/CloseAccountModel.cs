using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.User
{
    public class CloseAccountModel
    {
        [Required]
        public string Password { get; set; }
    }
}
