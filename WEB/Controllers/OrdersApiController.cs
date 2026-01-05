using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Data;
using FoodDelivery.Web.Models;
using FoodDelivery.Web.Models.Dto;

namespace FoodDelivery.Web.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersApiController : ControllerBase
    {
        private readonly FoodDeliveryContext _db;

        public OrdersApiController(FoodDeliveryContext db)
        {
            _db = db;
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("ok");

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request)
        {
            var customer = await _db.Customers.FindAsync(request.CustomerId);
            var restaurant = await _db.Restaurants.FindAsync(request.RestaurantId);

            if (customer == null || restaurant == null)
                return BadRequest("Invalid customer or restaurant");

            var menuItemIds = request.Items.Select(i => i.MenuItemId).ToList();
            var menuItems = await _db.MenuItems
                .Where(m => menuItemIds.Contains(m.MenuItemId))
                .ToListAsync();

            if (!menuItems.Any())
                return BadRequest("No valid menu items");

            var order = new Order
            {
                CustomerId = request.CustomerId,
                RestaurantId = request.RestaurantId,
                DeliveryAddress = request.DeliveryAddress,
                PlacedAt = DateTime.Now,
                Status = "InPregatire",
                TotalAmount = 0
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            foreach (var item in request.Items)
            {
                var menuItem = menuItems.First(m => m.MenuItemId == item.MenuItemId);

                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    MenuItemId = menuItem.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = menuItem.Price
                };

                order.TotalAmount += menuItem.Price * item.Quantity;
                _db.OrderItems.Add(orderItem);
            }

            await _db.SaveChangesAsync();

            return Ok(new
            {
                orderId = order.OrderId,
                status = order.Status
            });
        }

        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetStatus(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            return Ok(new
            {
                orderId = order.OrderId,
                status = order.Status
            });
        }
    }
}
