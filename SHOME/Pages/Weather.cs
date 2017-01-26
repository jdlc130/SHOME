using SHOME.Data;
using Xamarin.Forms;

namespace SHOME
{
    public class Weather : ContentPage
    {
        public Grid WeatherGrid;
        public Grid ForecastGrid;

        public Weather()
        {
            GetTemperature();
        }

        public string Temperature { get; set; }
        public string TempMax { get; set; }
        public string TempMin { get; set; }
        public string Wind { get; set; }
        public string WindD { get; set; }
        public string Humidity { get; set; }
        public string InsideTemperature { get; set; }

        //Forecast
        public string TempT { get; set; }
        public string TempTMax { get; set; }
        public string TempTMin { get; set; }
        public string HumidityT { get; set; }
        public string Rain { get; set; }

        public async void GetTemperature()
        {
            var aux = 0;
            var json = await WebServicesData.SyncTask("GET", "temperature");
            var size = json.Count;

            var device = json["Device_Num_7"];
            var state = device["states"];
            var position = state[0];
            var value = position["value"];
            InsideTemperature = value;

            Constructor();
        }

        public void Constructor()
        {
            this.Temperature = " ";
            this.TempMax = " ";
            this.TempMin = " ";
            this.Wind = " ";
            WindD = " ";
            this.Humidity = " ";

            //forecast
            this.TempT = " ";
            this.TempTMax = " ";
            this.TempTMin = " ";
            this.HumidityT = " ";
            this.Rain = " ";

            //Cabeçalho
            var header = new Image
            {
                Source = "header_weather.png"
            };

            var TempInsGrid = new Grid
            {
                Padding = new Thickness(20, 0, 20, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                }
            };
            // Condições no Interior
            var conditionsI = new Label
            {
                Text = "Enviromental conditions inside",
                TextColor = Color.Teal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                FontFamily = "Roboto"
            };
            var temperatureInside = new Label
            {
                Text = InsideTemperature + " ºC",
                HorizontalTextAlignment = TextAlignment.Center,
                FontFamily = "Roboto",
                FontSize = 14
            };
            TempInsGrid.Children.Add(conditionsI, 0, 0);
            TempInsGrid.Children.Add(temperatureInside, 0, 1);

            //Condições no Exterior
            var TempExtGrid = new Grid
            {
                Padding = new Thickness(20, 0, 20, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                }
            };
            var conditions = new Label
            {
                Text = "Enviromental conditions outside",
                TextColor = Color.Teal,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
                FontSize = 18,
                FontFamily = "Roboto"
            };
            var location = new Entry();
            var submit = new Button {Text = "Submit"};
            TempExtGrid.Children.Add(conditions, 0, 0);
            TempExtGrid.Children.Add(location, 0, 1);
            TempExtGrid.Children.Add(submit, 0, 2);
            // Tempo
            submit.Clicked += async (sender, e) =>
            {
                if (location == null) return;
                var weather = await WeatherCore.GetWeather(location.Text);
                //var temperture = weather.Temperature;

                if (weather != null)
                {
                    TemperatureL.Text = "Temperature: " + weather.Temperature;
                    TempMinL.Text = "Temperature Min: " + weather.TempMin;
                    TempMaxL.Text = "Temperature Max: " + weather.TempMax;
                    WindL.Text = "Wind: " + weather.Wind;
                    WindDirectionL.Text = "Wind Direction: " + weather.WindD;
                    HumidityL.Text = "Humidity: " + weather.Humidity;
                    WeatherConstrutor();
                }

                //Forecast
                var forecast = await WeatherCore.GetForecast(location.Text);
                if (forecast != null)
                {
                    TempTL.Text = "Temperature: " + forecast.TempT;
                    TempTMinL.Text = "Temperature Min: " + forecast.TempTMin;
                    TempTMaxL.Text = "Temperature Max: " + forecast.TempTMax;
                    HumidityTL.Text = "Humidity: " + forecast.HumidityT;
                    RainL.Text = "Rain: " + forecast.Rain;
                    ForeCastConstrutor();
                }
            };
            TemperatureL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            TempMinL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            TempMaxL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            HumidityL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            WindL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            WindDirectionL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            WeatherGrid = new Grid
            {
                Padding = new Thickness(20, 20, 20, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                }
            };

            ForecastL = new Label
            {
                Text = "Forecast for Tomorrow",
                TextColor = Color.Teal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                FontFamily = "Roboto"
            };
            TempTL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            TempTMaxL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            TempTMinL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            HumidityTL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            RainL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            ForecastGrid = new Grid
            {
                Padding = new Thickness(20, 20, 20, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                }
            };

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        header,
                        TempInsGrid,
                        TempExtGrid,
                        WeatherGrid,
                        ForecastGrid
                    }
                }
            };
        }

        public Label TemperatureL, TempMinL, TempMaxL, HumidityL, WindL, WindDirectionL, ForecastL,
            TempTL, TempTMaxL, TempTMinL, HumidityTL, RainL;

        private void WeatherConstrutor()
        {
            WeatherGrid.Children.Add(TemperatureL, 0, 0);
            WeatherGrid.Children.Add(TempMinL, 0 , 1);
            WeatherGrid.Children.Add(TempMaxL, 0, 2);
            WeatherGrid.Children.Add(HumidityL, 0, 3);
            WeatherGrid.Children.Add(WindL, 0, 4);
            WeatherGrid.Children.Add(WindDirectionL, 0, 5);
        }

        private void ForeCastConstrutor()
        {
            ForecastGrid.Children.Add(ForecastL, 0, 0);
            ForecastGrid.Children.Add(TempTL, 0, 1);
            ForecastGrid.Children.Add(TempTMaxL, 0, 2);
            ForecastGrid.Children.Add(TempTMinL, 0, 3);
            ForecastGrid.Children.Add(HumidityTL, 0, 4);
            ForecastGrid.Children.Add(RainL, 0, 5);
        }
    }
}