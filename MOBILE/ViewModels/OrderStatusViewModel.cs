using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MOBILE.Services;

namespace MOBILE.ViewModels;

public class OrderStatusViewModel : INotifyPropertyChanged
{
    private readonly ApiClient _api;

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private int _orderId;
    public int OrderId
    {
        get => _orderId;
        set { _orderId = value; OnPropertyChanged(); }
    }

    private string _statusText = "";
    public string StatusText
    {
        get => _statusText;
        set { _statusText = value; OnPropertyChanged(); }
    }

    private bool _hasResult;
    public bool HasResult
    {
        get => _hasResult;
        set { _hasResult = value; OnPropertyChanged(); }
    }

    public ICommand CheckStatusCommand { get; }

    public OrderStatusViewModel(ApiClient api)
    {
        _api = api;

        CheckStatusCommand = new Command(async () => await CheckAsync());
    }

    private async Task CheckAsync()
    {
        HasResult = false;
        StatusText = "";

        if (OrderId <= 0)
        {
            await Shell.Current.DisplayAlert("Eroare", "Introdu un OrderId valid.", "OK");
            return;
        }

        try
        {
            var res = await _api.GetOrderStatusAsync(OrderId);
            StatusText = $"Comanda #{res.OrderId}: {res.Status}";
            HasResult = true;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Eroare", ex.Message, "OK");
        }
    }
}
