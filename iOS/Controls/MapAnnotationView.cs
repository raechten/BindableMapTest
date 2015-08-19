using System;
using MapKit;
using BindableMapTest.Interfaces;
using CoreLocation;
using Foundation;
using UIKit;
using BindableMapTest.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Linq;
using BindableMapTest.Helpers;
using System.Windows.Input;
using CoreGraphics;
using System.Threading.Tasks;
using System.Net.Http;

namespace BindableMapTest.iOS.Controls
{

	public class MapAnnotationView : MKAnnotationView
	{
		public MapAnnotationView (IMKAnnotation annotation, string reuseIdentifier) : base(annotation,reuseIdentifier)
		{
		}

		public override void SetDragState(MKAnnotationViewDragState newDragState, bool animated)
		{
			if (newDragState == MKAnnotationViewDragState.Starting) 
			{
				DragState = MKAnnotationViewDragState.Dragging;
			}
			else if (newDragState == MKAnnotationViewDragState.Ending || newDragState == MKAnnotationViewDragState.Canceling)
			{
				DragState = MKAnnotationViewDragState.None;
			}
		}
	} 
}
