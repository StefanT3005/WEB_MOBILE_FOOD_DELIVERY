namespace MOBILE.Services;

public class OrderDraft
{
    public int? RestaurantId { get; set; }
    public int? MenuItemId { get; set; }

    public void Clear()
    {
        RestaurantId = null;
        MenuItemId = null;
    }
}
