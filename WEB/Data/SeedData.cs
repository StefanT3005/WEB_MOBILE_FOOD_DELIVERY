using FoodDelivery.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Web.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(FoodDeliveryContext db)
        {
            // daca ai deja Customers, presupunem ca s-a facut seed
            if (await db.Customers.AnyAsync())
                return;

            // 2–3 Restaurants
            var r1 = new Restaurant { Name = "Pizza Napoli", Phone = "0711000001", AddressLine = "Str. Victoriei 10", City = "Bucuresti", IsOpen = true };
            var r2 = new Restaurant { Name = "Sushi Zen", Phone = "0711000002", AddressLine = "Bd. Unirii 22", City = "Bucuresti", IsOpen = true };
            var r3 = new Restaurant { Name = "Burger House", Phone = "0711000003", AddressLine = "Str. Aviatorilor 5", City = "Cluj-Napoca", IsOpen = false };

            db.Restaurants.AddRange(r1, r2, r3);
            await db.SaveChangesAsync();

            // 6–10 MenuItems
            var mi = new List<MenuItem>
            {
                new MenuItem { RestaurantId = r1.RestaurantId, Name = "Margherita", Price = 32.50m, IsAvailable = true },
                new MenuItem { RestaurantId = r1.RestaurantId, Name = "Diavola", Price = 39.00m, IsAvailable = true },
                new MenuItem { RestaurantId = r1.RestaurantId, Name = "Quattro Formaggi", Price = 41.00m, IsAvailable = false },

                new MenuItem { RestaurantId = r2.RestaurantId, Name = "California Roll", Price = 29.00m, IsAvailable = true },
                new MenuItem { RestaurantId = r2.RestaurantId, Name = "Salmon Nigiri", Price = 34.00m, IsAvailable = true },
                new MenuItem { RestaurantId = r2.RestaurantId, Name = "Miso Soup", Price = 16.00m, IsAvailable = true },

                new MenuItem { RestaurantId = r3.RestaurantId, Name = "Classic Burger", Price = 33.00m, IsAvailable = true },
                new MenuItem { RestaurantId = r3.RestaurantId, Name = "Cheese Fries", Price = 18.00m, IsAvailable = true }
            };

            db.MenuItems.AddRange(mi);
            await db.SaveChangesAsync();

            // 3 Customers
            var c1 = new Customer { Name = "Andrei Pop", Email = "andrei.pop@test.com", Phone = "0722000001", City = "Bucuresti" };
            var c2 = new Customer { Name = "Maria Ionescu", Email = "maria.ionescu@test.com", Phone = "0722000002", City = "Bucuresti" };
            var c3 = new Customer { Name = "Stefan Dumitru", Email = "stefan.dumitru@test.com", Phone = "0722000003", City = "Cluj-Napoca" };

            db.Customers.AddRange(c1, c2, c3);
            await db.SaveChangesAsync();

            // Helper: ia item-uri pe restaurant
            var pizzaItems = await db.MenuItems.Where(x => x.RestaurantId == r1.RestaurantId).ToListAsync();
            var sushiItems = await db.MenuItems.Where(x => x.RestaurantId == r2.RestaurantId).ToListAsync();

            // 3–5 Orders cu OrderItems
            var o1 = new Order
            {
                CustomerId = c1.CustomerId,
                RestaurantId = r1.RestaurantId,
                PlacedAt = DateTime.Now.AddDays(-3),
                DeliveryAddress = "Str. Lalelelor 1, Bucuresti",
                TotalAmount = 0m
            };

            var o2 = new Order
            {
                CustomerId = c2.CustomerId,
                RestaurantId = r2.RestaurantId,
                PlacedAt = DateTime.Now.AddDays(-2),
                DeliveryAddress = "Bd. Decebal 12, Bucuresti",
                TotalAmount = 0m
            };

            var o3 = new Order
            {
                CustomerId = c3.CustomerId,
                RestaurantId = r1.RestaurantId,
                PlacedAt = DateTime.Now.AddDays(-1),
                DeliveryAddress = "Str. Observatorului 20, Cluj-Napoca",
                TotalAmount = 0m
            };

            db.Orders.AddRange(o1, o2, o3);
            await db.SaveChangesAsync();

            var oi = new List<OrderItem>
            {
                new OrderItem { OrderId = o1.OrderId, MenuItemId = pizzaItems[0].MenuItemId, Quantity = 1, UnitPrice = pizzaItems[0].Price },
                new OrderItem { OrderId = o1.OrderId, MenuItemId = pizzaItems[1].MenuItemId, Quantity = 2, UnitPrice = pizzaItems[1].Price },

                new OrderItem { OrderId = o2.OrderId, MenuItemId = sushiItems[0].MenuItemId, Quantity = 2, UnitPrice = sushiItems[0].Price },
                new OrderItem { OrderId = o2.OrderId, MenuItemId = sushiItems[2].MenuItemId, Quantity = 1, UnitPrice = sushiItems[2].Price },

                new OrderItem { OrderId = o3.OrderId, MenuItemId = pizzaItems[0].MenuItemId, Quantity = 1, UnitPrice = pizzaItems[0].Price }
            };

            db.OrderItems.AddRange(oi);
            await db.SaveChangesAsync();

            // calculeaza totalurile
            foreach (var order in new[] { o1, o2, o3 })
            {
                var items = await db.OrderItems.Where(x => x.OrderId == order.OrderId).ToListAsync();
                order.TotalAmount = items.Sum(x => x.UnitPrice * x.Quantity);
            }
            await db.SaveChangesAsync();

            // 2 Payments
            var p1 = new Payment
            {
                OrderId = o1.OrderId,
                Method = "Card",
                Amount = o1.TotalAmount,
                PaidAt = DateTime.Now.AddDays(-3)
            };

            var p2 = new Payment
            {
                OrderId = o2.OrderId,
                Method = "Cash",
                Amount = o2.TotalAmount,
                PaidAt = DateTime.Now.AddDays(-2)
            };

            db.Payments.AddRange(p1, p2);
            await db.SaveChangesAsync();

            // 3 Reviews
            var rev1 = new Review
            {
                CustomerId = c1.CustomerId,
                RestaurantId = r1.RestaurantId,
                Rating = 5,
                Comment = "Super buna pizza, livrare rapida!",
                CreatedAt = DateTime.Now.AddDays(-3)
            };

            var rev2 = new Review
            {
                CustomerId = c2.CustomerId,
                RestaurantId = r2.RestaurantId,
                Rating = 4,
                Comment = "Sushi ok, portii bune.",
                CreatedAt = DateTime.Now.AddDays(-2)
            };

            var rev3 = new Review
            {
                CustomerId = c3.CustomerId,
                RestaurantId = r1.RestaurantId,
                Rating = 5,
                Comment = "Foarte bun, recomand!",
                CreatedAt = DateTime.Now.AddDays(-1)
            };

            db.Reviews.AddRange(rev1, rev2, rev3);
            await db.SaveChangesAsync();
        }
    }
}
