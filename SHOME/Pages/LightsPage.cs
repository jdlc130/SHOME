using System.Collections.Generic;
using System.Diagnostics;
using SHOME.Data;
using Xamarin.Forms;

namespace SHOME
{
    public class LightsPage : ContentPage
    {
        // Dictionary to get Color from color name.
        private readonly Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            {"Blue", Color.Blue},
            {"Pink", Color.Pink},
            {"Green", Color.Green},
            {"Yellow", Color.Yellow},
            {"White", Color.White}
        };

        public LightsPage()
        {
            Padding = new Thickness(20, 20, 20, 20);

            var header = new Image
            {
                Source = "lights.png",
                HorizontalOptions = LayoutOptions.Center
            };

            var light_lbl = new Label
            {
                Text = "Principal",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            var power_btn = new Switch
            {
                HorizontalOptions = LayoutOptions.End
            };
            var nn = PostData.FetchAsync($"montalegre.m-iti.org:22941/ToggleDevice/" + "17" + "/" + "1");
            Debug.WriteLine(nn.ToString());

            power_btn.Toggled += power_btn_Toggled;
            

            var intensity_btn = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            intensity_btn.ValueChanged += Onintensity_btnValueChanged;

            var picker = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (var colorName in nameToColor.Keys)
                picker.Items.Add(colorName);

            // Create BoxView for displaying picked Color
            var boxView = new BoxView
            {
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            picker.SelectedIndexChanged += (sender, args) =>
            {
                if (picker.SelectedIndex == -1)
                {
                    boxView.Color = Color.Default;
                }
                else
                {
                    var colorName = picker.Items[picker.SelectedIndex];
                    boxView.Color = nameToColor[colorName];
                }
            };


            Content = new StackLayout
            {
                Children =
                {
                    header,
                    light_lbl,
                    power_btn,
                    intensity_btn,
                    picker,
                    boxView
                }
            };
        }


        private void power_btn_Toggled(object sender, ToggledEventArgs e)
        {
            var lll = new Label
            {
                Text = string.Format("Is now {0}", e.Value)
            };

            DisplayAlert("Power", lll.Text, "OK");
        }

        private void Onintensity_btnValueChanged(object sender, ValueChangedEventArgs e)
        {
            var lll = new Label
            {
                Text = string.Format("Is now {0}", e.NewValue)
            };

            DisplayAlert("Intensity", lll.Text, "OK");
        }
    }
}