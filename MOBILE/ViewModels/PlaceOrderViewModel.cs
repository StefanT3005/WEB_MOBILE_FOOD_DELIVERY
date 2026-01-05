using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MOBILE.Services;
using MOBILE.Models;

namespace MOBILE.ViewModels;

public class PlaceOrderViewModel : INotifyPropertyChanged
{
    private readonly ApiClient _api;
    private readonly OrderDraft _draft;

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private int _customerId;
    public int CustomerId
    {
        get => _customerId;
        set { _customerId = value; OnPropertyChanged(); }
    }

    private string _deliveryAddress = "";
    public string DeliveryAddress
    {
        get => _deliveryAddress;
        set { _deliveryAddress = value; OnPropertyChanged(); }
    }

    private int _quantity = 1;
    public int Quantity
    {
        get => _quantity;
        set { _quantity = value; OnPropertyChanged(); }
    }

    public int? RestaurantId => _draft.RestaurantId;
    public int? MenuItemId => _draft.MenuItemId;

    public ICommand PlaceOrderCommand { get; }

    public PlaceOrderViewModel(ApiClient api, OrderDraft draft)
    {
        _api = api;
        _draft = draft;

        PlaceOrderCommand = new Command(async () => await PlaceOrderAsync());
    }

    private async Task PlaceOrderAsync()
    {
        if (CustomerId <= 0)
        {
            await Shell.Current.DisplayAlert("Eroare", "Completeaza un CustomerId valid (ex: 1).", "OK");
            return;
        }

        if (RestaurantId == null || MenuItemId == null)
        {
            await Shell.Current.DisplayAlert("Eroare", "Selecteaza restaurantul si produsul din meniu inainte.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(DeliveryAddress))
        {
            await Shell.Current.DisplayAlert("Eroare", "Completeaza adresa de livrare.", "OK");
            return;
        }

        if (Quantity <= 0)
        {
            await Shell.Current.DisplayAlert("Eroare", "Cantitatea trebuie sa fie >= 1.", "OK");
            return;
        }

        var req = new PlaceOrderRequest
        {
            CustomerId = CustomerId,
            RestaurantId = RestaurantId.Value,
            DeliveryAddress = DeliveryAddress,
            Items = new List<PlaceOrderItem>
            {
                new PlaceOrderItem { MenuItemId = MenuItemId.Value, Quantity = Quantity }
            }
        };

        try
        {
            var orderId = await _api.PlaceOrderAsync(req);

            await Shell.Current.DisplayAlert("Succes", $"Comanda plasata. OrderId: {orderId}", "OK");

            _draft.Clear();
            DeliveryAddress = "";
            Quantity = 1;

            await Shell.Current.GoToAsync("orderstatus");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Eroare", ex.Message, "OK");
        }
    }
}
