using MonoTouch.MapKit;
using BindableMapTest.Models;

namespace BindableMapTest.iOS.Controls
{
	public sealed class MonkeyManager : MapAnnotationManager
	{
		public MonkeyManager(MKMapView mapView) : base(mapView)
		{
		}

		public override MKAnnotation CreateAnnotation(object item)
		{
			return new CustomAnnotation(item as Monkey);
		}
	}
}

