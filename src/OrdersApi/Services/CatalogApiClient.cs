using OrdersApi.Models;

namespace OrdersApi.Services;

public class CatalogApiClient
{
    private readonly HttpClient _http;

    public CatalogApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<EventDto?> GetEventAsync(int eventId)
    {
        var response = await _http.GetAsync($"/api/events/{eventId}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<EventDto>();
    }
}
