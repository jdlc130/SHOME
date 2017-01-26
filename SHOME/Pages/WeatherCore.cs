using System;
using System.Threading.Tasks;

namespace SHOME
{
    internal class WeatherCore
    {
        public static async Task<Weather> GetWeather(string city)
        {
            //Sign up for a free API key at http://openweathermap.org/appid
            var key = "fc9f6c524fc093759cd28d41fda89a1b";
            var queryString = "http://" + "api.openweathermap.org/data/2.5/weather?q="
                              + city + "&units=metric&appid=" + key;

            var results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            if (results != null)
            {
                var weather = new Weather
                {
                    Temperature = (string) results["main"]["temp"] + " ºC",
                    TempMax = (string) results["main"]["temp_max"] + " ºC",
                    TempMin = (string) results["main"]["temp_min"] + " ºC",
                    Wind = (string) results["wind"]["speed"] + " mph",
                    WindD = (string) results["wind"]["deg"] + " Deegres",
                    Humidity = (string) results["main"]["humidity"] + " %"
                };

                var time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var sunrise = time.AddSeconds((double) results["sys"]["sunrise"]);
                var sunset = time.AddSeconds((double) results["sys"]["sunset"]);
                return weather;
            }
            return null;
        }

        public static async Task<Weather> GetForecast(string city)
        {
            //Sign up for a free API key at http://openweathermap.org/appid
            var key = "fc9f6c524fc093759cd28d41fda89a1b";
            var queryString2 = "http://" + "api.openweathermap.org/data/2.5/forecast?q=" + city +
                               ",pt&units=metric&appid=" + key;

            var results2 = await DataService.GetDataFromService(queryString2).ConfigureAwait(false);

            if (results2 != null)
            {
                var list = results2["list"];
                var frag = list[1];
                var main = frag["main"];
                var resultTempT = main["temp"];
                var resultTempTMax = main["temp_min"];
                var resultTempTMin = main["temp_max"];
                var resultHumidityT = main["humidity"];
                var fragment = list[5];
                var rainFr = fragment["rain"];
                var resultRain = rainFr["3h"];
                resultRain = resultRain ?? "Nula";

                var forecast = new Weather
                {
                    TempT = (string )resultTempT + " ºC",
                    TempTMax = (string)resultTempTMax + " ºC",
                    TempTMin = (string)resultTempTMin + " ºC",
                    HumidityT = (string)resultHumidityT + " %",
                    Rain = (string)resultRain  
                };
                return forecast;
            }
            return null;
        }
    }
}