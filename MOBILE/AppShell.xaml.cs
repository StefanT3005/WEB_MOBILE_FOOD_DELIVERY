using MOBILE.Views;

namespace MOBILE;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("menuitems", typeof(MenuItemsPage));
        Routing.RegisterRoute("placeorder", typeof(PlaceOrderPage));
        Routing.RegisterRoute("orderstatus", typeof(OrderStatusPage));
    }
}
