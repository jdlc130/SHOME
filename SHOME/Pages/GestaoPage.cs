using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SHOME
{
	public class GestaoPage : ContentPage
	{

        class Values
        {
            public Values(string titulo, string value)
            {
                var Titulo = titulo;
                var Value = value;
            }

            public string Titulo { private  set; get; }

            public string Value { private  set; get; }

        };

        public GestaoPage()
		{

            Label header = new Label
            {
                Text = "Power level",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Define some data.
            List<Values> people = new List<Values>
            {
                new Values("Home appliances", "65%"),
                new Values("Nivel de energia", "35%"),
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
                    Label Tittle_lbl = new Label();
                    Tittle_lbl.SetBinding(Label.TextProperty, "Titulo");

                    Label Value_lbl = new Label();
                    Value_lbl.SetBinding(Label.TextProperty, "Value");

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            VerticalOptions = LayoutOptions.Center,
                            Children =
                            {
                                Tittle_lbl,
                                Value_lbl
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
            DisplayAlert("Suggestion", "You have been alerted", "OK");
        }
    }
}

