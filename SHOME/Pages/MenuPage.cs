using System;

using Xamarin.Forms;

namespace SHOME
{
	public class MenuPage : TabbedPage
	{
		public MenuPage()
		{
			Title = "SHOME";


			Children.Add(new SleepPage()
			{
				Title = "Sleep",
				Icon = "image019.png"

			});

			Children.Add(new EstarPage()
			{
				Title = "Estar",
				Icon = "image017.png"

			});

			Children.Add(new HomePage()
			{
				Title = "HOME",
				Icon = "image012.png"

			});

			Children.Add(new GardenPage()
		{
			Title = "Garden",
			Icon = "image015.png"

		});





		}
	}
};


