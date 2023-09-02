using System;

namespace RepairDesk.Api.Models.User
{
    public class UserListModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
    }

}