using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Web.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Order>? Orders { get; set; }
    }
}
