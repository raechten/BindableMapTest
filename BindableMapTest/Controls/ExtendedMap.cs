using Xamarin.Forms.Maps;
using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using BindableMapTest.Interfaces;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using BindableMapTest.Extensions;
using Xamarin.Forms;

namespace BindableMapTest.Controls
{
	public class ExtendedMap : Map
	{
		private readonly ObservableCollection<IMapModel> _items = new ObservableCollection<IMapModel>();

		public ExtendedMap(MapSpan region) : base(region)
		{
			LastMoveToRegion = region;
		}

		public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<ExtendedMap, ExtendedPin> (x => x.SelectedPin, null);

		public ExtendedPin SelectedPin 
		{
			get{ return (ExtendedPin)base.GetValue (SelectedPinProperty); }
			set{ base.SetValue (SelectedPinProperty, value); }
		}

		public ICommand ShowDetailCommand { get; set; }

		private MapSpan _visibleRegion;

		public MapSpan LastMoveToRegion { get; private set; }
			
		public new MapSpan VisibleRegion
		{
			get { return _visibleRegion; }
			set
			{
				if (_visibleRegion == value)
				{
					return;
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				OnPropertyChanging("VisibleRegion");
				_visibleRegion = value;
				OnPropertyChanged("VisibleRegion");
			}
		}

		public ObservableCollection<IMapModel> Items
		{
			get { return _items; }
		}

		public void UpdatePins(IEnumerable<IMapModel> items)
		{
			Pins.Clear();
			Items.Clear();
			foreach (var item in items)
			{
				Items.Add(item);
				Pins.Add(item.AsPin());
			}
		}
	}
}

