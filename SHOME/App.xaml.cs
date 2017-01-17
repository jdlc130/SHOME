using Xamarin.Forms;
using System.Collections.Generic;
namespace SHOME
{
	public partial class App : Application
	{

		public static bool IsUserLoggedIn { get; set; }

		public App()
		{
	

			Home home = new Home();
			Division room = new Division("Quarto de JD", "bedroom");
			home.add(room);
			room = new Division("Sala de estar", "livingRoom");
			home.add(room);


			MainPage = new MenuPage();
			/*
			if (Device.OS == TargetPlatform.iOS)
			{
				if (!IsUserLoggedIn)
				{
					MainPage = new NavigationPage(new LoginPage());
				}
				else {
					MainPage = new NavigationPage(new SHOME.MenuPage());
				}

			}
			else 
			{
				MainPage = new MenuPage();
			}*/
			/*
			if (!IsUserLoggedIn)
			{
				MainPage = new NavigationPage(new LoginPage());
			}
			else {
				MainPage = new NavigationPage(new SHOME.MenuPage());
			}*/
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
