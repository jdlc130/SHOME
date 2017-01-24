using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SHOME
{
    public class GestaoPage : ContentPage
    {
        public GestaoPage()
        {
            Padding = new Thickness(20, 20, 20, 20);

            var header = new Label
            {
                Text = "Energy Manager",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            var Energy = "60%";

            var Tittle_energy_lbl = new Label
            {
                Text = "Panel Energy Level" + "      " + Energy,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };

            // Define some data.
            var people = new List<Values>
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
                ItemsSource = people,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    var Tittle_lbl = new Label();
                    Tittle_lbl.SetBinding(Label.TextProperty, "Titulo");

                    var power_btn = new Switch();
                    power_btn.SetBinding(Switch.IsToggledProperty, "Power");
                    power_btn.Toggled += power_btn_Toggled;

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
                                Tittle_lbl,
                                new StackLayout
                                {
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.EndAndExpand,
                                    Children =
                                    {
                                        power_btn
                                    }
                                }
                            }
                        }
                    };
                })
            };

            var suggestion_btn = new Button
            {
                Text = "Suggestions"
            };
            suggestion_btn.Clicked += Onsuggestion_btnClicked;

            Content = new StackLayout
            {
                Children =
                {
                    header,
                    Tittle_energy_lbl,
                    listView,
                    suggestion_btn
                }
            };
        }

        private void Onsuggestion_btnClicked(object sender, EventArgs e)
        {
            /* BDD INTERACTION */

            DisplayAlert("We suggest you to plug in", "Washing Machine, Cook Robot", "Submit", "Cancel");
        }

        private void power_btn_Toggled(object sender, ToggledEventArgs e)
        {
            var lll = new Label
            {
                Text = string.Format("Is now {0}", e.Value)
            };

            /*   DisplayAlert("Power", lll.Text, "OK"); */
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