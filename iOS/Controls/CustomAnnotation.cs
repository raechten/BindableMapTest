using BindableMapTest.Interfaces;

namespace BindableMapTest.iOS.Controls
{

	public class CustomAnnotation : MapAnnotation<IMapModel> 
	{
		public override string Title { get { return string.IsNullOrWhiteSpace(Model.Name) ? "-" : Model.Name; } }
		public override string Subtitle { get { return Model.Details ?? "-"; } }

		public CustomAnnotation(IMapModel model) : base(model)
		{

		}
	}
	
}
