using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Data;
using FoodDelivery.Web.Models;

namespace WEB.Pages.MenuItems
{
    public class EditModel : PageModel
    {
        private readonly FoodDelivery.Web.Data.FoodDeliveryContext _context;

        public EditModel(FoodDelivery.Web.Data.FoodDeliveryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MenuItem MenuItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuitem =  await _context.MenuItems.FirstOrDefaultAsync(m => m.MenuItemId == id);
            if (menuitem == null)
            {
                return NotFound();
            }
            MenuItem = menuitem;
           ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "AddressLine");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MenuItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(MenuItem.MenuItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.MenuItemId == id);
        }
    }
}
