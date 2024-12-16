using System.Net.Http.Json;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "77dece0cf89c7b83d503b59324078347"; // Your API key

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherForecastResponse> GetWeatherForecastAsync(string city, int count)
    {
        try
        {
            // Construct the API call
            var response = await _httpClient.GetAsync($"data/2.5/forecast/daily?q={city}&cnt={count}&appid={ApiKey}&units=metric");
            response.EnsureSuccessStatusCode();

            // Deserialize and return the forecast data
            return await response.Content.ReadFromJsonAsync<WeatherForecastResponse>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            throw;
        }
    }
}

// Models for the API response
public class WeatherForecastResponse
{
    public CityInfo City { get; set; }
    public List<DailyForecast> List { get; set; }
}

public class CityInfo
{
    public string Name { get; set; }
}

public class DailyForecast
{
    public Temp Temp { get; set; }
    public List<Weather> Weather { get; set; }

    public string WeatherDescription => Weather?[0]?.Description ?? "N/A"; // Null-safe description
}

public class Temp
{
    public double Day { get; set; }
    public double Night { get; set; }
}

public class Weather
{
    public string Description { get; set; }
    public string Icon { get; set; }
}
