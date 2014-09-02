using System;
using System.Reflection;
using System.ComponentModel;

namespace BindableMapTest.Helpers
{

	public class WeakEventSubscription<TSource, TEventArgs>	: IDisposable
		where TSource : class
		where TEventArgs : EventArgs
	{
		private readonly WeakReference _targetReference;
		private readonly WeakReference _sourceReference;

		private readonly MethodInfo _eventHandlerMethodInfo;

		private readonly EventInfo _sourceEventInfo;

		private bool _subscribed;

		public WeakEventSubscription(
			TSource source,
			string sourceEventName,
			EventHandler<TEventArgs> targetEventHandler)
			: this(source, typeof (TSource).GetEvent(sourceEventName), targetEventHandler)
		{
		}

		public WeakEventSubscription(
			TSource source,
			EventInfo sourceEventInfo,
			EventHandler<TEventArgs> targetEventHandler)
		{
			if (source == null)
				throw new ArgumentNullException();

			if (sourceEventInfo == null)
				throw new ArgumentNullException("sourceEventInfo",
				                                "missing source event info in MvxWeakEventSubscription");

			_eventHandlerMethodInfo = targetEventHandler.Method;
			_targetReference = new WeakReference(targetEventHandler.Target);
			_sourceReference = new WeakReference(source);
			_sourceEventInfo = sourceEventInfo;

			AddEventHandler();
		}

		protected virtual Delegate CreateEventHandler()
		{
			return new EventHandler<TEventArgs>(OnSourceEvent);
		}

		//This is the method that will handle the event of source.
		protected void OnSourceEvent(object sender, TEventArgs e)
		{
			var target = _targetReference.Target;
			if (target != null)
			{
				_eventHandlerMethodInfo.Invoke(target, new[] {sender, e});
			}
			else
			{
				RemoveEventHandler();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				RemoveEventHandler();
			}
		}

		private void RemoveEventHandler()
		{
			if (!_subscribed)
				return;

			var source = (TSource) _sourceReference.Target;
			if (source != null)
			{
				_sourceEventInfo.GetRemoveMethod().Invoke(source, new object[] {CreateEventHandler()});
				_subscribed = false;
			}
		}

		private void AddEventHandler()
		{
			if (_subscribed)
				throw new Exception("Should not call _subscribed twice");

			var source = (TSource) _sourceReference.Target;
			if (source != null)
			{
				_sourceEventInfo.GetAddMethod().Invoke(source, new object[] {CreateEventHandler()});
				_subscribed = true;
			}
		}
	}
}
