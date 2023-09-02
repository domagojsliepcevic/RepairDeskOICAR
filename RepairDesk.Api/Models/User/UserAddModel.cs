﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.User
{
    public class UserAddModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
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

        [Required]
        public int UserTypeId { get; set; }
    }

}