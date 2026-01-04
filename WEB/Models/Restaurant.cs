using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Web.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required, Phone, StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required, StringLength(120)]
        public string AddressLine { get; set; } = string.Empty;

        [Required, StringLength(60)]
        public string City { get; set; } = string.Empty;

        public bool IsOpen { get; set; } = true;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
