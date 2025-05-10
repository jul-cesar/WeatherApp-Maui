using WeatherLiz.Services;
using WeatherLiz.Models;

namespace WeatherLiz
{
    public partial class MainPage : ContentPage
    {
        private WeatherService _weatherService;

        public MainPage()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var searchText = searchBar.Text;
            Console.WriteLine(searchText);

            if (!string.IsNullOrEmpty(searchText))
            {
                var weatherData = await _weatherService.GetWeather(searchText);


                if (weatherData?.Location != null && weatherData?.Current != null)
                {
                    CityLabel.Text = $"{weatherData.Location.Name}, {weatherData.Location.Region}";
                    TemperatureLabel.Text = $"{weatherData.Current.Temp_C}°C";
                    WeatherIcon.Source = $"https:{weatherData.Current.Condition.Icon}";
                    HumidityLabel.Text = $"Humidity: {weatherData.Current.Humidity}%";
                    WindLabel.Text = $"Wind: {weatherData.Current.WindMph} mph";
                }
                else
                {
                    CityLabel.Text = "Weather data not found.";
                    TemperatureLabel.Text = string.Empty;
                    WeatherIcon.Source = string.Empty;
                    HumidityLabel.Text = string.Empty;
                    WindLabel.Text = string.Empty;
                    Console.WriteLine("Error: No weather data available.");
                }
            }
        }
    }
}
