using E_Commerce.Models;

namespace E_Commerce.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }

        public int? LoyaltyPoints { get; set; }

        public int? Quantity { get; set; }

        public virtual ProductOrderDTO? ProductOrderDTO { get; set; }

        public virtual UserOrderDTO? UserOrderDTO { get; set; }

        public virtual VoucherOrderDTO? Vouchers { get; set; }

        
    }
}
