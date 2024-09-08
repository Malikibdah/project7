using E_Commerce.Models;

namespace E_Commerce.DTO
{
    public class AddOrderDTO
    {
        

        public int? UserId { get; set; }

        public string? Status { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public ProductOrderDTO productOrderDTO { get; set; }



    }
   
}
