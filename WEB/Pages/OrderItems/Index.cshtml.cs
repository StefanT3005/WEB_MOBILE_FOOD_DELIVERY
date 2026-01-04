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
    public class IndexModel : PageModel
    {
        private readonly FoodDelivery.Web.Data.FoodDeliveryContext _context;

        public IndexModel(FoodDelivery.Web.Data.FoodDeliveryContext context)
        {
            _context = context;
        }

        public IList<OrderItem> OrderItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            OrderItem = await _context.OrderItems
                .Include(o => o.MenuItem)
                .Include(o => o.Order).ToListAsync();
        }
    }
}
