using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTO
{
    public class VoucherDTO
    {
        
        public string Code { get; set; } = null!;
        [Required]
        public decimal DiscountAmount { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        
        public bool? IsActive { get; set; }
    }
}
