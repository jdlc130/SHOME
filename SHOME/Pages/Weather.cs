using Xamarin.Forms;
using SHOME.Data;

namespace SHOME
{
    public class Weather : ContentPage
    {
        public string Temperature { get; set; }
        public string Wind { get; set; }
        public string WindD { get; set; }
        public string Humidity { get; set; }
		public string InsideTemperature { get; set; }
       


		public async void GetTemperature()
		{

			var aux = 0;
			var json = await WebServicesData.SyncTask("GET", "temperature");
			var size = json.Count;

			var teste = json["Device_Num_7"];
			var t = teste["states"];
			var tee = t[0];
			var ee = tee["value"];
			InsideTemperature = ee;

			Constructor();


		}



		public Weather()
        {
			
			GetTemperature();
        }

		public void Constructor()
		{ 

			this.Temperature = " ";
			this.Wind = " ";
			this.WindD = " ";
			this.Humidity = " ";

			Image header = new Image { Source = "header_weather.png" };
			var conditions = new Label
			{
				Text = "Enviromental conditions outside",
				TextColor = Color.Teal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
			};
			var location = new Entry();

			Button buttonS = new Button { Text = "Submit" };

			var Temperature = new Label { };
			var Wind = new Label { };
			var Humidity = new Label { };
			var WindDirection = new Label { };

			var TemperatureInside = new Label { };
			TemperatureInside.Text = InsideTemperature;

			buttonS.Clicked += async (sender, e) =>
			{
				if ((location) != null)
				{
					Weather weather = await WeatherCore.GetWeather(location.Text);
					var temperture = weather.Temperature;

					if (weather != null)
					{
						Temperature.Text = "Temperature: " + weather.Temperature;
						Wind.Text = "Wind: " + weather.Wind;
						WindDirection.Text = "Wind Direction: " + weather.WindD;
						Humidity.Text = "Humidity: " + weather.Humidity;

					}
				}
			};

			var conditionsI = new Label
			{
				Text = "Enviromental conditions inside",
				TextColor = Color.Teal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
			};

			Content = new StackLayout
			{
				Children =
					{
						header,
					conditions,
						location,
						buttonS,
					Temperature,
					Wind,
					Humidity,
					WindDirection,
					conditionsI,
					TemperatureInside
					}
			};


		}
    }
}