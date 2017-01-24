using Xamarin.Forms;

namespace SHOME.Pages
{
    internal class ConsumptionPage : ContentPage
    {
        public ConsumptionPage()
        {
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


            var period = new Grid
            {
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                }
            };
            var day = new Button();
            var mouth = new Button();
            var year = new Button();

            period.Children.Add(day, 0, 0);
            period.Children.Add(mouth, 0, 1);
            period.Children.Add(year, 0, 2);


            var divisions = new Grid
            {
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                }
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