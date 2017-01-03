using System;

using Xamarin.Forms;

namespace SHOME
{
	public class IndexPage : ContentPage
	{
		public IndexPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

