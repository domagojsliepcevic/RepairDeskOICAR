using System.ComponentModel.DataAnnotations.Schema;

namespace RepairDesk.Api.Data
{
    public class Cart : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public int Quantity { get; set; }

        public decimal Total { get; set; }

    }
}
