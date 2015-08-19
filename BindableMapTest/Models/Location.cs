using BindableMapTest.Interfaces;
using Xamarin.Forms.Maps;

namespace BindableMapTest.Models
{
	public class Location : ILocation
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public static Position DefaultPosition
		{
			get { return new Position(34.033897, -118.291869); }
		}
	}
}

