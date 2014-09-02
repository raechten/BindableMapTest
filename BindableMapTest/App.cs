using System;
using Xamarin.Forms;
using BindableMapTest.Pages;

namespace BindableMapTest
{
	public class App
	{
		public static Page GetMainPage()
		{	
			return new MapPage();
		}
	}
}

