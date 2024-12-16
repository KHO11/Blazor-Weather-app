using System;
using System.Threading.Tasks;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "77dece0cf89c7b83d503b59324078347";
    private const string BaseUrl = "https://api.openweathermap.org/";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherData> GetCurrentWeather(string city)
    {
        var response = await _httpClient.GetFromJsonAsync<WeatherData>(
            $"{BaseUrl}weather?q={city}&appid={ApiKey}&units=metric");
        return response;
    }

    public async Task<ForecastData> GetWeatherForecast(string city)
    {
        var response = await _httpClient.GetFromJsonAsync<ForecastData>(
            $"{BaseUrl}forecast?q={city}&appid={ApiKey}&units=metric");
        return response;
    }
}

