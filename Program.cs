//Author: Shamoun Hennawi
//Date: 23.11.2021
using System.Text.Json;

Console.Write("From which city do you want to know the weather?: ");
string city = Console.ReadLine();

string URL = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=4dcb331f409e04b9201f3fd8a6c2b455";
HttpClient client = new HttpClient();

var responseObject = await GetWeather(URL);
Console.WriteLine($"Description of the weather: {responseObject.weather[0].description}");
Console.WriteLine($"Min and max temperature of city: Min: {Math.Round(responseObject.main.temp_min_C)}°C," +
    $" Max: {Math.Round(responseObject.main.temp_max_C)}°C");

async Task<WeatherModel> GetWeather(string url)
{
    var response = await client.GetAsync(url);
    var weatherResponseString = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<WeatherModel>(weatherResponseString);
}

class WeatherModel
{
    public string name { get; set; }
    public WeatherDescriptionModel[] weather { get; set; }
    public TemperatureModel main { get; set; }
    public string description { get; set; }
    
}
class WeatherDescriptionModel
{
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }

}
class TemperatureModel
{
    public float temp_max_C { get => temp_max - 273.15f; set => temp_max = value; }
    public float temp_min_C { get => temp_min - 273.15f; set => temp_min = value; }
    public float temp_max {get;set;}
    public float temp_min { get; set; }

}