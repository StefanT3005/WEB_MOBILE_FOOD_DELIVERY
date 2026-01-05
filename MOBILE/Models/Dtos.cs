namespace MOBILE.Models;


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
}

public class RestaurantDto
{
    public int restaurantId { get; set; }
    public string? name { get; set; }
    public string? addressLine { get; set; }
}


public class PlaceOrderItem
{
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
}

public class PlaceOrderRequest
{
    public int CustomerId { get; set; }
    public int RestaurantId { get; set; }
    public string DeliveryAddress { get; set; } = "";
    public List<PlaceOrderItem> Items { get; set; } = new();
}

public class PlaceOrderResponse
{
    public int orderId { get; set; }
    public string status { get; set; } = "";
}