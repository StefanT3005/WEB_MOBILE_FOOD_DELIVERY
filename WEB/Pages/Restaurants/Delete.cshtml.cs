using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Data;
using FoodDelivery.Web.Models;

namespace WEB.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly FoodDelivery.Web.Data.FoodDeliveryContext _context;

        public DeleteModel(FoodDelivery.Web.Data.FoodDeliveryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(m => m.RestaurantId == id);

            if (restaurant == null)
            {
                return NotFound();
            }
            else
            {
                Restaurant = restaurant;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                Restaurant = restaurant;
                _context.Restaurants.Remove(Restaurant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
