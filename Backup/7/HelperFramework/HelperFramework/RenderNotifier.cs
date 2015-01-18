using System;
using System.Collections.Generic;
using System.Windows;

namespace HelperFramework
{
	public class RenderNotifier
	{
		Boolean renderComplete = false;

		FrameworkElement TargetElement;
		List<FrameworkElement> observedChildren = new List<FrameworkElement>();

		public delegate void OnRenderComplete(object sender);
		public event OnRenderComplete RenderComplete;

		public RenderNotifier(FrameworkElement targetElement)
		{
			this.TargetElement = targetElement;
			TargetElement.SizeChanged += new SizeChangedEventHandler(TargetElement_SizeChanged);
		}

		public void AddObservedChild(FrameworkElement child)
		{
			if (!observedChildren.Contains(child))
			{
				child.SizeChanged += Child_SizeChanged;
				observedChildren.Add(child);
			}
		}

		public void RemoveObservedChild(FrameworkElement child)
		{
			if (observedChildren.Remove(child))
			{
				child.SizeChanged -= Child_SizeChanged;
			}
		}

		public void ClearObservedChildren()
		{
			foreach (FrameworkElement elem in observedChildren)
			{
				elem.SizeChanged -= Child_SizeChanged;
			}

			observedChildren.Clear();
		}

		Int32 notificationsPending = 0;

		void Child_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (!renderComplete)
			{
				if (--notificationsPending == 0)
				{
					renderComplete = true;  // fire once;
					RenderComplete(TargetElement);
				}
			}
		}

		void TargetElement_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			notificationsPending = observedChildren.Count;
		}
	}
}
