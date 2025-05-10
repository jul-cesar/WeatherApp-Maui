namespace WeatherLiz.Models
{
    public class WeatherResponse
    {
        public Location Location { get; set; }
        public CurrentWeather Current { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string TzId { get; set; }
        public long LocaltimeEpoch { get; set; }
        public string Localtime { get; set; }
    }

    public class CurrentWeather
    {
        public long LastUpdatedEpoch { get; set; }
        public string LastUpdated { get; set; }
        public double Temp_C { get; set; }
        public double Temp_F { get; set; }
        public bool IsDay { get; set; }
        public WeatherCondition Condition { get; set; }
        public double WindMph { get; set; }
        public double WindKph { get; set; }
        public int WindDegree { get; set; }
        public string WindDir { get; set; }
        public double PressureMb { get; set; }
        public double PressureIn { get; set; }
        public double PrecipMm { get; set; }
        public double PrecipIn { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public double FeelsLikeC { get; set; }
        public double FeelsLikeF { get; set; }
        public double WindChillC { get; set; }
        public double WindChillF { get; set; }
        public double HeatIndexC { get; set; }
        public double HeatIndexF { get; set; }
        public double DewPointC { get; set; }
        public double DewPointF { get; set; }
        public double VisKm { get; set; }
        public double VisMiles { get; set; }
    
        public double GustMph { get; set; }
        public double GustKph { get; set; }
    }

    public class WeatherCondition
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public int Code { get; set; }
    }
}