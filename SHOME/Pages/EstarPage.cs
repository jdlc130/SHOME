using System.Collections.Generic;
using SHOME.Data;
using SHOME.Pages;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace SHOME
{
    public class EstarPage : ContentPage
    {
        public List<Division> Divisions = new List<Division>();
		static bool teste = false;

		//TODO se fizer sentido e for mais prático fazes aqui o foreach em vez de mandar o iterator.
		public async void DivisionData(int iterator, params object[] devices)
		{
			var tt = 0;
			var json = await WebServicesData.SyncTask("GET", "division");
			var result = json[1];
			var dd = json[1].Count;
			//TODO em vez de "idDivision" é o type (SERVER DOWN)
			var division = new Division(
				result["divisionName"],
				result["typeDivision"]
			);
			Divisions.Add(division);


			var deviceLock = new Devices("lock", "lock");
			var deviceLights = new Devices("Luzes", "lights");
			var deviceCctv = new Devices("Camaras", "cctv");
			division.AddDivice(deviceLock);
			tt++;
			//TODO FOREACH para adicionar devices
			if (dd == tt)
			{

			}

		}


		//public int tabb;
		//public EstarPage(int tab)
		//{
		//	tabb = tab;
		//}

		public class Devices
		{
			public Devices(string name, string type)
			{
				Name = name;
				Type = type;
			}

			public string Name { get; set; }
			public string Type { get; set; }
		}

		public class Division
		{
			public Division(string name, string type)
			{
				Name = name;
				Type = type;
			}

			public string Name { get; set; }
			public string Type { get; set; }

			public List<Devices> devices { get; } = new List<Devices>();

			public void AddDivice(Devices dev)
			{
				devices.Add(dev);
			}
		}

		public EstarPage(int tabb)
        {


            BackgroundImage = "home_n.png";
            var semiTransparentColor = new Color(0, 0, 0, 0.5);
            BackgroundColor = semiTransparentColor;

            /// Device
            var deviceLock = new Devices("lock", "lock");
            var deviceLights = new Devices("Luzes", "lights");
            var deviceCctv = new Devices("Camaras", "cctv");
            var deviceAudio = new Devices("audio", "audio");
            var deviceBlinds = new Devices("blinds", "blinds");
            var deviceAC = new Devices("ac", "ac");
            var deviceWeather = new Devices("weather", "weather");
            var deviceIrrigation = new Devices("irrigation", "irrigation");
			//if (tabb == 1)
			//{
			//    // Division
			//    var roomm = new Division("Quarto de Antonio", "bedroom");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceAC);

			//    roomm = new Division("Quarto da Ana", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceLock);
			//    roomm.AddDivice(deviceBlinds);
			//    roomm.AddDivice(deviceAC);


			//    roomm = new Division("Quarto Maria", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceAC);
			//    roomm.AddDivice(deviceAudio);


			//    roomm = new Division("Quarto Joana", "bedroom");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLock);
			//    roomm.AddDivice(deviceAudio);
			//}
			//if (tabb == 2)
			//{
			//    // Division

			//    var roomm = new Division("Sala de estar", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceAC);
			//    roomm.AddDivice(deviceLock);
			//    roomm.AddDivice(deviceBlinds);


			//    roomm = new Division("Escritorio", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceCctv);
			//    roomm.AddDivice(deviceLock);
			//    roomm.AddDivice(deviceAudio);
			//    roomm.AddDivice(deviceAC);
			//}

			//if (tabb == 3)
			//{
			//    // Division
			//    var roomm = new Division("All", "bedroom");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceIrrigation);
			//    roomm.AddDivice(deviceAC);
			//    roomm.AddDivice(deviceWeather);

			//    // Division
			//    roomm = new Division("Quarto de Antonio", "bedroom");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceAC);

			//    roomm = new Division("Quarto da Ana", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceLock);
			//    roomm.AddDivice(deviceBlinds);
			//    roomm.AddDivice(deviceAC);


			//    roomm = new Division("Quarto Maria", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceAC);
			//    roomm.AddDivice(deviceAudio);


			//    roomm = new Division("Quarto Joana", "bedroom");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLock);
			//    roomm.AddDivice(deviceAudio);

			//    // Division

			//    roomm = new Division("Sala de estar", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceAC);
			//    roomm.AddDivice(deviceLock);
			//    roomm.AddDivice(deviceBlinds);


			//    roomm = new Division("Escritorio", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceCctv);
			//    roomm.AddDivice(deviceLock);
			//    roomm.AddDivice(deviceAudio);
			//    roomm.AddDivice(deviceAC);


			//    // Division
			//    roomm = new Division("Jardim da direita", "bedroom");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceIrrigation);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceCctv);
			//    roomm.AddDivice(deviceLock);


			//    roomm = new Division("Jardim principal", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceIrrigation);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceBlinds);


			//    roomm = new Division("Entrada", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLock);
			//}

			//if (tabb == 4)
			//{
			//    // Division
			//    var roomm = new Division("Jardim da direita", "bedroom");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceIrrigation);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceCctv);
			//    roomm.AddDivice(deviceLock);


			//    roomm = new Division("Jardim principal", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceIrrigation);
			//    roomm.AddDivice(deviceLights);
			//    roomm.AddDivice(deviceBlinds);


			//    roomm = new Division("Entrada", "estar");
			//    Divisions.Add(roomm);
			//    roomm.AddDivice(deviceLock);
			//}



			while (teste)
			{
				var buttonLights = new List<Image>();
				var buttonCctvs = new List<Image>();
				var buttonACs = new List<Image>();
				var buttonListAudio = new List<Image>();
				var buttonListBlinds = new List<Image>();
				var buttonListLock = new List<Image>();
				var buttonListIrrigation = new List<Image>();
				var buttonListWeather = new List<Image>();

				var stack = new StackLayout(); ////
				Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);

				//var tt = await RequestData.SyncTask("division");


				foreach (var s in Divisions)
				{
					var grid = new Grid();
					grid.BackgroundColor = new Color(0, 0, 0, 0.5);
					grid.RowDefinitions = new RowDefinitionCollection
				{
					new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
				};
					//grid.HorizontalOptions = LayoutOptions.StartAndExpand;
					stack.Children.Add(new Label { Text = "\n\n", HorizontalTextAlignment = TextAlignment.Center });
					stack.Children.Add(new Label
					{
						Text = s.Name,
						HorizontalTextAlignment = TextAlignment.Center,
						BackgroundColor = new Color(74, 154, 220, 0.7)
					});


					var rowGrid = 0;
					var columnGrid = 0;
					foreach (var dev in s.devices)
					{
						switch (dev.Type)
						{
							case "lights":
								var buttonLight = new Image
								{
									Source = "lights.png",
									WidthRequest = 70,
									HeightRequest = 70,
									Opacity = 2
								};
								buttonLights.Add(buttonLight);
								grid.Children.Add(buttonLight, columnGrid, rowGrid);
								break;

							case "cctv":
								var buttonCctv = new Image { Source = "cctv.png", WidthRequest = 7, HeightRequest = 70 };
								buttonCctvs.Add(buttonCctv);
								grid.Children.Add(buttonCctv, columnGrid, rowGrid);
								break;

							case "ac":
								var buttonAC = new Image { Source = "ac.png", WidthRequest = 7, HeightRequest = 70 };
								buttonACs.Add(buttonAC);
								grid.Children.Add(buttonAC, columnGrid, rowGrid);
								break;

							case "audio":
								var buttonAudio = new Image { Source = "audio.png", WidthRequest = 7, HeightRequest = 70 };
								buttonListAudio.Add(buttonAudio);
								grid.Children.Add(buttonAudio, columnGrid, rowGrid);
								break;
							case "blinds":
								var buttonBlinds = new Image { Source = "blinds.png", WidthRequest = 7, HeightRequest = 70 };
								buttonListBlinds.Add(buttonBlinds);
								grid.Children.Add(buttonBlinds, columnGrid, rowGrid);
								break;
							case "lock":
								var buttonLock = new Image { Source = "lock.png", WidthRequest = 7, HeightRequest = 70 };
								buttonListLock.Add(buttonLock);
								grid.Children.Add(buttonLock, columnGrid, rowGrid);
								break;
							case "irrigation":
								var buttonIrrigation = new Image
								{
									Source = "irrigation.png",
									WidthRequest = 7,
									HeightRequest = 70
								};
								buttonListIrrigation.Add(buttonIrrigation);
								grid.Children.Add(buttonIrrigation, columnGrid, rowGrid);
								break;
							case "weather":
								var buttonWeather = new Image { Source = "weather.png", WidthRequest = 7, HeightRequest = 70 };
								buttonListWeather.Add(buttonWeather);
								grid.Children.Add(buttonWeather, columnGrid, rowGrid);
								break;
						}
						columnGrid++;
					}


					//grid.Opacity = 0.5;
					stack.Children.Add(new ScrollView
					{
						Content = grid,
						Orientation = ScrollOrientation.Horizontal
					});
				}


				var i = 0;
				while (i < buttonLights.Count)
				{
					buttonLights[i].GestureRecognizers.Add(new TapGestureRecognizer
					{
						Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
						NumberOfTapsRequired = 1
					});
					i++;
				}

				i = 0;
				while (i < buttonACs.Count)
				{
					buttonACs[i].GestureRecognizers.Add(new TapGestureRecognizer
					{
						Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
						NumberOfTapsRequired = 1
					});
					i++;
				}

				i = 0;
				while (i < buttonListLock.Count)
				{
					buttonListLock[i].GestureRecognizers.Add(new TapGestureRecognizer
					{
						Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
						NumberOfTapsRequired = 1
					});
					i++;
				}

				i = 0;
				while (i < buttonListAudio.Count)
				{
					buttonListAudio[i].GestureRecognizers.Add(new TapGestureRecognizer
					{
						Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
						NumberOfTapsRequired = 1
					});
					i++;
				}

				i = 0;
				while (i < buttonListBlinds.Count)
				{
					buttonListBlinds[i].GestureRecognizers.Add(new TapGestureRecognizer
					{
						Command = new Command(() => { Navigation.PushModalAsync(new LightsPage()); }),
						NumberOfTapsRequired = 1
					});
					i++;
				}

				i = 0;
				while (i < buttonCctvs.Count)
				{
					buttonCctvs[i].GestureRecognizers.Add(new TapGestureRecognizer
					{
						Command = new Command(() => { Navigation.PushModalAsync(new CameraPage()); }),
						NumberOfTapsRequired = 1
					});
					i++;
				}

				i = 0;
				while (i < buttonListIrrigation.Count)
				{
					buttonListIrrigation[i].GestureRecognizers.Add(new TapGestureRecognizer
					{
						Command = new Command(() => { Navigation.PushModalAsync(new GestaoPage()); }),
						NumberOfTapsRequired = 1
					});
					i++;
				}

				i = 0;
				while (i < buttonListWeather.Count)
				{
					buttonListWeather[i].GestureRecognizers.Add(new TapGestureRecognizer
					{
						Command = new Command(() => { Navigation.PushModalAsync(new MyCarouselPage()); }),
						NumberOfTapsRequired = 1
					});
					i++;
				}
				/*
				buttonCctv.GestureRecognizers.Add(new TapGestureRecognizer(sender =>
				{



					buttonCctv.Opacity = 0.6;
					buttonCctv.FadeTo(1);
					Navigation.PushModalAsync(new CameraPage());

				}));
	*/
				//this.Opacity = 0.5;

				var scollVertical = new ScrollView
				{
					Content = stack,
					Orientation = ScrollOrientation.Vertical
				};

				Content = scollVertical;
			}
        }

      
    }
}