using System;
using System.Windows.Threading;

namespace HelperFramework
{
	public class TimeOut
	{
		public static void SetTimeout(Int32 milliseconds, Action func)
		{
			var timer = new DispatcherTimerContainingAction
			{
				Interval = new TimeSpan(0, 0, 0, 0, milliseconds),
				Action = func
			};
			timer.Tick += OnTimeout;
			timer.Start();
		}

		private static void OnTimeout(Object sender, EventArgs arg)
		{
			var t = sender as DispatcherTimerContainingAction;
			if (t == null) return;
			t.Stop();
			t.Action();
			t.Tick -= OnTimeout;
		}
	}

	public class DispatcherTimerContainingAction : DispatcherTimer
	{
		/// <summary>
		/// uncomment this to see when the DispatcherTimerContainingAction is collected
		/// if you remove  t.Tick -= _onTimeout; line from _onTimeout method
		/// you will see that the timer is never collected
		/// </summary>
		//~DispatcherTimerContainingAction()
		//{
		//    throw new Exception("DispatcherTimerContainingAction is disposed");
		//}

		public Action Action { get; set; }
	}
}
