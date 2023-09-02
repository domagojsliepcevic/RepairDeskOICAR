using RepairDesk.Api.Models.OrderItem;
using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Order
{
    public class OrderAdminAddModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public List<OrderItemAddModel> OrderItems { get; set; }
        [Required]
        public int POSId { get; set; }

    }
}
