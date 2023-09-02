using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Order
{
    public class OrderAddModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]	
		public string PaymentMethod { get; set; }
	}
}
