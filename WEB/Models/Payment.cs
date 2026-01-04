using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Web.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Required, StringLength(20)]
        public string Method { get; set; } = "Card";

        [Required, Range(0.01, 99999)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}
