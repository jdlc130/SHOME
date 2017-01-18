using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SHOME
{
    public class GestaoPage : ContentPage
    {

        class Values
        {
            public Values(string titulo, Boolean power )
            {
                this.Titulo = titulo;
                this.Power = power;
  
            }

            public string Titulo { private set; get; }
            public Boolean Power { private set; get; }

        };

        public GestaoPage()
        {

            Padding = new Thickness(20, 20, 20, 20);

            Label header = new Label
            {
                Text = "Energy Manager",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            var Energy = "60%";

            var Tittle_energy_lbl = new Label()
              {
                Text = "Panel Energy Level" + "      " + Energy,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };

            // Define some data.
            List<Values> people = new List<Values>
            {
                new Values("Washing Machine",  true),
                new Values("Dishwasher",  false),
                new Values("Machine Dryer",  false),
                new Values("Cook Robot",  true),
            };

            // Create the ListView.
            ListView listView = new ListView
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

        void Onsuggestion_btnClicked(object sender, EventArgs e)
        {

            /* BDD INTERACTION */

            DisplayAlert("We suggest you to plug in", "Washing Machine, Cook Robot", "Submit", "Cancel");
        }

        void power_btn_Toggled(object sender, ToggledEventArgs e)
        {

            var lll = new Label
            {
                Text = string.Format("Is now {0}", e.Value)
            };

         /*   DisplayAlert("Power", lll.Text, "OK"); */

        }
    }
}
