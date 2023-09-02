using RepairDesk.Api.Models.OrderItem;
using System.ComponentModel.DataAnnotations;

namespace RepairDesk.Api.Models.Order
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemListModel> OrderItems { get; set; }
		public string PaymentMethod { get; set; }


	}
}
