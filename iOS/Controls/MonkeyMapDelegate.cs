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

	public class MonkeyMapDelegate : MKMapViewDelegate
	{
		private const string PetAnnotationIdDog = "dog";
		private const string PetAnnotationIdCat = "cat";
		private const string PetAnnotationIdFamily = "family";

		private readonly bool _pinIsDraggable;
		private readonly bool _canShowCallout;
		private readonly ICommand _petDetailCommand;

		public MonkeyMapDelegate (bool pinIsDraggable = true, bool canShowCallout = false, ICommand petDetailCommand = null)
		{
			_pinIsDraggable = pinIsDraggable;
			_canShowCallout = canShowCallout;
			_petDetailCommand = petDetailCommand;
		}

		public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, NSObject annotation)
		{
			var pet = ((CustomAnnotation)annotation).Model;
			var petAnnotationType = GetPetAnnotationType (pet);

			var pin = (MapAnnotationView)mapView.DequeueReusableAnnotation(petAnnotationType);
			string pinImage = petAnnotationType + ".png";
			if (pin == null)
			{
				pin = new MapAnnotationView(annotation, petAnnotationType);
				pin.Image = UIImage.FromBundle(pinImage);
				pin.CenterOffset = new PointF (0, -15);
			}
			else
			{
				pin.Annotation = annotation;
			}

			pin.Draggable = _pinIsDraggable;
			pin.CanShowCallout = _canShowCallout;

			if (_canShowCallout) 
			{
				Task.Run( async () => pin.LeftCalloutAccessoryView = await GetImage(pet));
				pin.RightCalloutAccessoryView = GetDetailButton (pet);
			}
			return pin;
		}

		private UIButton GetDetailButton(IMapModel pet)
		{
			var detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);
			detailButton.TouchUpInside += (s,e) => {
				if (_petDetailCommand != null)
					_petDetailCommand.Execute(pet);
			};

			return detailButton;
		}

		private string GetPetAnnotationType (IMapModel pet) 
		{
			return PetAnnotationIdDog;
//			switch (pet.MissingType) 
//			{
//				case MissingType.Dog:
//					return PetAnnotationIdDog;
//				case MissingType.Cat:
//					return PetAnnotationIdCat;
//				case MissingType.Family:
//					return PetAnnotationIdFamily;
//				default:
//					return PetAnnotationIdDog;
//			}
		}

		private async Task<UIImageView> GetImage (IMapModel pet)
		{
			var imageView = new UIImageView(new RectangleF(5f,5f,75f,75f));
			imageView.Image = await LoadImage(pet.ImageUrl);
			return imageView;
		}

		private async Task<UIImage> LoadImage (string imageUrl)
		{
			var httpClient = new HttpClient();

			Task<byte[]> contentsTask = httpClient.GetByteArrayAsync (imageUrl);

			// await! control returns to the caller and the task continues to run on another thread
			var contents = await contentsTask;

			// load from bytes
			return UIImage.LoadFromData (NSData.FromArray (contents));
		}
	} 

}
