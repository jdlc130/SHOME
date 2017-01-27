using System.Collections.Generic;
using SHOME.Data;
using Xamarin.Forms;

namespace SHOME.Pages
{
    public class LightsPage : ContentPage
    {
        // Dictionary to get slider value from color code.
        private readonly Dictionary<int, int> _codeInt = new Dictionary<int, int>
        {
            {46920, 0},
            {57670, 1},
            {25500, 2},
            {12750, 3},
            {31456, 4},
            {65280, 5}
        };

        // Dictionary to get Color from color name.
        private readonly Dictionary<int, Color> _codeToColor = new Dictionary<int, Color>
        {
            {46920, Color.Blue},
            {57670, Color.Pink},
            {25500, Color.Green},
            {12750, Color.Yellow},
            {31456, Color.White},
            {65280, Color.Red}
        };

        // Dictionary to get Color code from slider value.
        private readonly Dictionary<int, int> _intCode = new Dictionary<int, int>
        {
            {0, 46920},
            {1, 57670},
            {2, 25500},
            {3, 12750},
            {4, 31456},
            {5, 65280}
        };

        private int _color;

        private double _intensity;
        private bool _state;

        public LightsPage(int id)
        {
            InitializeView(id);
        }

        /// <summary>
        /// Lights page constructor.
        /// </summary>
        /// <param name="id"></param>
        private void Construtor(int id)
        {
            var header = new Image
            {
                Source = "header_lights.png",
                HorizontalOptions = LayoutOptions.Center
            };

            var stateGrid = new Grid
            {
                Padding = new Thickness(10, 30, 10, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                }
            };
            var powerLabel = new Label
            {
                Text = "\t STATE",
                FontFamily = "Roboto",
                FontAttributes = FontAttributes.Bold,
                FontSize = 18,
                TextColor = Color.Gray
            };

            // set values when power is changed
            var powerBtn = new Switch
            {
                HorizontalOptions = LayoutOptions.End,
                IsToggled = _state
            };
            powerBtn.Toggled += async (sender, e) =>
            {
                var json = await WebServicesData.SyncTask("POST", "ToggleDevice", id, e.Value ? 1 : 0);
                if (json == null) return;
                var state = int.Parse(json["Status"]);
                _state = state == 1;
            };

            stateGrid.Children.Add(powerLabel, 0, 0);
            stateGrid.Children.Add(powerBtn, 1, 0);

            // set values when intensity is changed
            var intensitySlider = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = _intensity
            };
            intensitySlider.ValueChanged += async (sender, e) =>
            {
                var json = await WebServicesData.SyncTask("POST", "Light", "changeBright", id, e.NewValue);
                if (json == null) return;
                _intensity = double.Parse(json["BrightLevel"]);
            };
            var intensityStack = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                Children = {intensitySlider}
            };

            // set values when color is selected
            var index = _codeInt[_color];
            var colorSlider = new Slider
            {
                Minimum = 0,
                Maximum = 5,
                Value = index
            };
            colorSlider.ValueChanged += async (sender, e) =>
            {
                var json =
                    await WebServicesData.SyncTask("POST", "Light", "changeColor", id, _intCode[(int) e.NewValue]);
                _color = int.Parse(json["Color"]);
            };
            var colorImage = new Image
            {
                Source = "color_picker.png",
                Scale = 0.965
            };

            // A grid is created to display  a list of colors
            var colorGrid = new Grid
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                }
            };
            colorGrid.Children.Add(colorImage, 0, 0);
            colorGrid.Children.Add(colorSlider, 0, 0);

            // Is assigned to the content of the page the header, stategrid, intensity slide and color gird.
            Content = new StackLayout
            {
                Children =
                {
                    header,
                    stateGrid,
                    intensityStack,
                    colorGrid
                }
            };
        }

        /// <summary>
        /// Get data from Database to know the state of the light to update the view.
        /// </summary>
        /// <param name="id"></param>
        private async void InitializeView(int id)
        {
            var json = await WebServicesData.SyncTask("GET", "GetDeviceStatus", id);
            var index = 0;
            while (index < json.Count)
            {
                var result = json[index];
                var state = result["actuatorState"];
                var parameter = result["parameterName"];
                var parameterValue = result["parameterValue"];

                _state = state != 0;

                if (parameter == "Color")
                    _color = parameterValue;
                else if (parameter == "Brightness")
                    _intensity = parameterValue;
                index++;
            }
            Construtor(id);
        }
    }
}