using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Web.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        [Required]
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;

        [Required, StringLength(120)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Payment? Payment { get; set; }
        public Review? Review { get; set; }
    }
}
