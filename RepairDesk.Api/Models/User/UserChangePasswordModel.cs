using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.User
{
    public class UserChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

    }
}
