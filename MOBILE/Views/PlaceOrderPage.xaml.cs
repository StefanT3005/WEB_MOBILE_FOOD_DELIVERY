using MOBILE.ViewModels;

namespace MOBILE.Views;

public partial class PlaceOrderPage : ContentPage
{
    public PlaceOrderPage(PlaceOrderViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
