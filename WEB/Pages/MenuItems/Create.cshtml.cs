using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodDelivery.Web.Data;
using FoodDelivery.Web.Models;

namespace WEB.Pages.MenuItems
{
    public class CreateModel : PageModel
    {
        private readonly FoodDelivery.Web.Data.FoodDeliveryContext _context;

        public CreateModel(FoodDelivery.Web.Data.FoodDeliveryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "AddressLine");
            return Page();
        }

        [BindProperty]
        public MenuItem MenuItem { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MenuItems.Add(MenuItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
