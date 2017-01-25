using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SHOME.Pages
{
    class AddActuator : ContentPage
    {
        public AddActuator()
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
                        "Images/XXXXX.png",
                        "XXXXX.png",
                        "XXXXXX.png")
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };
            
            var forms = new Grid
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
                ColumnSpacing = 5
            };
        }
        
    }
}
