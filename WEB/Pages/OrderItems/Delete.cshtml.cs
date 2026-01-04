using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Data;
using FoodDelivery.Web.Models;

namespace WEB.Pages.OrderItems
{
    public class DeleteModel : PageModel
    {
        private readonly FoodDelivery.Web.Data.FoodDeliveryContext _context;

        public DeleteModel(FoodDelivery.Web.Data.FoodDeliveryContext context)
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

            var orderitem = await _context.OrderItems.FirstOrDefaultAsync(m => m.OrderItemId == id);

            if (orderitem == null)
            {
                return NotFound();
            }
            else
            {
                OrderItem = orderitem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderitem = await _context.OrderItems.FindAsync(id);
            if (orderitem != null)
            {
                OrderItem = orderitem;
                _context.OrderItems.Remove(OrderItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
