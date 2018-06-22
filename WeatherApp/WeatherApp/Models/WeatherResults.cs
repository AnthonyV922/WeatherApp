using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    class WeatherResults
    {
        // API KEY
        static readonly string key = "a3c9c17b8b23a90fba492ff80e9c5008";
        static DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static async Task<Weather> GetWeather(string ZipOrCity)
        {
            string queryString = String.Format("Http://api.openweathermap.org/data/2.5/weather?q={0},us&appid={1}&type=accurate&units=imperial", ZipOrCity, key);

            // Json from API
            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            
            // If Json isnt empty, a Weather object is initialized with the json value
            if (results["weather"] != null)
            {
                DateTime currentTime = time.AddSeconds((double)results["dt"]);
                Weather weather = new Weather
                {
                    Title = (string)results["name"],
                    Temperature = (string)results["main"]["temp"] + " F",
                    Description = (string)results["weather"][0]["description"],
                    Wind = (string)results["wind"]["speed"] + " MPH",
                    Humidity = (string)results["main"]["humidity"] + " %",
                    Visibility = (string)results["weather"][0]["main"],
                    Pressure = (string)results["main"]["pressure"] + " IN",
                    Time = currentTime.DayOfWeek.ToString() + " " + currentTime.ToShortDateString() + " " + currentTime.ToShortTimeString()
                };
                return weather;
            }
            else
            {
                return null;
            }
        }

        public static  async Task<ObservableCollection<Forecast>> GetForecast(string ZipOrCity)
        {
                                            
            string queryString = String.Format("Http://api.openweathermap.org/data/2.5/forecast?q={0},us&appid={1}&type=accurate&units=imperial", ZipOrCity, key);

            // Json from API
            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            // If Json isnt empty, a collection of Forecast objects is filled with the json
            if (results["list"][0]["weather"] != null)
            {
                DateTime currentTime;
                ObservableCollection<Forecast> forecastList = new ObservableCollection<Forecast>();
                
                // Loops through every three hour forecast for up to 5 days
                for (int i = 0; i < 38; i++)
                {
                    Forecast forecast = new Forecast();
                    currentTime = time.AddSeconds((double)results["list"][i]["dt"]);

                    // Displays when the next day starts
                    if (currentTime.ToShortTimeString() == "12:00 AM")
                    {
                        forecast.Location = "";
                        forecast.Temperature = "";
                        forecast.Description = currentTime.ToShortDateString();
                        forecast.Time = currentTime.DayOfWeek.ToString();
                        forecastList.Add(forecast);
                        continue;
                    }
                  
                    // Fills list with forecast for every three hours
                    forecast.Location = (string)results["city"]["name"];
                    forecast.Temperature = (string)results["list"][i]["main"]["temp"] + " F";
                    forecast.Description = (string)results["list"][i]["weather"][0]["description"];
                    forecast.Time = currentTime.ToShortTimeString();
                    forecastList.Add(forecast);
                }
                return forecastList;
            }
            else
            {
                return null;
            }
        }

        public static async Task<Weather> GetCoordinatesWeather(string lat, string lon)
        {

            string queryString = String.Format("Http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}&type=accurate&units=imperial", lat, lon, key);

            //Json from API
            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            // If Json isnt empty, a Weather object is initialized with the json value
            if (results["weather"] != null)
            {
                DateTime currentTime = time.AddSeconds((double)results["dt"]);
                Weather weather = new Weather
                {
                    Title = (string)results["name"],
                    Temperature = (string)results["main"]["temp"] + " F",
                    Description = (string)results["weather"][0]["description"],
                    Wind = (string)results["wind"]["speed"] + " MPH",
                    Humidity = (string)results["main"]["humidity"] + " %",
                    Visibility = (string)results["weather"][0]["main"],
                    Pressure = (string)results["main"]["pressure"] + " IN",
                    Time = currentTime.DayOfWeek.ToString() + " " + currentTime.ToShortDateString() + " " + currentTime.ToShortTimeString()
                };
                return weather;
            }
            else
            {
                return null;
            }
        }
        public static async Task<ObservableCollection<Forecast>> GetCoordinateForecast(string lat, string lon)
        {

            string queryString = String.Format("Http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&appid={2}&type=accurate&units=imperial", lat, lon, key);

            // Json from API
            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            // If Json isnt empty, a collection of Forecast objects is filled with the json
            if (results["list"][0]["weather"] != null)
            {
                DateTime currentTime;
                ObservableCollection<Forecast> forecastList = new ObservableCollection<Forecast>();

                // Loops through every three hours for 5 day API Forecast
                for (int i = 0; i < 38; i++)
                {
                    Forecast forecast = new Forecast();
                    currentTime = time.AddSeconds((double)results["list"][i]["dt"]);

                    // Displays when the next day starts
                    if (currentTime.ToShortTimeString() == "12:00 AM")
                    {
                        forecast.Location = "";
                        forecast.Temperature = "";
                        forecast.Description = currentTime.ToShortDateString();
                        forecast.Time = currentTime.DayOfWeek.ToString();
                        forecastList.Add(forecast);
                        continue;
                    }

                    // Fills list with forecast for every three hours
                    forecast.Location = (string)results["city"]["name"];
                    forecast.Temperature = (string)results["list"][i]["main"]["temp"] + " F";
                    forecast.Description = (string)results["list"][i]["weather"][0]["description"];
                    forecast.Time = currentTime.ToShortTimeString();
                    forecastList.Add(forecast);
                }

                return forecastList;
            }
            else
            {
                return null;
            }
        }
    }
}
