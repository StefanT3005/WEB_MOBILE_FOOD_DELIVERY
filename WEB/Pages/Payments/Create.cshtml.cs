using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodDelivery.Web.Data;
using FoodDelivery.Web.Models;

namespace WEB.Pages.Payments
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
        ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DeliveryAddress");
            return Page();
        }

        [BindProperty]
        public Payment Payment { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Payments.Add(Payment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
