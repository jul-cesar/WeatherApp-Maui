using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace WeatherLiz.Services;

public class WeatherService
{
    public HttpClient _httpClient { get; set; }
    private const string ApiURL = "https://api.weatherapi.com/v1";
    private const string ApiKey = "";

    public WeatherService()
    {
        _httpClient = new HttpClient();
    }


}