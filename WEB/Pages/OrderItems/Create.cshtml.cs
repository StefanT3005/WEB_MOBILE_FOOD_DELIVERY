using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodDelivery.Web.Data;
using FoodDelivery.Web.Models;

namespace WEB.Pages.OrderItems
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
        ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "MenuItemId", "Name");
        ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DeliveryAddress");
            return Page();
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderItems.Add(OrderItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
