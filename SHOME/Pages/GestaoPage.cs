using System;
using System.Collections.Generic;
using System.Linq;
using SHOME.Data;
using Xamarin.Forms;

namespace SHOME
{
    public class GestaoPage : ContentPage
    {
        public GestaoPage()
        {
            GetDevices();
        }

        public string Suggestion { set; get; }
        public int EnergyProduced { get; set; }

        public List<Values> Devices { get; set; } = new List<Values>();

        private void Construtor()
        {
            var header = new Image
            {
                Source = new FileImageSource
                {
                    File = Device.OnPlatform(
                        "Images/header_energyManagement.png",
                        "header_energyManagement.png",
                        "Images/header_energyManagement.png")
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            var tittleEnergyLbl = new Label
            {
                Text = "Produced Energy",
                FontSize = 18
            };
            var energyValue = new Label
            {
                Text = EnergyProduced + " Wh",
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.End
            };

            var infoGrid = new Grid
            {
                Padding = new Thickness(10, 10, 20, 10),
                BackgroundColor = new Color(0, 0, 0, 0),
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    }
                }
            };
            infoGrid.Children.Add(tittleEnergyLbl, 0, 0);
            infoGrid.Children.Add(energyValue, 1, 0);

            // Create the ListView.
            var value = -1;
            var listView = new ListView
            {
                // Source of data items.
                ItemsSource = Devices,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                    {
                        value++;

                        // Create views with bindings for displaying each property.
                        var tittleLbl = new Label();
                        tittleLbl.SetBinding(Label.TextProperty, "Name");

                        // Return an assembled ViewCell.
                        return new ViewCell
                        {
                            View = new StackLayout
                            {
                                Padding = new Thickness(0, 5),
                                Orientation = StackOrientation.Horizontal,
                                VerticalOptions = LayoutOptions.Center,
                                Children =
                                {
                                    tittleLbl,
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalOptions = LayoutOptions.EndAndExpand,
                                        Children =
                                        {
                                            Devices[value].PowerSwitch
                                        }
                                    }
                                }
                            }
                        };
                    }
                )
            };

            var view = new StackLayout
            {
                Padding = new Thickness(10, 20, 10, 10),
                Children = {listView}
            };

            var suggestionBtn = new Button
            {
                Text = "SUGGESTIONS",
                FontFamily = "Roboto",
                FontSize = 18,
                VerticalOptions = LayoutOptions.End
            };
            suggestionBtn.Clicked += Onsuggestion;

            Content = new StackLayout
            {
                Children =
                {
                    header,
                    infoGrid,
                    view,
                    suggestionBtn
                }
            };
        }

        private void Onsuggestion(object sender, EventArgs e)
        {
            SuggestionCreator();
            var labelTitle = new Label
            {
                Text = "Please, consider",
                FontFamily = "Roboto",
                FontSize = 18
            };

            var labelSuggestion = new Label
            {
                Text = Suggestion,
                FontFamily = "Roboto",
                FontSize = 14
            };

            DisplayAlert(labelTitle.Text,
                labelSuggestion.Text,
                "Ok");
        }

        public async void GetDevices()
        {
            var aux = 0;
            var json = await WebServicesData.SyncTask("GET", "appliance");
            var size = json.Count;

            while (size > aux)
            {
                var result = json[aux];
                var value = new Values(
                    result["idAppliance"],
                    result["applianceName"],
                    result["consumo"],
                    result["state"]
                );
                value.PowerSwitch = new Switch {IsToggled = value.State};
                value.PowerSwitch.Toggled +=
                    async (sender, e) =>
                    {
                        await WebServicesData.SyncTask("POST", "appliance", value.ApplianceId, e.Value ? 1 : 0);
                        value.State = e.Value;
                    };

                Devices.Add(value);
                aux++;
            }
            GetEnergy();
        }

        private async void GetEnergy()
        {
            var json = await WebServicesData.SyncTask("GET", "box");
            EnergyProduced = json["power"];

            Construtor();
        }

        private void SuggestionCreator()
        {
            var suggestion =
                Devices.Where(appliance => (EnergyProduced - appliance.Consumption >= 0) && !appliance.State)
                    .Aggregate("", (current, appliance) => current + appliance.Name + ";");
            if (!string.IsNullOrEmpty(suggestion))
                Suggestion = "Turning on " + suggestion;
            else
                Suggestion = "Turning off non-essential appliances.";
        }

        public class Values
        {
            public Values(int id, string name, int consuption, bool state)
            {
                Name = name;
                State = state;
                ApplianceId = id;
                Consumption = consuption;
            }

            public string Name { set; get; }
            public bool State { get; set; }
            public int ApplianceId { get; }
            public int Consumption { get; }
            public Switch PowerSwitch { set; get; }
        }
    }
}