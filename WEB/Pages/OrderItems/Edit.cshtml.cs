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

namespace WEB.Pages.OrderItems
{
    public class EditModel : PageModel
    {
        private readonly FoodDelivery.Web.Data.FoodDeliveryContext _context;

        public EditModel(FoodDelivery.Web.Data.FoodDeliveryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderitem =  await _context.OrderItems.FirstOrDefaultAsync(m => m.OrderItemId == id);
            if (orderitem == null)
            {
                return NotFound();
            }
            OrderItem = orderitem;
           ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "MenuItemId", "Name");
           ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DeliveryAddress");
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

            _context.Attach(OrderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(OrderItem.OrderItemId))
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

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.OrderItemId == id);
        }
    }
}
