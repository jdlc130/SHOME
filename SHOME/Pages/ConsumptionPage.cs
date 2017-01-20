using Xamarin.Forms;

namespace SHOME.Pages
{
    internal class ConsumptionPage : ContentPage
    {
        public ConsumptionPage()
        {
            BackgroundColor = Color.White;
            var background = new Image
            {
                Source = new FileImageSource
                {
                    File = Device.OnPlatform(
                        "Images/energy_consumption_background.png",
                        "energy_consumption_background.png",
                        "Images/energy_consumption_background.png")
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            Content = new StackLayout
            {
                Children =
                {
                    background
                }
            };
        }
    }
}