using System;
using MonoTouch.MapKit;
using BindableMapTest.Interfaces;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BindableMapTest.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Linq;
using BindableMapTest.Helpers;
using System.Windows.Input;
using System.Drawing;
using System.Threading.Tasks;
using System.Net.Http;

namespace BindableMapTest.iOS.Controls
{

	public class PetsMapDelegate : MonkeyMapDelegate
	{
		public PetsMapDelegate () : this(null)
		{
		}

		public PetsMapDelegate (ICommand petDetailCommand) : base(false, true, petDetailCommand)
		{
		}
	}	

}
