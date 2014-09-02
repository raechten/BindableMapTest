using System;
using Xamarin.Forms;
using BindableMapTest.Controls;
using BindableMapTest.Android.Renderers;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using System.Linq;
using BindableMapTest.Interfaces;

[assembly: ExportRenderer (typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace BindableMapTest.Android.Renderers
{
	public class ExtendedMapRenderer : MapRenderer
	{
		bool _isDrawnDone;

		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);
			var formsMap = (ExtendedMap)Element;
			var androidMapView = (MapView)Control;

			if (androidMapView != null && androidMapView.Map != null)
			{
				androidMapView.Map.InfoWindowClick += MapOnInfoWindowClick;
			}

			if (formsMap != null)
			{
				((ObservableCollection<Pin>)formsMap.Pins).CollectionChanged += OnCollectionChanged;
			}
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (e.PropertyName.Equals ("VisibleRegion") && !_isDrawnDone) {
				UpdatePins();

				_isDrawnDone = true;

			}
		}

		void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			UpdatePins();
		}

		private void UpdatePins()
		{
			var androidMapView = (MapView)Control;
			var formsMap = (ExtendedMap)Element;

			androidMapView.Map.Clear ();

			androidMapView.Map.MarkerClick += HandleMarkerClick;
			androidMapView.Map.MyLocationEnabled = formsMap.IsShowingUser;

			var items = formsMap.Items;

			foreach (var item in items) {
				var markerWithIcon = new MarkerOptions ();
				markerWithIcon.SetPosition (new LatLng (item.Location.Latitude, item.Location.Longitude));
				markerWithIcon.SetTitle (string.IsNullOrWhiteSpace(item.Name) ? "-" : item.Name);
				markerWithIcon.SetSnippet (item.Details);

				try
				{
					markerWithIcon.InvokeIcon(BitmapDescriptorFactory.FromResource(GetPinIcon()));
				}
				catch (Exception)
				{
					markerWithIcon.InvokeIcon(BitmapDescriptorFactory.DefaultMarker());
				}

				androidMapView.Map.AddMarker (markerWithIcon);
			}
		}

		private static int GetPinIcon()
		{
			return Resource.Drawable.dog;
		}

		private void HandleMarkerClick (object sender, GoogleMap.MarkerClickEventArgs e)
		{
			var marker = e.Marker;
			marker.ShowInfoWindow ();

			var map = this.Element as ExtendedMap;

			var formsPin = new ExtendedPin(marker.Title,marker.Snippet, marker.Position.Latitude, marker.Position.Longitude);

			map.SelectedPin = formsPin;
		}

		private void MapOnInfoWindowClick (object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			Marker clickedMarker = e.Marker;
			// Find the matchin item
			var formsMap = (ExtendedMap)Element;
			formsMap.ShowDetailCommand.Execute(formsMap.SelectedPin);
		}

		private bool IsItem(IMapModel item, Marker marker)
		{
			return item.Name == marker.Title && 
				   item.Details == marker.Snippet && 
				   item.Location.Latitude == marker.Position.Latitude && 
				   item.Location.Longitude == marker.Position.Longitude;
		}

		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);

			//NOTIFY CHANGE

			if (changed) {
				_isDrawnDone = false;
			}
		} 
	}
}


