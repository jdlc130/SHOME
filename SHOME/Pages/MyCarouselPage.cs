using Xamarin.Forms;

namespace SHOME

{
    public class MyCarouselPage : CarouselPage
    {
        public MyCarouselPage()
        {
            Children.Add(new Weather.TemperaturePage());
            //this.Children.Add (new Weather.RainPage ());
            Children.Add(new Weather.WindPage());
            Children.Add(new Weather.HumidityPage());
            //this.Children.Add (new Weather.AirQualityDescPage());
        }
    }
}