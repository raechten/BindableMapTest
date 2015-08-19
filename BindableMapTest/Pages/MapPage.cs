using BindableMapTest.Models;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using BindableMapTest.ViewModels;
using BindableMapTest.Extensions;
using BindableMapTest.Controls;

namespace BindableMapTest.Pages
{
	public class MapPage : ContentPage
	{
		private readonly ExtendedMap _map;

		public MapViewModel ViewModel
		{
			get { return BindingContext as MapViewModel; }
		}

		public MapPage()
		{
			BindingContext = new MapViewModel();;

			var mapSpan = MapSpan.FromCenterAndRadius(Location.DefaultPosition, Distance.FromKilometers(45));

			_map = new ExtendedMap(mapSpan);
			_map.ShowDetailCommand = ViewModel.ShowDetailCommand;

			ViewModel.Monkeys.CollectionChanged += UpdatePins;
			var loadButton = new Button { Text = "Load Monkeys" };
			var addButton = new Button { Text = "Add Monkey" };

			loadButton.SetBinding(Button.CommandProperty, "LoadMonkeysCommand");
			addButton.SetBinding(Button.CommandProperty, "AddMonkeyCommand");

			var stack = new StackLayout { Spacing = 0, Padding = new Thickness(0, Device.OnPlatform(20,0,0),0,0) };
			stack.Children.Add(loadButton);
			stack.Children.Add(addButton);
			stack.Children.Add(_map);
			Content = stack;
		}

		private void UpdatePins(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			_map.UpdatePins(ViewModel.Monkeys);
		}
	}
}

