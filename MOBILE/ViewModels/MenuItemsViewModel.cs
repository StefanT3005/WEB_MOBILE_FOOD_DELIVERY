using System.Collections.ObjectModel;
using System.Windows.Input;
using MOBILE.Models;
using MOBILE.Services;

namespace MOBILE.ViewModels;

public class MenuItemsViewModel
{
    private readonly ApiClient _api;
    private readonly OrderDraft _draft;

    public ObservableCollection<MenuItemDto> Items { get; } = new();

    public int RestaurantId { get; private set; }
    public string RestaurantName { get; private set; } = "";

    public ICommand LoadCommand { get; }
    public ICommand PickItemCommand { get; }


    public MenuItemsViewModel(ApiClient api, OrderDraft draft)
    {
        _api = api;
        _draft = draft;

        PickItemCommand = new Command<MenuItemDto>(async item =>
        {
            if (item == null) return;

            _draft.RestaurantId = RestaurantId;
            _draft.MenuItemId = item.MenuItemId;

            await Shell.Current.GoToAsync("placeorder");
        });
    }


    public async Task LoadAsync(int restaurantId, string restaurantName = "")
    {
        RestaurantId = restaurantId;
        RestaurantName = restaurantName;

        // optional: tinem si restaurantul selectat in draft
        _draft.RestaurantId = restaurantId;

        Items.Clear();
        var list = await _api.GetMenuItemsAsync(restaurantId);
        foreach (var it in list)
            Items.Add(it);
    }
}
