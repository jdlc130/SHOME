using Xamarin.Forms;

namespace SHOME
{
    public class MenuPage : TabbedPage
    {
        public MenuPage()
        {
            Title = "SHOME";


            Children.Add(new EstarPage(1)
            {
                Icon = "bedroom.png"
            });

            Children.Add(new EstarPage(2)
            {
                Icon = "lounge.png"
            });

            Children.Add(new EstarPage(3)
            {
                Icon = "homeIcon.png"
            });

            Children.Add(new EstarPage(4)
            {
                Icon = "garden.png"
            });
            Children.Add(new EstarPage(4)
            {
                Icon = "kitchen.png"
            });
        }
    }
}