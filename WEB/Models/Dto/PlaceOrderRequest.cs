namespace FoodDelivery.Web.Models.Dto
{
    public class PlaceOrderRequest
    {
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public string DeliveryAddress { get; set; } = "";
        public List<PlaceOrderItemDto> Items { get; set; } = new();
    }

    public class PlaceOrderItemDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
