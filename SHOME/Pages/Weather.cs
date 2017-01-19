using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Color = Xamarin.Forms.Color;

namespace SHOME

{
    public class Weather
    {
        public float Temperature { get; set; }
        public float Wind { get; set; }
        public float Humidity { get; set; }
        public float Rain { get; set; }
        public string AirQualityDesc { get; set; }


        public Weather()
        {
            this.Temperature = Temperature;
            this.Wind = Wind;
            this.Humidity = Humidity;
            this.Rain = Rain;
            this.AirQualityDesc = " ";

        }

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
                var imageR = new Image()
                {
                    Source = new FileImageSource
                    {
                        File = Device.OnPlatform(iOS:"Images/humidity.png",
                            Android: "humidity.png",
                            WinPhone: "Images/humidity.png")
                    },
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start
                };
                var lblhumidity = new Label
                {
                    Text = "85%" ,
                    TextColor = Color.Gray,
                    FontSize = 40,
                    XAlign = TextAlignment.Center,
                    YAlign = TextAlignment.Center,
                    FontFamily = "Roboto Medium"

                };

                BackgroundColor =Color.White;
                
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
                var imageR = new Image()
                {
                    Source = new FileImageSource
                    {
                        File = Device.OnPlatform(iOS: "Images/temperature.png",
                            Android: "temperature.png",
                            WinPhone: "Images/temperature.png")
                    },
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start};

                var lblTemp = new Label
                {
                    Text = "25 Graus" ,
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
                var imageR = new Image()
                {
                    Source = new FileImageSource
                    {
                        File = Device.OnPlatform(iOS: "Images/wind.png",
                            Android: "wind.png",
                            WinPhone: "Images/wind.png")
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
