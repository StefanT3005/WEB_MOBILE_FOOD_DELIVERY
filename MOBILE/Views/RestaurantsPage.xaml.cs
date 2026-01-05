using MOBILE.ViewModels;

namespace MOBILE.Views;

public partial class RestaurantsPage : ContentPage
{
    private readonly RestaurantsViewModel _vm;

    public RestaurantsPage(RestaurantsViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_vm.Restaurants.Count == 0)
            await _vm.LoadAsync();
    }
}
