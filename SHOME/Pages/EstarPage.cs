using System;

using Xamarin.Forms;

namespace SHOME
{
	public class EstarPage : ContentPage
	{
		public EstarPage()
		{

			Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
			var grid = new Grid();
			var grid1 = new Grid();
			var grid2 = new Grid();

			grid1.Children.Add(new Label() { Text = "Sala de estar" }, 0,0);
			grid.Children.Add(new ScrollView
			{
				Content = grid1,
				Orientation = ScrollOrientation.Horizontal,
			}, 0, 1);

			grid.Children.Add(new Label() { Text = "Zona de lazer" }, 0, 3);
			grid.Children.Add(new ScrollView
			{
				Content = grid2,
				Orientation = ScrollOrientation.Horizontal,
			}, 0, 4);

	

			grid1.Children.Add(new Image()
			{
			Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("lights1.png"),
			Android: ImageSource.FromFile("lights1.png"),
			WinPhone: ImageSource.FromFile("lights1.png"))
			}, 0, 1);

			grid1.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("Cameras.png"),
			Android: ImageSource.FromFile("Cameras.png"),
			WinPhone: ImageSource.FromFile("Cameras.png"))
			}, 1, 1);

			grid1.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("icon4.png"),
			Android: ImageSource.FromFile("icon4.png"),
			WinPhone: ImageSource.FromFile("icon4.png"))
			},2, 1);

			grid1.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("icon4.png"),
			Android: ImageSource.FromFile("icon4.png"),
			WinPhone: ImageSource.FromFile("icon4.png"))
			}, 3, 1);

			grid1.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
				iOS: ImageSource.FromFile("security.png"),
				Android: ImageSource.FromFile("security.png"),
				WinPhone: ImageSource.FromFile("security.png"))
			}, 4, 1);

			grid1.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
				iOS: ImageSource.FromFile("security.png"),
				Android: ImageSource.FromFile("security.jpg"),
				WinPhone: ImageSource.FromFile("security.png"))
			}, 5, 1);

			grid1.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
				iOS: ImageSource.FromFile("security.png"),
				Android: ImageSource.FromFile("security.png"),
				WinPhone: ImageSource.FromFile("security.png"))
			}, 6, 1);



			grid2.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("icon5.png"),
			Android: ImageSource.FromFile("icon5.png"),
			WinPhone: ImageSource.FromFile("icon5.png"))
			}, 0, 0);

			grid2.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("icon4.png"),
			Android: ImageSource.FromFile("icon4.png"),
			WinPhone: ImageSource.FromFile("icon4.png"))
			}, 1, 0);
		
			grid2.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("Cameras.png"),
			Android: ImageSource.FromFile("Cameras.png"),
			WinPhone: ImageSource.FromFile("Cameras.png"))
			}, 2, 0);

			grid2.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("security.png"),
			Android: ImageSource.FromFile("security.png"),
			WinPhone: ImageSource.FromFile("security.png"))
			}, 3, 0);

			grid2.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("security.png"),
			Android: ImageSource.FromFile("security.png"),
			WinPhone: ImageSource.FromFile("security.png"))
			}, 4, 0);

			grid2.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("security.png"),
			Android: ImageSource.FromFile("security.png"),
			WinPhone: ImageSource.FromFile("security.png"))
			}, 5, 0);

			grid2.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("security.png"),
			Android: ImageSource.FromFile("security.png"),
			WinPhone: ImageSource.FromFile("security.png"))
			}, 6, 0);

			grid2.Children.Add(new Image()
			{
				Source = Device.OnPlatform(
			iOS: ImageSource.FromFile("security.png"),
			Android: ImageSource.FromFile("security.png"),
			WinPhone: ImageSource.FromFile("security.png"))
			}, 7, 0);


			 




			var scollVertical = new ScrollView()
			{
				Content = grid,
				Orientation = ScrollOrientation.Vertical,
			};

			Content = scollVertical;

		}
	}
}

