using Xamarin.Forms;

namespace SHOME

{
    public class Weather
    {
        public Weather()
        {
            Temperature = Temperature;
            Wind = Wind;
            Humidity = Humidity;
            Rain = Rain;
            AirQualityDesc = " ";
        }

        public float Temperature { get; set; }
        public float Wind { get; set; }
        public float Humidity { get; set; }
        public float Rain { get; set; }
        public string AirQualityDesc { get; set; }

        //public class RainPage : ContentPage
        //{
        //    public RainPage()
        //    {
        //        var imageR = new Image()
        //        {
        //            Source = new FileImageSource
        //            {
        //                File = Device.OnPlatform(iOS: "Images/weather3.png",
        //                    Android: "weather3.png",
        //                    WinPhone: "Images/weather3.png")
        //            },
        //            HorizontalOptions = LayoutOptions.Center,
        //            VerticalOptions = LayoutOptions.CenterAndExpand
        //        };

        //        var lblchuva = new Label
        //        {
        //            Text = "Chuva",
        //            TextColor = Color.Blue,
        //            FontSize = 30,
        //            XAlign = TextAlignment.Center
        //        };

        //        Content = new StackLayout
        //        {
        //            Children =
        //            {
        //                imageR,
        //                lblchuva

        //            }
        //        };
        //    }
        //}

        //public class AirQualityDescPage : ContentPage
        //{
        //    public AirQualityDescPage()
        //    {
        //        var lblAirQuality= new Label
        //        {
        //            Text = "Air Quality",
        //            TextColor = Color.Blue,
        //            FontSize = 30,
        //            XAlign = TextAlignment.Center
        //        };


        //        Content = new StackLayout
        //        {
        //            Children =
        //            {
        //                lblAirQuality
        //            }

        //        };
        //    }
        //}


        public class HumidityPage : ContentPage
        {
            public HumidityPage()
            {
                var imageR = new Image
                {
                    Source = new FileImageSource
                    {
                        File = Device.OnPlatform("Images/humidity.png",
                            "humidity.png",
                            "Images/humidity.png")
                    },
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start
                };
                var lblhumidity = new Label
                {
                    Text = "85%",
                    TextColor = Color.Gray,
                    FontSize = 40,
                    XAlign = TextAlignment.Center,
                    YAlign = TextAlignment.Center,
                    FontFamily = "Roboto Medium"
                };

                BackgroundColor = Color.White;

                Content = new StackLayout
                {
                    Children =
                    {
                        imageR,
                        lblhumidity
                    }
                };
            }
        }

        public class TemperaturePage : ContentPage
        {
            public TemperaturePage()
            {
                BackgroundColor = Color.White;
                var imageR = new Image
                {
                    Source = new FileImageSource
                    {
                        File = Device.OnPlatform("Images/temperature.png",
                            "temperature.png",
                            "Images/temperature.png")
                    },
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start
                };

                var lblTemp = new Label
                {
                    Text = "25 Graus",
                    TextColor = Color.Gray,
                    FontSize = 40,
                    XAlign = TextAlignment.Center,
                    YAlign = TextAlignment.Center,
                    FontFamily = "Roboto Medium"
                };

                Content = new StackLayout
                {
                    Children =
                    {
                        imageR,
                        lblTemp
                    }
                };
            }
        }

        public class WindPage : ContentPage
        {
            public WindPage()
            {
                BackgroundColor = Color.White;
                var imageR = new Image
                {
                    Source = new FileImageSource
                    {
                        File = Device.OnPlatform("Images/wind.png",
                            "wind.png",
                            "Images/wind.png")
                    },
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start
                };

                var lblwind = new Label
                {
                    Text = "40 Km/h",
                    TextColor = Color.Gray,
                    FontSize = 40,
                    XAlign = TextAlignment.Center,
                    YAlign = TextAlignment.Center,
                    FontFamily = "Roboto Medium"
                };

                Content = new StackLayout
                {
                    Children =
                    {
                        imageR,
                        lblwind
                    }
                };
            }
        }
    }
}