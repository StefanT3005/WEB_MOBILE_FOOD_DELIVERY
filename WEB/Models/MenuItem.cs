using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Web.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        public Restaurant? Restaurant { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required, Range(0.01, 9999)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
