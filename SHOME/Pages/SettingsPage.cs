using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SHOME.Pages
{
    class SettingsPage : ContentPage
    {
        public static bool ClicksEnabled;
        public static bool BeaconsEnabled;

        public SettingsPage()
        {
            Construtor();
        }

        private void Construtor()
        {
            var header = new Image
            {
                Source = new FileImageSource
                {
                    File = Device.OnPlatform(
                        "Images/header_settings.png",
                        "header_settings.png",
                        "Images/header_settings.png")
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            var configGrid = new Grid
            {
                Padding = new Thickness(20, 20, 20, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                },
                RowSpacing = 10
            };
            var clicksLabel = new Label
            {
                Text = "Sort by number of Clicks",
                FontFamily = "Roboto",
                FontSize = 14,
                TextColor = Color.Gray
            };
            var clicks = new Switch
            {
                HorizontalOptions = LayoutOptions.End,
                IsToggled = ClicksEnabled
            };
            clicks.Toggled += (sender, e) =>
            {
                ClicksEnabled = e.Value;
            };
            var beaconsLabel = new Label
            {
                Text = "Beacons Localization",
                FontFamily = "Roboto",
                FontSize = 14,
                TextColor = Color.Gray
            };
            var beacons = new Switch
            {
                HorizontalOptions = LayoutOptions.End,
                IsToggled = BeaconsEnabled
            };
            beacons.Toggled += (sender, e) =>
            {
                BeaconsEnabled = e.Value;
            };
            configGrid.Children.Add(clicksLabel, 0, 0);
            configGrid.Children.Add(clicks, 1, 0);
            configGrid.Children.Add(beaconsLabel, 0, 1);
            configGrid.Children.Add(beacons, 1, 1);

            Content = new StackLayout
            {
                Children =
                {
                    header,
                    configGrid
                }
            };
        }
    }
}
