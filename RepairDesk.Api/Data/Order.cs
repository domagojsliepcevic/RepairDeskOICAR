using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepairDesk.Api.Data
{
    public class Order : BaseEntity
    {
        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [ForeignKey(nameof(OrderStatus))]
        public int OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(POS))]
        public int POSId { get; set; }
        public virtual POS POS { get; set; }

		public string PaymentMethod { get; set; }
	}
}
