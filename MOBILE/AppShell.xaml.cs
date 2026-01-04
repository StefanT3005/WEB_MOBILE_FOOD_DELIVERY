using MOBILE.Views;

namespace MOBILE;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(OrderStatusPage), typeof(OrderStatusPage));
    }
}
