using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Data;

namespace FoodDelivery.Web.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsApiController : ControllerBase
    {
        private readonly FoodDeliveryContext _db;

        public RestaurantsApiController(FoodDeliveryContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _db.Restaurants
                .Select(r => new
                {
                    r.RestaurantId,
                    r.Name,
                    r.AddressLine
                })
                .ToListAsync();

            return Ok(restaurants);
        }

        [HttpGet("{id}/menuitems")]
        public async Task<IActionResult> GetMenuItems(int id)
        {
            var exists = await _db.Restaurants.AnyAsync(r => r.RestaurantId == id);
            if (!exists)
                return NotFound();

            var menuItems = await _db.MenuItems
                .Where(m => m.RestaurantId == id)
                .Select(m => new
                {
                    m.MenuItemId,
                    m.Name,
                    m.Price
                })
                .ToListAsync();

            return Ok(menuItems);
        }
    }
}
