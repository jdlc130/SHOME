using System.Collections.Generic;
using SHOME.Data;
using SHOME.Pages;
using Xamarin.Forms;

namespace SHOME
{
    public class ListEventPage : ContentPage
    {
        public Button Buttons;
        public Image Header;

        public ListEventPage()
        {
			Header = new Image
			{
				Source = "header_events.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            Buttons = new Button
            {
                Text = "ADD EVENT",
                FontFamily = "Roboto",
                FontSize = 18,
                VerticalOptions = LayoutOptions.End
            };
            Buttons.Clicked += async (sender, e) => { await Navigation.PushModalAsync(new EventPage()); };

            GetEvents();
        }


        public List<Event> Events { get; set; } = new List<Event>();
        public async void GetEvents()
        {
            var aux = 0;
            var json = await WebServicesData.SyncTask("GET", "GetEvents");
            var size = json.Count;

            while (size > aux)
            {
                var result = json[aux];

                var eventS = new Event(
                    result["eventName"]
                );

                Events.Add(eventS);
                aux++;
            }
            Constructor();
        }

        public void Constructor()
        {
            var dataTemplate = new DataTemplate(typeof(TextCell));
            dataTemplate.SetBinding(TextCell.TextProperty, "Name");

            var listView = new ListView
            {
                ItemsSource = Events,
                ItemTemplate = dataTemplate
            };

            var stack = new StackLayout
            {
                Padding = new Thickness(20, 0, 20, 10),
                Children = { Buttons, listView}
                
            };

            Content = new StackLayout
            {
                Spacing = 20,
                Children =
                {
                    Header,
                    stack
                }
            };
        }

        public class Event
        {
            public Event(string name)
            {
                Name = name;
            }

            public string Name { set; get; }
            public string Descripton { get; set; }
            public string Divison { get; set; }
            public string Device { get; set; }
        }
    }
}