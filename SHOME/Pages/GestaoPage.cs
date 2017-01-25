using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SHOME
{
    public class GestaoPage : ContentPage
    {
        public GestaoPage()
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
                        "Images/header_energyManagement.png",
                        "header_energyManagement.png",
                        "Images/header_energyManagement.png")
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };


            const string energy = "60%";
            var tittleEnergyLbl = new Label
            {
                Text = "Panel Energy Level",
                FontSize = 18
            };
            var energyValue = new Label
            {
                Text = energy,
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.End
            };

            var infoGrid = new Grid
            {
                Padding = new Thickness(10, 10, 10, 10),
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

            // Define some data.
            var appliances = new List<Values>
            {
                new Values("Washing Machine", true),
                new Values("Dishwasher", false),
                new Values("Machine Dryer", false),
                new Values("Cook Robot", true)
            };

            // Create the ListView.
            var listView = new ListView
            {
                // Source of data items.
                ItemsSource = appliances,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    var tittleLbl = new Label();
                    tittleLbl.SetBinding(Label.TextProperty, "Titulo");

                    var powerBtn = new Switch();
                    powerBtn.SetBinding(Switch.IsToggledProperty, "Power");
                    powerBtn.Toggled += (sender, e) =>
                    {
                        //TODO   
                    };

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
                                        powerBtn
                                    }
                                }
                            }
                        }
                    };
                })
            };


            var view = new StackLayout
            {
                Padding = new Thickness(10, 20, 10, 10),
                Children = { listView}
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
            /* BDD INTERACTION */

            DisplayAlert("We suggest you to plug in", "Washing Machine, Cook Robot", "Submit", "Cancel");
        }
        
        private class Values
        {
            public Values(string titulo, bool power)
            {
                Titulo = titulo;
                Power = power;
            }

            public string Titulo { private set; get; }
            public bool Power { private set; get; }
        }
    }
}