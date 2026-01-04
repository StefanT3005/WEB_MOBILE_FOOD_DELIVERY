using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Data;
using FoodDelivery.Web.Models;

namespace WEB.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly FoodDeliveryContext _context;

        public IndexModel(FoodDeliveryContext context)
        {
            _context = context;
        }

        public IList<Review> Review { get; set; } = new List<Review>();

        public async Task OnGetAsync()
        {
            Review = await _context.Reviews
                .AsNoTracking()
                .ToListAsync();
        }
    }

}
