using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Web.Data
{
    public class FoodDeliveryIdentityContext : IdentityDbContext
    {
        public FoodDeliveryIdentityContext(DbContextOptions<FoodDeliveryIdentityContext> options)
            : base(options) { }
    }
}
