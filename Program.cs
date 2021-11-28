//Author: Shamoun Hennawi
//Date: 23.11.2021
using System.Text.Json;

Console.Write("From which city do you want to know the weather?: ");
string city = Console.ReadLine();

string URL = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=4dcb331f409e04b9201f3fd8a6c2b455";
HttpClient client = new HttpClient();

Console.WriteLine(await GetWeather(URL));

async Task<string> GetWeather(string url)
{
    var response = await client.GetAsync(url);
    var weatherResponseString = await response.Content.ReadAsStringAsync();
    var weatherResponseObject = JsonSerializer.Deserialize<WeatherModel>(weatherResponseString);
    return weatherResponseObject.name;
}

class WeatherModel
{
    public string name { get; set; }
    public string description { get; set; }
    private float _temp_min;
    private float _temp_max;
    public float temp_max 
    {   get => _temp_max - 273.15f;
        private set => _temp_max = value - 273.15f;
    }
    public float temp_min
    {
        get => _temp_min - 273.15f; 
        private set => _temp_min = value - 273.15f; 
    }
}