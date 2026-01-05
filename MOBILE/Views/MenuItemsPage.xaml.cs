using MOBILE.ViewModels;

namespace MOBILE.Views;

[QueryProperty(nameof(RestaurantId), "restaurantId")]
[QueryProperty(nameof(RestaurantName), "restaurantName")]
public partial class MenuItemsPage : ContentPage
{
    private readonly MenuItemsViewModel _vm;

    public string RestaurantName { get; set; } = "";
    public int RestaurantId { get; set; }

    public MenuItemsPage(MenuItemsViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync(RestaurantId, Uri.UnescapeDataString(RestaurantName ?? ""));
    }
}
