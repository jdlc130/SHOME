using System;

using Xamarin.Forms;
using System.Diagnostics;
using System.Json;
using SHOME.Data;

namespace SHOME
{
	public class LocksPage : ContentPage
	{

		public Boolean state;
		public Image imagelock = new Image
		{
			Source = "unlock.png",
			HorizontalOptions = LayoutOptions.Center
		};

		Image header = new Image
		{
			Source = "header_lock.png",
			HorizontalOptions = LayoutOptions.Center
		};


		Switch power_btn = new Switch
		{
			HorizontalOptions = LayoutOptions.Center
		};




		public LocksPage()
		{

			//Padding = new Thickness(0, 20, 20, 20);



			GetState();
		}


		public async void GetState()
		{

			var json = await WebServicesData.SyncTask("GET", "lockState");
			var size = json.Count;


				var result = json[0];

				state = result["parameterValue"];

		
			

		



			update();


		}



		public void update()
		{ 
			power_btn.IsToggled = state;

			power_btn.Toggled += power_btn_Toggled;

		Content = new StackLayout
		{
			Children =
				{
					header,
					power_btn,
					imagelock
				}
		};
		}


		void power_btn_Toggled(object sender, ToggledEventArgs e)
		{
			var lll = new Label
			{
				Text = string.Format("Is now {0}", e.Value)
			};
			if (e.Value)
			{

				WebServicesData.SyncTask("POST", "Lock", 20, 1);

				imagelock.Source = "lockk.png";

			}
			else
			{
				WebServicesData.SyncTask("POST", "Lock", 20, 0);

		
				imagelock.Source = "unlock.png";
			
			}



			DisplayAlert("Power", lll.Text, "OK");
		}

	}
}

