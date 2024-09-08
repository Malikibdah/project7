using E_Commerce.DTO;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MyDbContext _db;
        public OrderController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetCart/{id}")]
        public ActionResult GetCartByUserId(int id)
        {
            var showcart = _db.Carts.Where(x => x.UserId == id).Select(x =>
            new CartDTO
            {
                UserId = x.UserId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                ProductDTO = new ProductDTO
                {
                    ProductName = x.Product.ProductName,
                    Price = x.Product.Price
                }

            }).ToList();

            return Ok(showcart);

        }
        [HttpPost("GetUserAddress")]
        public IActionResult GetUserAddress([FromForm] AddressDTO addressDTO)
        {
            var address = new Address
            {
                UserId = addressDTO.UserId,
                AddressLine = addressDTO.AddressLine,
                City = addressDTO.City,
                Country = addressDTO.Country,
                PhoneNumber = addressDTO.PhoneNumber,
                PostalCode = addressDTO.PostalCode
            };
            _db.Addresses.Add(address);
            _db.SaveChanges();
            return Ok();

        }
        [HttpGet("getaddress/{id}")]
        public ActionResult GetUserAddress(int id)
        {
            var userAddress = _db.Addresses.FirstOrDefault(x => x.UserId == id);
            if (userAddress == null)
            {
                return NotFound("User Address not found");
            }
            return Ok(userAddress);
        }
        [HttpGet("GetAllOrdersByUserId/{id}")]
        public IActionResult GetAllOrders(int id)
        {
            var orders = _db.Orders.Where(x => x.UserId == id).Select(m =>
             new OrderDTO
             {
                 Id = m.Id,
                 OrderDate = m.OrderDate,
                 TotalAmount = m.TotalAmount,
                 Status = m.Status,
                 LoyaltyPoints = m.LoyaltyPoints,
                 Quantity = m.Quantity,

                 ProductOrderDTO = new ProductOrderDTO
                 {
                     ProductName = m.Product.ProductName,
                     Price = m.Product.Price,
                 },
                 UserOrderDTO = new UserOrderDTO
                 {
                     Username = m.User.Username,
                 },
                 Vouchers = new VoucherOrderDTO
                 {
                     Code = _db.Vouchers.Where(c => c.OrderId == m.Id).FirstOrDefault().Code,
                     DiscountAmount = _db.Vouchers.Where(c => c.OrderId == m.Id).FirstOrDefault().DiscountAmount,

                 },
                 
             });
            return Ok(orders);
        }

        [HttpPost("AddNewOrderByUserId")]
        public IActionResult AddNewOrder([FromBody] AddNewOrderByUserId addNewOrderByUserId)
        {
            var cart = _db.Carts.Where(c => c.UserId == addNewOrderByUserId.Id).ToList();

            foreach (var item in cart)
            {

                var productPrice = _db.Products.Where(p => p.Id == item.ProductId).Select(p => p.Price).FirstOrDefault();
                var order = new Order
                {
                    UserId = addNewOrderByUserId.Id,
                    OrderDate = DateTime.Now,
                    TotalAmount = item.Quantity * productPrice,
                    Status = "Pending",
                    LoyaltyPoints = 0,
                    TransactionId = "M123",
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,


                };
                _db.Orders.Add(order);
                _db.SaveChanges();

            }
            return Ok(cart);
        }
        [HttpGet("GetLastOrderIdByUserId/{id}")]
        public IActionResult GetLastOrderIdByUserId(int id )
        { 
            var lastorder = _db.Orders.Where(k => k.UserId == id).OrderByDescending(k => k.OrderDate).FirstOrDefault();
            return Ok(lastorder);
        }


    }
}
