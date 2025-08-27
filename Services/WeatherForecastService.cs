using System.Net.Http.Json;
using Client.Models;

namespace Client.Services;

public class WeatherForecastService
{
    private readonly HttpClient _httpClient;

    public WeatherForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherForecast[]?> GetForecastAsync()
    {
        try
        {
            // Add logging to debug the API call
            Console.WriteLine($"Calling API at: {_httpClient.BaseAddress}api/weatherforecast");
            var forecasts = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("api/weatherforecast");
            Console.WriteLine($"Received {forecasts?.Length ?? 0} forecasts from API");
            return forecasts;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching weather data: {ex.Message}");
            // Return empty array instead of null to avoid null reference exceptions
            return Array.Empty<WeatherForecast>();
        }
    }
}