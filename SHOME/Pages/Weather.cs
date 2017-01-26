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

        //Função para obter a temperatura do interior
        public async void GetTemperature()
        {
            var aux = 0;
            var json = await WebServicesData.SyncTask("GET", "temperature");
            var size = json.Count;

            var device = json["Device_Num_7"];
            var state = device["states"];
            var position = state[0];
            var value = position["value"]; //o valor da temperatura está guardado na posicao value
            InsideTemperature = value; // a temperatura do interior é o valor obtido anteriormente

            Constructor();
        }

        public void Constructor()
        {
            //weather inicialmente está vazio
            this.Temperature = " ";
            this.TempMax = " ";
            this.TempMin = " ";
            this.Wind = " ";
            this.WindD = " ";
            this.Humidity = " ";

            //forecast inicialmente está vazio
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
            // Criar a label com o titulo 
            var conditionsI = new Label
            {
                Text = "Enviromental conditions inside",
                TextColor = Color.Teal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                FontFamily = "Roboto"
            };
            // label que apresenta o valor da temperatura no interior
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
            //Cria uma grelha 
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
            // Criar label para apresentar novamente um titulo das condiçoes no exterior
            var conditions = new Label
            {
                Text = "Enviromental conditions outside",
                TextColor = Color.Teal,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
                FontSize = 18,
                FontFamily = "Roboto"
            };
            // Entrada para inserir a localização pretendida
            var location = new Entry();
            // Botão de submeter
            var submit = new Button {Text = "Submit"};
            
            //Inserir as variaveis na grelha
            TempExtGrid.Children.Add(conditions, 0, 0);
            TempExtGrid.Children.Add(location, 0, 1);
            TempExtGrid.Children.Add(submit, 0, 2);
            
            // quando o botao submit 
            submit.Clicked += async (sender, e) =>
            {
                // se a localiza~ção for vazia não retorna nada
                if (location == null) return;
                //variavel weather guarda a informação obtida da função getWeather 
                //da localizaçao inserida
                var weather = await WeatherCore.GetWeather(location.Text);
                
                //se o weather for diferente de nulo é atribuido o valor a cada label para
                //apresentar os vários parametros
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
                //variavel forecast guarda a informação obtida da função getForecast 
                //da localizaçao inserida
                var forecast = await WeatherCore.GetForecast(location.Text);
                //se o forecast for diferente de nulo é atribuido os valores às label para 
                //apresnetar os valores
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
            //label da temperatura
            TemperatureL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            //label da temperatura min
            TempMinL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            //label da temperatura max
            TempMaxL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            //label da humidade
            HumidityL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            //label do vento
            WindL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            //label da direccao do vento
            WindDirectionL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            // criar a grelha do weather
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
            // label com o titulo do tempo para amanhã
            ForecastL = new Label
            {
                Text = "Forecast for Tomorrow",
                TextColor = Color.Teal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                FontFamily = "Roboto"
            };
            // label com a temperatura para o dia seguinte
            TempTL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            // label com a temperatura max para o dia seguinte
            TempTMaxL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            // label com a temperatura min para o dia seguinte
            TempTMinL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            // label com a humidade para o dia seguinte
            HumidityTL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            // label com a chuva para o dia seguinte
            RainL = new Label
            {
                Text = " ",
                HorizontalTextAlignment = TextAlignment.Start,
                FontFamily = "Roboto",
                FontSize = 14
            };
            // criar a gelha para o forecast
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
                        TempInsGrid, //grelha com os dados da tem do interior
                        TempExtGrid, //grelha com os dados da temp do exterior parte de preenchimento
                        WeatherGrid, //grelha com os dados do tempo exterior após insercao da localizaçao 
                        ForecastGrid //grelha com os dados da previsao tempo exterior
                                     //após insercao da localizaçao
                    }
                }
            };
        }

        public Label TemperatureL, TempMinL, TempMaxL, HumidityL, WindL, WindDirectionL, ForecastL,
            TempTL, TempTMaxL, TempTMinL, HumidityTL, RainL;

        private void WeatherConstrutor()
        {
            //inserção das diversas labels na grelha do weather
            WeatherGrid.Children.Add(TemperatureL, 0, 0);
            WeatherGrid.Children.Add(TempMinL, 0 , 1);
            WeatherGrid.Children.Add(TempMaxL, 0, 2);
            WeatherGrid.Children.Add(HumidityL, 0, 3);
            WeatherGrid.Children.Add(WindL, 0, 4);
            WeatherGrid.Children.Add(WindDirectionL, 0, 5);
        }

        private void ForeCastConstrutor()
        {
            //inserção das diversas labels na grelha do forecast
            ForecastGrid.Children.Add(ForecastL, 0, 0);
            ForecastGrid.Children.Add(TempTL, 0, 1);
            ForecastGrid.Children.Add(TempTMaxL, 0, 2);
            ForecastGrid.Children.Add(TempTMinL, 0, 3);
            ForecastGrid.Children.Add(HumidityTL, 0, 4);
            ForecastGrid.Children.Add(RainL, 0, 5);
        }
    }
}