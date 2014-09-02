using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin;

namespace BindableMapTest.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			Forms.Init();
			FormsMaps.Init();

			window = new UIWindow(UIScreen.MainScreen.Bounds);
			
			window.RootViewController = App.GetMainPage().CreateViewController();
			window.MakeKeyAndVisible();
			
			return true;
		}
	}
}

