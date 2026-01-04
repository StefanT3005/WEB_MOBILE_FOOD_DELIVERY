using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Models;

namespace FoodDelivery.Web.Data
{
    public class FoodDeliveryContext : DbContext
    {
        public FoodDeliveryContext(DbContextOptions<FoodDeliveryContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany(mi => mi.OrderItems)
                .HasForeignKey(oi => oi.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // FIX pentru Reviews: opreste cascade pe minim una din relatii
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // sau NoAction

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(res => res.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade); // sau Restrict, important e sa nu fie ambele Cascade
        }

    }
}
