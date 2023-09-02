using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Data
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [ForeignKey(nameof(UserType))]
        public int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }


        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Repair> Repairs { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? ClosedAt { get; set; }
    }
}
