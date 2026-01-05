using System.Collections.ObjectModel;
using System.Windows.Input;
using MOBILE.Models;
using MOBILE.Services;

namespace MOBILE.ViewModels;

public class RestaurantsViewModel
{
    private readonly ApiClient _api;
    private readonly OrderDraft _draft;

    public ObservableCollection<RestaurantDto> Restaurants { get; } = new();

    public ICommand RefreshCommand { get; }
    public ICommand OpenMenuCommand { get; }

    public RestaurantsViewModel(ApiClient api, OrderDraft draft)
    {
        _api = api;
        _draft = draft;

        RefreshCommand = new Command(async () => await LoadAsync());

        OpenMenuCommand = new Command<RestaurantDto>(async r =>
        {
            if (r == null) return;

            // tinem selectia in draft global
            _draft.RestaurantId = r.restaurantId;

            await Shell.Current.GoToAsync(
                $"menuitems?restaurantId={r.restaurantId}&restaurantName={Uri.EscapeDataString(r.name)}"
            );
        });
    }

    public async Task LoadAsync()
    {
        Restaurants.Clear();
        var list = await _api.GetRestaurantsAsync();
        foreach (var r in list)
            Restaurants.Add(r);
    }
}
