using System;
using MapKit;
using Foundation;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BindableMapTest.Helpers;
using System.Collections.Specialized;

namespace BindableMapTest.iOS.Controls
{

	public abstract class MapAnnotationManager
	{
		private readonly MKMapView _mapView;
		private IEnumerable _itemsSource;
		private IDisposable _subscription;
		private readonly Dictionary<object, MKAnnotation> _annotations = new Dictionary<object, MKAnnotation>();

		protected MapAnnotationManager(MKMapView mapView)
		{
			_mapView = mapView;
		}

		public virtual IEnumerable ItemsSource
		{
			get { return _itemsSource; }
			set { SetItemsSource(value); }
		}

		protected virtual void SetItemsSource(IEnumerable value)
		{
			if (_itemsSource == value)
				return;

			if (_subscription != null)
			{
				_subscription.Dispose();
				_subscription = null;
			}
			_itemsSource = value;

			#if DEBUG
			if (_itemsSource != null && !(_itemsSource is IList))
				Debug.WriteLine("Binding to IEnumerable rather than IList - this can be inefficient, especially for large lists");
			#endif

			ReloadAllAnnotations();

			var newObservable = _itemsSource as INotifyCollectionChanged;
			if (newObservable != null)
			{
				_subscription = new NotifyCollectionChangedEventSubscription(newObservable, OnItemsSourceCollectionChanged);
			}
		}

		protected virtual void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					AddAnnotations(e.NewItems);
					break;
				case NotifyCollectionChangedAction.Remove:
					RemoveAnnotations(e.OldItems);
					break;
				case NotifyCollectionChangedAction.Replace:
					RemoveAnnotations(e.OldItems);
					AddAnnotations(e.NewItems);
					break;
				case NotifyCollectionChangedAction.Move:
					// not interested in this
					break;
				case NotifyCollectionChangedAction.Reset:
					ReloadAllAnnotations();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		protected virtual void ReloadAllAnnotations()
		{
			_mapView.RemoveAnnotations(_annotations.Values.Select(x => (IMKAnnotation)x).ToArray());
			_annotations.Clear();

			if (_itemsSource == null)
				return;

			AddAnnotations(_itemsSource);
		}

		public abstract MKAnnotation CreateAnnotation(object item);

		protected virtual void RemoveAnnotations(IEnumerable oldItems)
		{
			foreach (var item in oldItems)
			{
				RemoveAnnotationFor(item);
			}
		}

		protected virtual void RemoveAnnotationFor(object item)
		{
			var annotation = _annotations[item];
			_mapView.RemoveAnnotation(annotation);
			_annotations.Remove(item);
		}

		protected virtual void AddAnnotations(IEnumerable newItems)
		{
			foreach (object item in newItems)
			{
				AddAnnotationFor(item);
			}
		}

		protected virtual void AddAnnotationFor(object item)
		{
			var annotation = CreateAnnotation(item);
			_annotations[item] = annotation;
			_mapView.AddAnnotation(annotation);
		}
	}
	
}
