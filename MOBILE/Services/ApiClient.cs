using System.Net.Http.Json;
using MOBILE.Models;

namespace MOBILE.Services;

public class ApiClient
{
    private readonly HttpClient _http;

    // IMPORTANT: setezi BaseAddress aici
    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<RestaurantDto>> GetRestaurantsAsync()
        => await _http.GetFromJsonAsync<List<RestaurantDto>>("api/restaurants") ?? new();

    public async Task<List<MenuItemDto>> GetMenuItemsAsync(int restaurantId)
        => await _http.GetFromJsonAsync<List<MenuItemDto>>($"api/restaurants/{restaurantId}/menuitems") ?? new();

    public async Task<OrderStatusResponse?> CreateOrderAsync(SimpleOrderRequest req)
    {
        var resp = await _http.PostAsJsonAsync("api/orders/simple", req);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<OrderStatusResponse>();
    }

    public async Task<OrderStatusResponse?> GetOrderStatusAsync(int orderId)
        => await _http.GetFromJsonAsync<OrderStatusResponse>($"api/orders/{orderId}/status");
}
