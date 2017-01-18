using System;

using Xamarin.Forms;

namespace SHOME
{
	public class GardenPage : ContentPage
	{
		
		public GardenPage()
		{



	var grid = new Grid
	{
		RowDefinitions =
		{
		  new RowDefinition { Height = new GridLength(60, GridUnitType.Absolute) },
		  new RowDefinition { Height = GridLength.Auto },
		},
		ColumnDefinitions =
		{
		  new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) },
		  new ColumnDefinition { Width = new GridLength(.2, GridUnitType.Star) },
		  new ColumnDefinition { Width = new GridLength(.2, GridUnitType.Star) },
		  new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) },
		  new ColumnDefinition { Width = new GridLength(.1, GridUnitType.Star) }
		}
	};
			var ScrollH = new ScrollView
			{
				Content = grid,
				Orientation = ScrollOrientation.Horizontal,
			};


			//if right and bottom aren't specified, the column and row spans will be 1    
			grid.Children.Add(new Label { Text = "Hello" }, 0, 0);

			grid.Children.Add(new Label { Text = "Hello" }, 3, 0);

			grid.Children.Add(new Label { Text = "Hello" }, 4, 0);

			grid.Children.Add(new Label { Text = "Hello" }, 7, 0);

			//to start at column 1 and span 4 columns, right needs to be column + column span; 1 + 4 = 5.  Since this overload requires values for top and bottom, the row (top) is 0, and the row span is 1; 0 + 1 = 1, so bottom must be 1.
			grid.Children.Add(new Label { Text = "World" }, 1, 5, 0, 1);

			//column (left) = 0, right = column + column span; 0 + 5 = 6.  row (top) = 1, bottom = row + row span; 1 + 1 = 2
			grid.Children.Add(new Label { Text = "From AThThis is a very long label which I expect to scroll hThis is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect tois is a very long label which I expect to scroll hThis is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect to scroll horizontally because This is a very long label which I expect tolbuquerque, NM" }, 0, 7);


			this.Content = new ScrollView
			{
				Content = ScrollH,
				Orientation = ScrollOrientation.Vertical,
			};



		
		}
	}
}

