using System;
using MonoTouch.MapKit;
using BindableMapTest.Interfaces;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BindableMapTest.Models;

namespace BindableMapTest.iOS.Controls
{
	public class MapAnnotation<TModel> : MKAnnotation, IMapAnnotation where TModel : IMapModel	
	{
		private readonly TModel _model;

		public MapAnnotation(TModel model)
		{
			_model = model;
			Location = _model.Location;
		}

		private CLLocationCoordinate2D _coordinate;
		public override CLLocationCoordinate2D Coordinate
		{
			get { return _coordinate; }
			set { SetCoordinate(value); }
		}

		[Export("_original_setCoordinate:")]
		public void SetCoordinate(CLLocationCoordinate2D coord)
		{
			_coordinate = coord;
			var handler = LocationChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public event EventHandler LocationChanged;

		public ILocation Location
		{
			get { return new Location { Latitude = Coordinate.Latitude, Longitude = Coordinate.Longitude }; }
			set
			{
				if (value.Equals(Location))
					return;

				UIView.Animate(1.0, () =>
				{
					WillChangeValue("coordinate");
					_coordinate = new CLLocationCoordinate2D(value.Latitude, value.Longitude);
					DidChangeValue("coordinate");
				});
			}
		}

		public TModel Model
		{
			get { return _model; }
		}
	}
}
