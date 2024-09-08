using E_Commerce.Models;

namespace E_Commerce.DTO
{
    public class CartDTO
    {
        public int? UserId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual ProductDTO? ProductDTO { get; set; }

    }
}
