using BindableMapTest.Interfaces;
using BindableMapTest.Models;

namespace BindableMapTest.Controls
{
	public class ExtendedPin : IMapModel
	{
		public ExtendedPin(string name, string details, double latitude, double longitude)
		{
			Name = name;
			Details = details;
			Location = new Location { Latitude = latitude, Longitude = longitude };
		}

		public string Name { get; set; }
		public string Details { get; set; }
		public string ImageUrl { get; set; }
		public ILocation Location { get; set; }
	}
}

