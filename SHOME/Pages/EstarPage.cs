using System;

using Xamarin.Forms;

namespace SHOME
{
	public class EstarPage : ContentPage
	{
		public EstarPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = 
							"/n Hello Estar" }
				}
			};
		}
	}
}

