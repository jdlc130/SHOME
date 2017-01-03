using System;

using Xamarin.Forms;

namespace SHOME
{
	public class SleepPage : ContentPage
	{
		public SleepPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Sleepeeee" }
				}
			};
		}
	}
}

