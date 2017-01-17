using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SHOME
{
	public class EstarPage : ContentPage
	{
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

	

		public EstarPage()
		{
			Division roomm = new Division("Quarto de JD", "bedroom");
			divisions.Add(roomm);
			Devices device1 = new Devices("Luzes", "lights");
			roomm.addDivice(device1);
		    device1 = new Devices("Camaras", "cctv");
			roomm.addDivice(device1);
			roomm.addDivice(device1);
			roomm.addDivice(device1);


		
			Division room = new Division("Sala de estar", "livingRoom");
			divisions.Add(room);
			Devices device = new Devices("Camaras", "cctv");
			room.addDivice(device);
			device = new Devices("Luzes", "lights");
			room.addDivice(device);

			room = new Division("Escritorio", "livingRoom");
			divisions.Add(room);
			Devices device2 = new Devices("Camaras", "cctv");
			room.addDivice(device2);
			device2 = new Devices("Luzes", "lights");
			room.addDivice(device2);

			room = new Division("Escritorio", "livingRoom");
			divisions.Add(room);
			device2 = new Devices("Luzes", "lights");
			room.addDivice(device2);
			device2 = new Devices("Luzes", "lights");
			room.addDivice(device2);

			room = new Division("Escritorio", "livingRoom");
			divisions.Add(room);
			device2 = new Devices("Luzes", "lights");
			room.addDivice(device2);
			device2 = new Devices("Luzes", "lights");
			room.addDivice(device2);
			device2 = new Devices("Luzes", "lights");
			room.addDivice(device2);


			var buttonLight = new Image() { Source = "lights.png" };

			var buttonCctv = new Image() { Source = "cctv.png" };
		

			List<Image> buttonLights = new List<Image>();

			var stack = new StackLayout(); ////
			Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
			//var grid = new Grid();
			//var grid1 = new Grid();
			//var grid2 = new Grid();
		
			var row = 0;
			//var column = 0;

			foreach (Division s in divisions)
			{
				var grid = new Grid();

				//var grid2 = new Grid();

				stack.Children.Add(new Label() { Text = "\n\n\n\n"+s.Name, HorizontalTextAlignment = TextAlignment.Center });

				/*grid2.Children.Add(new Label() { Text = s.Name , BackgroundColor = Color.Red }, column, row );
				grid.Children.Add(grid2, column, row);
				row++;*/

				var rowGrid = 0;
				var columnGrid  = 0;
				foreach (Devices dev in s.devices) {
					switch (dev.Type)
					{
						case "lights":
							buttonLight = new Image() { Source = "lights.png" };
							buttonLights.Add(buttonLight);
							grid.Children.Add(buttonLight, columnGrid, rowGrid);
						break;

						case "cctv":
							buttonCctv = new Image() { Source = "cctv.png" };
							grid.Children.Add(buttonCctv, columnGrid, rowGrid);
						break;
					}
					columnGrid++;

				}
				grid.BackgroundColor = Color.Transparent;
				//grid.Opacity = 0.5;
				stack.Children.Add(new ScrollView
				{
					Content = grid,
					Orientation = ScrollOrientation.Horizontal,
				});

				/*grid.Children.Add(new ScrollView
				{
					Content = grid1,
					Orientation = ScrollOrientation.Horizontal,
				}, column, row);*/

				row++;

			}



				var i = 0;
 				while(i < buttonLights.Count)
			{  
				
				//	button.GestureRecognizers.Add(new TapGestureRecognizer(CameraPage));
				buttonLights[i].GestureRecognizers.Add(new TapGestureRecognizer(sender =>
				{



				buttonLights[i].Opacity = 0.6;
				buttonLights[i].FadeTo(1);
				// Navigation.PushModalAsync(new LightsPage());


					}));
				i++;
				}
				 

			buttonCctv.GestureRecognizers.Add(new TapGestureRecognizer(sender =>
			{



				buttonCctv.Opacity = 0.6;
				buttonCctv.FadeTo(1);
				Navigation.PushModalAsync(new CameraPage());

			}));



			var scollVertical = new ScrollView()
			{
				Content = stack,
				Orientation = ScrollOrientation.Vertical,
			};

			Content = scollVertical;

		}
	}
}

