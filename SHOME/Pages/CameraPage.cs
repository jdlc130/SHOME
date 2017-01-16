using System;

using Xamarin.Forms;

namespace SHOME
{
	public class CameraPage : ContentPage
	{
		
		public CameraPage()
		{

            Padding = new Thickness (20 , 20 , 20 , 20);

            var TituloCamara = new Label
            {
                Text = "Nest"
            };

            var CameraNest = new WebView
            {
                Source = new UrlWebViewSource {
                    Url = "https://video.nest.com/live/GRuDvinrXP"
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

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

