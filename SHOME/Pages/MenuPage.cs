using Xamarin.Forms;

namespace SHOME
{
    public class MenuPage : TabbedPage
    {
        public MenuPage()
        {
            Title = "SHOME";
		
            Children.Add(new ContentMenu("Bedroom")
            {
                Icon = "bedroom.png"
            });

            Children.Add(new ContentMenu("Living Room")
            {
                Icon = "lounge.png"
            });

            Children.Add(new ContentMenu("Home")
            {
                Icon = "homeIcon.png"
            });

            Children.Add(new ContentMenu("Garden")
            {
                Icon = "garden.png"
            });
            Children.Add(new ContentMenu("Kitchen")
            {
                Icon = "kitchen.png"
            });
        }
    }
}