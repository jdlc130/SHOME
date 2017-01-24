using System;
using SHOME.Data;
using Xamarin.Forms;

namespace SHOME.Pages
{
    internal class ConsumptionPage : ContentPage
    {
        private const string LowConsumption = "low_consumption.png";
        private const string MediumConsumption = "medium_consumption.png";
        private const string HighConsumption = "high_consumption.png";
        private string _image;

        private double _powerTotal;

        private double[] _sortesDevices = new double[9];
        private double[] _devices = new double[9];

        private readonly string[] _devicesName = new string[]
        {
            "Coffe Machine Delta",
            "Toaster",
            "Microwave",
            "Mixer",
            "Refrigerator",
            "Stove with Over",
            "Freeze",
            "Kettle",
            "TV Philips"
        };

        public ConsumptionPage()
        {
            //CalculatePower(DateTime.Now.DayOfYear + "-" + "01" + "01", DateTime.Now.DayOfYear + "-" + "12" + "31");

            CalculatePower("2016-11-26", "2016-11-26");
        }
        
        private async void CalculatePower(string startTime, string endTime)
        {
            var json = await WebServicesData.SyncTask("GET", "lucas/hourly", startTime, endTime);
            var index = 0;
            _powerTotal = 0;
            while (index < json.Count)
            {
                var result = json[index];
                var power = result["Power"];
                _powerTotal += power;
                index++;
            }
            _powerTotal = _powerTotal/(index+1);

            CalculatePowerDevices(startTime, endTime);
        }

        private async void CalculatePowerDevices(string startTime, string endTime)
        {
            var deviceIndex = 1;
            while (deviceIndex <= 1)
            {
                var json = await WebServicesData.SyncTask("GET", "lucas/device", deviceIndex, startTime, endTime);
                var index = 0;
                _devices[deviceIndex] = 0;
                while (index < json.Count)
                {
                    var result = json[index];
                    var power = result["Power"];
                    _devices[deviceIndex] += power;
                    index++;
                }
                _devices[deviceIndex] = _devices[deviceIndex] / (index + 1);

                deviceIndex++;
            }
            SortDevices();
        }
        
        private void SortDevices()
        {
            _sortesDevices = _devices;
            Array.Sort(_sortesDevices);

            Construtor();
        }

        private void Construtor()
        {
            _image = LowConsumption;

            var background = new Image
            {
                Source = new FileImageSource
                {
                    File = Device.OnPlatform(
                        "Images/header_energyConsumption.png",
                        "header_energyConsumption.png",
                        "Images/header_energyConsumption.png")
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };


            var period = new Grid
            {
                Padding = new Thickness(50, 10, 50, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    }
                },
                ColumnSpacing = 0
            };
            var day = new Button
            {
                Text = "Day",
                FontFamily = "Roboto",
                FontSize = 18,
                BackgroundColor = Color.FromRgb(211, 211, 211)
            };
            //day.Clicked += async (sender, e)
            //{

            //}

            var mouth = new Button
            {
                Text = "Mouth",
                FontFamily = "Roboto",
                FontSize = 18,
                BackgroundColor = Color.FromRgb(211, 211, 211)

            };
            //mouth.Clicked += async (sender, e)
            //{

            //}

            var year = new Button
            {
                Text = "Year",
                FontFamily = "Roboto",
                FontSize = 18,
                BackgroundColor = Color.Gray
            };
            //year.Clicked += async (sender, e)
            //{

            //}

            period.Children.Add(day, 0, 0);
            period.Children.Add(mouth, 1, 0);
            period.Children.Add(year, 2, 0);


            var feedback = new Image
            { 
                Source = _image,
                HorizontalOptions = LayoutOptions.Center,
                Scale = 0.5
            };

            var divisions = new Grid
            {
                Padding = new Thickness(20, 0, 20, 0),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                },
                ColumnSpacing = 0,
                RowSpacing = 0
            };
            for (var i = 0; i < 4; i++)
            {
                var index = Array.FindIndex(_devices, p => p ==_sortesDevices[_sortesDevices.Length - i - 1]);
                divisions.Children.Add(
                    new Label { Text = _devicesName[index], FontFamily = "Roboto", FontSize = 18, TextColor = Color.Gray}, 0, i );
                divisions.Children.Add(
                    new Label { Text = _sortesDevices[_sortesDevices.Length - i - 1].ToString(), FontFamily = "Roboto", FontSize = 18, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.End}, 1, i);
            }
            
            Content = new StackLayout
            {
                Children =
                {
                    background,
                    period,
                    feedback,
                    divisions
                }
            };
        }
    }
}