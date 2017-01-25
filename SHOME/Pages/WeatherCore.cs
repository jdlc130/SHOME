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
                              + city + "&units=metric" + "&appid=" + key;
            ;

            var results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                var weather = new Weather();
                weather.Temperature = (string) results["main"]["temp"] + " C";
                weather.Wind = (string) results["wind"]["speed"] + " mph";
                weather.WindD = (string) results["wind"]["deg"] + " Deegres";
                weather.Humidity = (string) results["main"]["humidity"] + " %";
                //  weather.Precipitation = (string) results["main"]["precipitation"] + " mm";


                var time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var sunrise = time.AddSeconds((double) results["sys"]["sunrise"]);
                var sunset = time.AddSeconds((double) results["sys"]["sunset"]);
                //weather.Sunrise = sunrise.ToString() + " UTC";
                //weather.Sunset = sunset.ToString() + " UTC";
                return weather;
            }
            return null;
        }
    }
}