namespace MOBILE.Models;

public class RestaurantDto
{
    public int RestaurantId { get; set; }
    public string Name { get; set; } = "";
}

public class MenuItemDto
{
    public int MenuItemId { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}

public class SimpleOrderRequest
{
    public int CustomerId { get; set; }
    public int RestaurantId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public string DeliveryAddress { get; set; } = "";
}

public class OrderStatusResponse
{
    public int OrderId { get; set; }
    public string Status { get; set; } = "";
    public decimal TotalAmount { get; set; }
    public DateTime PlacedAt { get; set; }
}
