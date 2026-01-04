using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Web.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required]
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        [Required, Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(300)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
