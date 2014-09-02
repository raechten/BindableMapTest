using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin;
using Xamarin.Forms;

namespace BindableMapTest.Android
{
	[Activity(Label = "BindableMapTest.Android.Android", MainLauncher = true)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			Forms.Init(this, bundle);
			FormsMaps.Init(this, bundle);

			SetPage(App.GetMainPage());
		}
	}
}

