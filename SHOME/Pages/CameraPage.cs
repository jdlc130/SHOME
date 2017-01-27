using Xamarin.Forms;

namespace SHOME
{
    public class CameraPage : ContentPage
    {
        public CameraPage()
        {
            Padding = new Thickness(20, 20, 20, 20);

            var TituloCamara = new Label
            {
                Text = "Nest"
            };

            // Create object "WebView" for show the cameras video
            var CameraNest = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = "https://video.nest.com/live/GRuDvinrXP"
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // Is assigned to the content of the page the title and the webview
            Content = new StackLayout
            {
                Children =
                {
                    TituloCamara,
                    CameraNest
                }
            };
        }
    }
}