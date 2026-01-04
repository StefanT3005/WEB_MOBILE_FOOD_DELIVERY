using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Data;
using FoodDelivery.Web.Models;

namespace WEB.Pages.Payments
{
    public class IndexModel : PageModel
    {
        private readonly FoodDelivery.Web.Data.FoodDeliveryContext _context;

        public IndexModel(FoodDelivery.Web.Data.FoodDeliveryContext context)
        {
            _context = context;
        }

        public IList<Payment> Payment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Payment = await _context.Payments
                .Include(p => p.Order).ToListAsync();
        }
    }
}
