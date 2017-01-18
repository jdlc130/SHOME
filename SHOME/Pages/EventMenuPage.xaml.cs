using System;

using Xamarin.Forms;

namespace SHOME
{
    public partial class EventMenuPage : ContentPage
        {
            public EventMenuPage()
            {
                InitializeComponent();
            }

            public void OnCreate(Object sender, EventArgs e)
            {
                Navigation.PushAsync(new CreateEvent());
            }

            public void OnList(Object sender, EventArgs e)
            {
                Navigation.PushAsync(new ListEventPage());
            }

        public void OnMeteo(object sender, EventArgs e)
        {
          
            Navigation.PushAsync(new SliderViewPage());
            
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
         
            }

        private class SliderViewPage : Page
        {
        }
    }
    }