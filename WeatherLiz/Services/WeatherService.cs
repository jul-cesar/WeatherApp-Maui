using System.Text.Json;
using WeatherLiz.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherLiz.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiURL = "https://api.weatherapi.com/v1";
        private const string ApiKey = "8ddba815649e4371bfc74803241504";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherResponse> GetWeather(string city)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{ApiURL}/current.json?key={ApiKey}&q={city}&aqi=no&lang=es");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return new WeatherResponse(); 
                }

                var responseJson = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseJson))
                {
                    Console.WriteLine("Error: Empty response.");
                    return new WeatherResponse(); 
                }

                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, 
                };

                var weatherData = JsonSerializer.Deserialize<WeatherResponse>(responseJson, options);

                if (weatherData == null)
                {
                    Console.WriteLine("Error: Deserialization failed.");
                    return new WeatherResponse(); 
                }

                return weatherData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new WeatherResponse(); 
            }
        }
    }
}
