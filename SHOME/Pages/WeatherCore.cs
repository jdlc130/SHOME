using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHOME
{
    class WeatherCore
    {
        public static async Task<Weather> GetWeather(string city)
        {
            //Sign up for a free API key at http://openweathermap.org/appid
            string key = "fc9f6c524fc093759cd28d41fda89a1b";
            string queryString = "http://" + "api.openweathermap.org/data/2.5/weather?q="
                                 + city + "&units=metric" + "&appid=" + key; ;

            var results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Temperature = (string)results["main"]["temp"] + " C";
                weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.WindD = (string)results["wind"]["deg"] + " Deegres";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                //  weather.Precipitation = (string) results["main"]["precipitation"] + " mm";


                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                //weather.Sunrise = sunrise.ToString() + " UTC";
                //weather.Sunset = sunset.ToString() + " UTC";
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}