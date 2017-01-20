using System.Collections.Generic;
using SHOME.Data;
using Xamarin.Forms;
using System.Diagnostics;
using System.Json;

namespace SHOME.Pages
{
	public class LightsPage : ContentPage
	{

        // Dictionary to get Color from color name.
        Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            { "Blue", Color.Blue },   { "Pink", Color.Pink },
            { "Green", Color.Green }, { "Yellow", Color.Yellow },
			{ "White", Color.White },   { "Red", Color.Red }
        };

        public LightsPage()
        {

            Padding = new Thickness(20, 20, 20, 20);

            Image header = new Image
            {
                Source = "lights.png",
                HorizontalOptions = LayoutOptions.Center
            };

            Label light_lbl = new Label
            {
                Text = "Principal",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
            };

            Switch power_btn = new Switch
            {
                HorizontalOptions = LayoutOptions.End
            };
           
            power_btn.Toggled += power_btn_Toggled;
            
            Slider intensity_btn = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            intensity_btn.ValueChanged += Onintensity_btnValueChanged;

            Picker picker = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string colorName in nameToColor.Keys)
            {
                picker.Items.Add(colorName);
            }

            // Create BoxView for displaying picked Color
            BoxView boxView = new BoxView
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
                    string colorName = picker.Items[picker.SelectedIndex];
                    boxView.Color = nameToColor[colorName];

					switch (colorName)
					{
						case "Blue":
							WebServicesData.SyncTask("POST", "Light", "changeColor", 17, 46920);
							break;

						case "Yellow":
							WebServicesData.SyncTask("POST", "Light", "changeColor", 17, 12750);
							break;

						case "Green":
							WebServicesData.SyncTask("POST", "Light", "changeColor", 17, 25500);
						break;
						
						case "Pink":
							WebServicesData.SyncTask("POST", "Light", "changeColor", 17, 57670);
						break;
							
						case "White":
							WebServicesData.SyncTask("POST", "Light", "changeColor", 17, 31456);
						break;
						case "Red":
							WebServicesData.SyncTask("POST", "Light", "changeColor", 17, 65280);
						break;

						
					}
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


        void power_btn_Toggled(object sender, ToggledEventArgs e)
        {
            var lll = new Label
            {
                Text = string.Format("Is now {0}", e.Value)
            };
			if (e.Value)
			{



			var json = WebServicesData.SyncTask("POST", "ToggleDevice", 17, 1);
				//var result = WebServicesData.SimpleParseData(json, "Status");

			}
			else
			{ 
			WebServicesData.SyncTask("POST","ToggleDevice", 17, 0);
			}



           DisplayAlert("Power", lll.Text, "OK");
        }




		void Onintensity_btnValueChanged(object sender, ValueChangedEventArgs e)
        {
            var lll = new Label
            {
                Text = string.Format("Is now {0}", e.NewValue)
            };

            DisplayAlert("Intensity", lll.Text, "OK");
        }


    }
}

