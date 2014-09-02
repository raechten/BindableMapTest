namespace BindableMapTest.Interfaces
{
	public interface IMapModel
	{
		string Name { get; set; }
		string Details { get; set; }
		ILocation Location { get; set; }
		string ImageUrl { get; set; }
	}
}

