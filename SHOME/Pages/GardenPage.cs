using System;

using Xamarin.Forms;

namespace SHOME
{
	public class GardenPage : ContentPage
	{
		public GardenPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello garden" }
				}
			};
		}
	}
}

