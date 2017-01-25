using Xamarin.Forms;
using System.Collections.Generic;
namespace SHOME
{
	public partial class App : Application
	{

		public static bool IsUserLoggedIn { get; set; }

		public App()
		{


			MainPage = new NavigationPage(new SHOME.MenuPage());
			//	MainPage = new MenuPage();
			//MainPage = new NavigationPage(new LoginPage());
			//MainPage = new NavigationPage(new MenuPage());

				//if (!IsUserLoggedIn)
				//{
				//	MainPage = new NavigationPage(new LoginPage());
				//}
				//else {
				//	MainPage = new NavigationPage(new SHOME.MenuPage());
				//}
			
			//}
			//else 
			//{
			//	if (!IsUserLoggedIn)
			//	{
			//		MainPage = new NavigationPage(new LoginPage());
			//	}
			//	else {
			//		MainPage = new NavigationPage(new SHOME.MenuPage());
			//	}

			//}
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
