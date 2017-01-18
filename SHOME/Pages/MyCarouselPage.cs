using System;
using Xamarin.Forms;

namespace SHOME

{
	public class MyCarouselPage : CarouselPage
    {
		public MyCarouselPage ()
		{
			this.Children.Add (new Weather.TemperaturePage());
			//this.Children.Add (new Weather.RainPage ());
			this.Children.Add (new Weather.WindPage ());
			this.Children.Add (new Weather.HumidityPage ());
            //this.Children.Add (new Weather.AirQualityDescPage());
        }
    }
}

