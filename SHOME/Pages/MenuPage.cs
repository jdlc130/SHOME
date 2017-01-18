using System;

using Xamarin.Forms;

namespace SHOME
{
	public class MenuPage : TabbedPage
	{
		public MenuPage()
		{
			Title = "SHOMEd";


			Children.Add(new EstarPage(1)
			{
				Title = "Sleep",
				Icon = "image019.png"

			});

			Children.Add(new EstarPage(2)
			{
				Title = "Estar",
				Icon = "image017.png"

			});

			Children.Add(new EstarPage(3)
			{
				Title = "HOME",
				Icon = "image012.png"

			});

			Children.Add(new EstarPage(4)
		{
			Title = "Garden",
			Icon = "image015.png"

		});





		}
	}
};


