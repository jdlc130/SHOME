using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SHOME
{
    public class ListEventPage : ContentPage
    {
        class Event
        {
            public string Name { set; get; }
            public string Descripton { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan Timei { get; set; }
            public TimeSpan Timef { get; set; }
            public string Divison { get; set; }
            public string Device { get; set; }
        }

        public ListEventPage()
        {
            var button = new Button
            {
                Text = "Add Event",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var events = new List<Event>()
            {
                new Event()
                {
                    Name = "Party",
                    Descripton = "Festa de aniversário",
                    // new DateTime(2010,5,12),
                    // new TimeSpan(14:00:00),
                    // new TimeSpan(15:00:00), 
                    Divison = "Quarto",
                    Device = "Luzes"
                },

                new Event()
                {
                    Name = "Special Day",
                    Descripton = "Halloween",
                    // new DateTime(2016,10,31),
                    // new TimeSpan(20:00:00),
                    // new TimeSpan(00:00:00), 
                    Divison = "Rua",
                    Device = "Luzes"
                },

                new Event()
                {
                    Name = "MailDay",
                    Descripton = "Package delivery",
                    // new DateTime(2016,10,31),
                    // new TimeSpan(20:00:00),
                    // new TimeSpan(00:00:00), 
                    Divison = "Rua",
                    Device = "Fechadura"
                }

            }; 

            var dataTemplate = new DataTemplate(typeof(TextCell));
            dataTemplate.SetBinding(TextCell.TextProperty, "Name" );
            
            var listView = new ListView()
            {
                ItemsSource = events,
                ItemTemplate = dataTemplate

            };

            Content = new StackLayout
            {
                Children =
                {
                    button,
                    listView
                }
            };
        }
    };
}


