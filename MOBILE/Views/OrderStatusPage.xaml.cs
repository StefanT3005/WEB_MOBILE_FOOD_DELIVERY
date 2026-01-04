using MOBILE.ViewModels;

namespace MOBILE.Views;

public partial class OrderStatusPage : ContentPage
{
    public OrderStatusPage(OrderStatusViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
