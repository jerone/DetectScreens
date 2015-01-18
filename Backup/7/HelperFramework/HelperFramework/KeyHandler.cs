using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace HelperFramework
{
	public class KeyHandler
	{
		private readonly Dictionary<Key, Boolean> _isPressed = new Dictionary<Key, Boolean>();

		public KeyHandler(FrameworkElement target)
		{
			ClearKeyPresses();
			target.KeyDown += TargetKeyDown;
			target.KeyUp += TargetKeyUp;
			target.LostFocus += TargetLostFocus;
		}

		public void ClearKeyPresses()
		{
			_isPressed.Clear();
		}
		public void ClearKeyPresses(Key key)
		{
			_isPressed.Remove(key);
		}

		public Boolean IsKeyPressed(Key k)
		{
			return _isPressed.ContainsKey(k);
		}

		private void TargetKeyDown(Object sender, KeyEventArgs e)
		{
			if (!_isPressed.ContainsKey(e.Key))
			{
				_isPressed.Add(e.Key, true);
			}
		}

		private void TargetKeyUp(Object sender, KeyEventArgs e)
		{
			if (_isPressed.ContainsKey(e.Key))
			{
				_isPressed.Remove(e.Key);
			}
		}

		private void TargetLostFocus(Object sender, RoutedEventArgs e)
		{
			ClearKeyPresses();
		}
	}
}
