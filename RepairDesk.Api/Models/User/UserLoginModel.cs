using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.User
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
