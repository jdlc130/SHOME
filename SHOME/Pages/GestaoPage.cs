using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SHOME
{
	public class GestaoPage : ContentPage
	{

        class Values
        {
            public Values(string titulo, string value, Boolean power)
            {
                this.Titulo = titulo;
                this.Value = value;
                this.Power = power;
            }

            public string Titulo { private  set; get; }
            public string Value { private  set; get; }
            public Boolean Power { private set; get; }
        };

        public GestaoPage()
		{

            Label header = new Label
            {
                Text = "Energy Manager",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Define some data.
            List<Values> people = new List<Values>
            {
                new Values("Home appliances", "65%", true),
                new Values("Nivel de energia", "35%", false),
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

                    var Value_lbl = new Label();
                    Value_lbl.SetBinding(Label.TextProperty, "Value");

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
                                        HorizontalOptions = LayoutOptions.End,
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
                    listView,
                    suggestion_btn
                }
            };

        }

        void Onsuggestion_btnClicked(object sender, EventArgs e)
        {

            /* BDD INTERACTION */

            DisplayAlert("Suggestion", "You have been alerted", "OK");
        }

        void power_btn_Toggled(object sender, ToggledEventArgs e)
        {

            var lll = new Label
            {
                Text = string.Format("Switch is now {0}", e.Value)
            };

            DisplayAlert("Suggestion", lll.Text, "OK");
        }
    }
}

