using System.Net.Http.Json;
using MOBILE.Models;
using System.Text;

namespace MOBILE.Services;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<RestaurantDto>> GetRestaurantsAsync()
    {
         var data = await _http.GetFromJsonAsync<List<RestaurantDto>>("api/restaurants");
        return data ?? new List<RestaurantDto>();
    }

    public async Task<List<MenuItemDto>> GetMenuItemsAsync(int restaurantId)
    {
        var data = await _http.GetFromJsonAsync<List<MenuItemDto>>($"api/restaurants/{restaurantId}/menuitems");
        return data ?? new List<MenuItemDto>();
    }

    public async Task<int> PlaceOrderAsync(PlaceOrderRequest request)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(request);
        System.Diagnostics.Debug.WriteLine("REQUEST JSONnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn: " + json);

        var response = await _http.PostAsJsonAsync("api/orders", request);

        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            // aici vei vedea mesajul exact: "Invalid customer or restaurant" sau "No valid menu items"
            throw new Exception($"API error {(int)response.StatusCode}: {body}");
        }

        // daca API-ul intoarce { orderId = ..., status = ... }
        var result = await response.Content.ReadFromJsonAsync<PlaceOrderResponse>();
        return result?.orderId ?? 0;
    }

    public async Task<OrderStatusResponse> GetOrderStatusAsync(int orderId)
    {
        var response = await _http.GetAsync($"api/orders/{orderId}/status");
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new Exception($"API error {(int)response.StatusCode}: {body}");
        }

        var result = await response.Content.ReadFromJsonAsync<OrderStatusResponse>();
        if (result == null) throw new Exception("API returned empty response.");
        return result;
    }

}
