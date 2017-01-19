using System;
using System.Collections.Generic;
using SHOME.Pages;
using Xamarin.Forms;

namespace SHOME
{
	public class EstarPage : ContentPage
	{
		//public int tabb;
		//public EstarPage(int tab)
		//{
		//	tabb = tab;
		//}


		public class Devices
		{
			public string Name { get; set; }
			public string Type { get; set; }
			public Devices(string name, string type)
			{
				Name = name;
				Type = type;
			}


		}


		public class Division
		{
			public string Name { get; set; }
			public string Type { get; set; }
			public Division(string name, string type)
			{
				Name = name;
				Type = type;
			}

			List<Devices> _devices = new List<Devices>();

			public List<Devices> devices
			{
				get { return _devices; }

			}

			public void addDivice(Devices dev)
			{
				_devices.Add(dev);
			
			}
		}

		List<Division> divisions = new List<Division>();

	

		public EstarPage(int tabb)
		{
	
			this.BackgroundImage = "home_n.png";
			var semiTransparentColor = new Color(0, 0, 0, 0.5);
			this.BackgroundColor = semiTransparentColor;



			/// Device
			Devices deviceLock = new Devices("lock", "lock");
			Devices deviceLights = new Devices("Luzes", "lights");
			Devices deviceCctv = new Devices("Camaras", "cctv");
			Devices deviceAudio = new Devices("audio", "audio");
			Devices deviceBlinds = new Devices("blinds", "blinds");
			Devices deviceAC = new Devices("ac", "ac");
			Devices deviceWeather = new Devices("weather", "weather");
			Devices deviceIrrigation = new Devices("irrigation", "irrigation");
			if (tabb == 1)
			{


				// Division
				Division roomm = new Division("Quarto de Antonio", "bedroom");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceAC);

				roomm = new Division("Quarto da Ana", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceLock);
				roomm.addDivice(deviceBlinds);
				roomm.addDivice(deviceAC);




				roomm = new Division("Quarto Maria", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceAC);
				roomm.addDivice(deviceAudio);


				roomm = new Division("Quarto Joana", "bedroom");
				divisions.Add(roomm);
				roomm.addDivice(deviceLock);
				roomm.addDivice(deviceAudio);

			}
			if (tabb == 2)
			{


				// Division

				Division roomm = new Division("Sala de estar", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceAC);
				roomm.addDivice(deviceLock);
				roomm.addDivice(deviceBlinds);


				roomm = new Division("Escritorio", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceCctv);
				roomm.addDivice(deviceLock);
				roomm.addDivice(deviceAudio);
				roomm.addDivice(deviceAC);


		

			}

			if (tabb == 3)
			{

				// Division
				Division roomm = new Division("All", "bedroom");
				divisions.Add(roomm);
				roomm.addDivice(deviceIrrigation);
				roomm.addDivice(deviceAC);
				roomm.addDivice(deviceWeather);

				// Division
				roomm = new Division("Quarto de Antonio", "bedroom");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceAC);

				roomm = new Division("Quarto da Ana", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceLock);
				roomm.addDivice(deviceBlinds);
				roomm.addDivice(deviceAC);




				roomm = new Division("Quarto Maria", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceAC);
				roomm.addDivice(deviceAudio);


				roomm = new Division("Quarto Joana", "bedroom");
				divisions.Add(roomm);
				roomm.addDivice(deviceLock);
				roomm.addDivice(deviceAudio);

				// Division

				roomm = new Division("Sala de estar", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceAC);
				roomm.addDivice(deviceLock);
				roomm.addDivice(deviceBlinds);


				roomm = new Division("Escritorio", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceCctv);
				roomm.addDivice(deviceLock);
				roomm.addDivice(deviceAudio);
				roomm.addDivice(deviceAC);


				// Division
				roomm = new Division("Jardim da direita", "bedroom");
				divisions.Add(roomm);
				roomm.addDivice(deviceIrrigation);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceCctv);
				roomm.addDivice(deviceLock);


				roomm = new Division("Jardim principal", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceIrrigation);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceBlinds);


				roomm = new Division("Entrada", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLock);

			}

			if (tabb == 4)
			{

				// Division
				Division roomm = new Division("Jardim da direita", "bedroom");
				divisions.Add(roomm);
				roomm.addDivice(deviceIrrigation);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceCctv);
				roomm.addDivice(deviceLock);
	

				roomm = new Division("Jardim principal", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceIrrigation);
				roomm.addDivice(deviceLights);
				roomm.addDivice(deviceBlinds);


				roomm = new Division("Entrada", "estar");
				divisions.Add(roomm);
				roomm.addDivice(deviceLock);
			}
			List<Image> buttonLights = new List<Image>();
			List<Image> buttonCctvs = new List<Image>();
			List<Image> buttonACs = new List<Image>();
			List<Image> buttonListAudio = new List<Image>();
			List<Image> buttonListBlinds = new List<Image>();
			List<Image> buttonListLock = new List<Image>();
			List<Image> buttonListIrrigation = new List<Image>();
			List<Image> buttonListWeather = new List<Image>();

			var stack = new StackLayout(); ////
			Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);


			foreach (Division s in divisions)
			{
				var grid = new Grid();
				grid.BackgroundColor = new Color(0, 0, 0, 0.5);
				grid.RowDefinitions = new RowDefinitionCollection {
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
			};
				//grid.HorizontalOptions = LayoutOptions.StartAndExpand;
				stack.Children.Add(new Label() { Text = "\n\n" , HorizontalTextAlignment = TextAlignment.Center });
				stack.Children.Add(new Label() { Text = s.Name, HorizontalTextAlignment = TextAlignment.Center , BackgroundColor = new Color(74, 154, 220, 0.7)});


				var rowGrid = 0;
				var columnGrid  = 0;
				foreach (Devices dev in s.devices) {
					switch (dev.Type)
					{
						case "lights":
							var buttonLight = new Image(){Source = "lights.png", WidthRequest = 70, HeightRequest = 70, Opacity = 2};
							buttonLights.Add(buttonLight);
							grid.Children.Add(buttonLight, columnGrid, rowGrid);
						break;

						case "cctv":
							var buttonCctv = new Image() { Source = "cctv.png",  WidthRequest = 7, HeightRequest = 70 };
							buttonCctvs.Add(buttonCctv);
							grid.Children.Add(buttonCctv, columnGrid, rowGrid);
						break;

						case "ac":
							var buttonAC = new Image() { Source = "ac.png", WidthRequest = 7, HeightRequest = 70 };
							buttonACs.Add(buttonAC);
							grid.Children.Add(buttonAC, columnGrid, rowGrid);
						break;
						
						case "audio":
							var buttonAudio = new Image() { Source = "audio.png", WidthRequest = 7, HeightRequest = 70 };
							buttonListAudio.Add(buttonAudio);
							grid.Children.Add(buttonAudio, columnGrid, rowGrid);
						break;
						case "blinds":
							var buttonBlinds = new Image() { Source = "blinds.png", WidthRequest = 7, HeightRequest = 70 };
							buttonListBlinds.Add(buttonBlinds);
							grid.Children.Add(buttonBlinds, columnGrid, rowGrid);
						break;
						case "lock":
							var buttonLock = new Image() { Source = "lock.png", WidthRequest = 7, HeightRequest = 70 };
							buttonListLock.Add(buttonLock);
							grid.Children.Add(buttonLock, columnGrid, rowGrid);
						break;
						case "irrigation":
							var buttonIrrigation = new Image() { Source = "irrigation.png", WidthRequest = 7, HeightRequest = 70 };
							buttonListIrrigation.Add(buttonIrrigation);
							grid.Children.Add(buttonIrrigation, columnGrid, rowGrid);
						break;
						case "weather":
							var buttonWeather = new Image() { Source = "weather.png", WidthRequest = 7, HeightRequest = 70 };
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
					Orientation = ScrollOrientation.Horizontal,
				});

			

		

			}



				var i = 0;
 				while(i < buttonLights.Count)
			{  
				buttonLights[i].GestureRecognizers.Add(new TapGestureRecognizer
				{
					Command = new Command(() =>
					{
						Navigation.PushModalAsync(new LightsPage());
					}),
                NumberOfTapsRequired = 1

				});
				i++;
				}

			i = 0;
			while (i < buttonACs.Count)
			{
				buttonACs[i].GestureRecognizers.Add(new TapGestureRecognizer
				{
					Command = new Command(() =>
					{
						Navigation.PushModalAsync(new LightsPage());
					}),
					NumberOfTapsRequired = 1

				});
				i++;
			}

			i = 0;
			while (i < buttonListLock.Count)
			{
				buttonListLock[i].GestureRecognizers.Add(new TapGestureRecognizer
				{
					Command = new Command(() =>
					{
						Navigation.PushModalAsync(new LightsPage());
					}),
					NumberOfTapsRequired = 1

				});
				i++;
			}

			i = 0;
			while (i < buttonListAudio.Count)
			{
				buttonListAudio[i].GestureRecognizers.Add(new TapGestureRecognizer
				{
					Command = new Command(() =>
					{
						Navigation.PushModalAsync(new LightsPage());
					}),
					NumberOfTapsRequired = 1

				});
				i++;
			}

			i = 0;
			while (i < buttonListBlinds.Count)
			{
				buttonListBlinds[i].GestureRecognizers.Add(new TapGestureRecognizer
				{
					Command = new Command(() =>
					{
						Navigation.PushModalAsync(new LightsPage());
					}),
					NumberOfTapsRequired = 1

				});
				i++;
			}

			i = 0;
			while (i < buttonCctvs.Count)
			{
				buttonCctvs[i].GestureRecognizers.Add(new TapGestureRecognizer
				{
					Command = new Command(() =>
					{
						Navigation.PushModalAsync(new CameraPage());
					}),
					NumberOfTapsRequired = 1

				});
				i++;
			}

			i = 0;
			while (i < buttonListIrrigation.Count)
			{
				buttonListIrrigation[i].GestureRecognizers.Add(new TapGestureRecognizer
				{
					Command = new Command(() =>
					{
						Navigation.PushModalAsync(new GestaoPage());
					}),
					NumberOfTapsRequired = 1

				});
				i++;
			}

			i = 0;
			while (i < buttonListWeather.Count)
			{
				buttonListWeather[i].GestureRecognizers.Add(new TapGestureRecognizer
				{
					Command = new Command(() =>
					{
						Navigation.PushModalAsync(new MyCarouselPage());
					}),
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

			var scollVertical = new ScrollView()
			{
				Content = stack,
				Orientation = ScrollOrientation.Vertical,
			};

			Content = scollVertical;


			   
				

		}
	}
}

