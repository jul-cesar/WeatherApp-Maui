using WeatherLiz.Services;
using WeatherLiz.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace WeatherLiz
{
    public partial class MainPage : ContentPage
    {
        private WeatherService _weatherService;
        private LocalDbService _db;

       
        public ObservableCollection<Favoritos> FavoritesList { get; set; } = new ObservableCollection<Favoritos>();
    

        public MainPage()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
            _db = new LocalDbService();
           BindingContext = this; 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFavorites();
        }

        private async Task LoadFavorites()
        {
            var favorites = await _db.GetFavoritos();  
            FavoritesList.Clear();
            foreach (var favorite in favorites)
            {
                FavoritesList.Add(favorite);
            }
        }

        

        private async void AddToFavoritesButton_Clicked(object sender, EventArgs e)
        {
            var newFavorite = new Favoritos
            {
                City = CityLabel.Text,
                Country = CountryLabel.Text,
                Description = DescriptionLabel.Text,
                Icon = WeatherIcon.Source.ToString(),
                Temperature = TemperatureLabel.Text,
                Time = TimeLabel.Text
            };

       
            await _db.Agregar(newFavorite);

        
            FavoritesList.Add(newFavorite);

            AddToFavoritesButton.IsVisible = false;

            DisplayAlert("Favoritos", "Ciudad agregada a favoritos", "OK");
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            InitialLayout.IsVisible = false;
            WeatherLayout.IsVisible = true;

            var searchText = searchBar.Text;

            if (!string.IsNullOrEmpty(searchText))
            {
                
                var weatherData = await _weatherService.GetWeather(searchText);
           

                
                var cityKey = $"{weatherData?.Location?.Name}, {weatherData?.Location?.Region}";

               
                var existingFavorite = await _db.GetOneFavorito(cityKey);

                if (existingFavorite != null && weatherData?.Location != null && weatherData?.Current != null)
                {
                
                    AddToFavoritesButton.IsVisible = false;

                    var updatedFavorite = new Favoritos
                    {
                        City = cityKey,
                        Country = weatherData.Location.Country,
                        Description = weatherData.Current.Condition.Text,
                        Icon = $"https:{weatherData.Current.Condition.Icon}",
                        Temperature = $"{weatherData.Current.Temp_C}°C",
                        Time = weatherData.Location.Localtime
                    };

                    
                    await _db.UpdateFavorito(updatedFavorite);

               
                    await LoadFavorites();

                    DisplayAlert("Favoritos", "Ciudad actualizada en favoritos", "OK");
                }
                else
                {
                   
                    AddToFavoritesButton.IsVisible = true;
                }

             
                if (weatherData?.Location != null && weatherData?.Current != null)
                {
                    CityLabel.Text = cityKey;
                    TimeLabel.Text = weatherData.Location.Localtime;
                    TemperatureLabel.Text = $"{weatherData.Current.Temp_C}°C";

                   
                    string iconSource = GetWeatherIcon(weatherData.Current.Condition.Code);
                    WeatherIcon.Source = iconSource;

                    DescriptionLabel.Text = weatherData.Current.Condition.Text;
                    CountryLabel.Text = weatherData.Location.Country;
                }
                else
                {
                    AddToFavoritesButton.IsVisible = false;

                    CityLabel.Text = "Ciudad no encontrada.";
                    TemperatureLabel.Text = string.Empty;
                    WeatherIcon.Source = string.Empty;
                    DescriptionLabel.Text = string.Empty;
                    TimeLabel.Text = string.Empty;
                    CountryLabel.Text = string.Empty;
                }
            }
        }

     
        private string GetWeatherIcon(int conditionCode)
        {
         
            var weatherIcons = new Dictionary<int, string>
    {
        { 1000, "clear.png" },
        { 1003, "clouds.png" },
        { 1006, "clouds.png" },
        { 1009, "clouds.png" },
        { 1030, "mist.png" },
        { 1063, "rain.png" },
        { 1066, "snow.png" },
        { 1069, "snow.png" },
        { 1072, "drizzle.png" },
        { 1087, "rain.png" },
        { 1114, "snow.png" },
        { 1117, "snow.png" },
        { 1135, "mist.png" },
        { 1147, "mist.png" },
        { 1150, "drizzle.png" },
        { 1153, "drizzle.png" },
        { 1168, "drizzle.png" },
        { 1171, "drizzle.png" },
        { 1180, "rain.png" },
        { 1183, "rain.png" },
        { 1186, "rain.png" },
        { 1189, "rain.png" },
        { 1192, "rain.png" },
        { 1195, "rain.png" },
        { 1198, "snow.png" },
        { 1201, "snow.png" },
        { 1204, "snow.png" },
        { 1207, "snow.png" },
        { 1210, "snow.png" },
        { 1213, "snow.png" },
        { 1216, "snow.png" },
        { 1219, "snow.png" },
        { 1222, "snow.png" },
        { 1225, "snow.png" },
        { 1237, "snow.png" },
        { 1240, "rain.png" },
        { 1243, "rain.png" },
        { 1246, "rain.png" },
        { 1249, "snow.png" },
        { 1252, "snow.png" },
        { 1255, "snow.png" },
        { 1258, "snow.png" },
        { 1261, "snow.png" },
        { 1264, "snow.png" },
        { 1273, "rain.png" },
        { 1276, "rain.png" },
        { 1279, "snow.png" },
        { 1282, "snow.png" }
    };

            
            if (weatherIcons.ContainsKey(conditionCode))
            {
                return $"Images/{weatherIcons[conditionCode]}";
            }
            else
            {
              
                return "Images/default.png";
            }
        }



        private async void DeleteFavoriteButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var favorite = button?.BindingContext as Favoritos;

            if (favorite != null)
            {
             
                await _db.Eliminar(favorite);

                
                FavoritesList.Remove(favorite);

                await DisplayAlert("Favoritos", "Ciudad eliminada de favoritos", "OK");
            }
        }


        private void FavoritesCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            WeatherLayout.IsVisible = true;
           
            var favorite = e.CurrentSelection.FirstOrDefault() as Favoritos;

            if (favorite != null)
            {
                FavoritesCollectionView.SelectedItem = null;


                CityLabel.Text = favorite.City;
                CountryLabel.Text = favorite.Country;
                TimeLabel.Text = favorite.Time;
                TemperatureLabel.Text = favorite.Temperature;
                WeatherIcon.Source = favorite.Icon;
                DescriptionLabel.Text = favorite.Description;

             
                AddToFavoritesButton.IsVisible = false;
            }
        }

    }

}
