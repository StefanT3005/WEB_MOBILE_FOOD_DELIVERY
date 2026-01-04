using FoodDelivery.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB.Data; 

namespace WEB.Pages
{
    public class IndexModel : PageModel
    {
        
        private readonly FoodDeliveryContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(FoodDeliveryContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int TotalRestaurants { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public double AverageRating { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                
                TotalRestaurants = await _context.Restaurants.CountAsync();
                TotalOrders = await _context.Orders.CountAsync();

                TotalRevenue = await _context.Orders.AnyAsync()
                    ? await _context.Orders.SumAsync(o => o.TotalAmount)
                    : 0;

                var reviews = await _context.Reviews.ToListAsync();
                AverageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la incarcarea statisticilor din FoodDeliveryContext.");
            }
        }
    }
}