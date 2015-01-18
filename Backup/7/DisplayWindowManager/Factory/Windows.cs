using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using HelperFramework;

namespace DisplayWindowManager.Factory
{
	/// <summary>
	/// Class for program windows;
	/// </summary>
	public class Window : IDisposable, IWin32Window
	{

		#region Fields;

		private Boolean _disposed = false;

		#endregion Fields;


		#region Properties;

		/// <summary>
		/// Window bounds;
		/// </summary>
		public Rectangle Bounds;  // Rectangle is a struct, so I can't make it a propery;

		/// <summary>
		/// Window handle;
		/// </summary>
		public IntPtr Handle { get; private set; }

		/// <summary>
		/// Indicates if a window is maximized;
		/// </summary>
		public Boolean Maximized { get; set; }

		/// <summary>
		/// Used by Dwm Thumbnail API;
		/// </summary>
		public IntPtr Thumb { get; set; }

		/// <summary>
		/// Window caption;
		/// </summary>
		public String Title { get; set; }

		public Boolean ForceRefresh { get; set; }

		#endregion Properties;


		#region Constructor;

		/// <summary>
		/// Make a program window;
		/// </summary>
		/// <param name="handle">Window handle;</param>
		public Window(IntPtr handle)
		{
			Handle = handle;

			StringBuilder sb = new StringBuilder(100);
			Win32.GetWindowText(handle, sb, sb.Capacity);
			Title = sb.ToString();

			Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
			placement.length = Marshal.SizeOf(placement);
			Win32.GetWindowPlacement(handle, ref placement);
			Bounds = placement.rcNormalPosition;
			Maximized = placement.showCmd == Win32.SW.SHOWMAXIMIZED;
		}

		#endregion Constructor;


		#region Override;

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override String ToString()
		{
			return Title;
		}

		#endregion Override;


		#region Method;

		/// <summary>
		/// Removes the Dwm Thumbnail;
		/// </summary>
		/// <returns>Success</returns>
		public Boolean Unregister()
		{
			Boolean success = true;
			if (Thumb != IntPtr.Zero)
			{
				try
				{
					if (Win32.DwmUnregisterThumbnail(Thumb) != 0)
					{
						success = false;
					}
				}
				catch
				{
					success = false;
				}
			}
			if (success)
			{
				Thumb = IntPtr.Zero;  // reset thumb;
			}
			return success;
		}

		#endregion Method;


		#region Interface;

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(Boolean disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Unregister();
				}

				Title = String.Empty;
				Handle = IntPtr.Zero;
				Bounds = Rectangle.Empty;
				Thumb = IntPtr.Zero;

				_disposed = true;
			}
		}

		/// <summary>
		/// Allows an <see cref="T:System.Object"/> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object"/> is reclaimed by garbage collection.
		/// </summary>
		~Window()
		{
			Dispose(false);
		}

		#endregion Interface;

	}

	/// <summary>
	/// Class for the desktop;
	/// </summary>
	public class Desktop : Window
	{
		/// <summary>
		/// Create a desktop;
		/// </summary>
		public Desktop()
			: base(Win32.GetShellWindow())
		{
			Maximized = true;
		}
	}

	/// <summary>
	/// Class for the taskbar;
	/// </summary>
	public class Taskbar : Window
	{
		/// <summary>
		/// Create a taskbar;
		/// </summary>
		public Taskbar()
			: base(Win32.FindWindow("Shell_TrayWnd", ""))
		{
			Title = "Taskbar";  // Taskbar hasn't got a title;
			Maximized = true;
		}
	}

	/// <summary>
	/// List of all windows;
	/// </summary>
	public class Windows : List<Window>, IDisposable
	{

		#region Fields;

		private Boolean _disposed = false;

		#endregion Fields;


		#region Constructor;

		/// <summary>
		/// A list of all Windows;
		/// </summary>
		public Windows()
			: this(true, true) { }
		/// <summary>
		/// A list of all Windows;
		/// </summary>
		/// <param name="addDesktop">Include the desktop too</param>
		/// <param name="addTaskbar">Include the taskbar too</param>
		public Windows(Boolean addDesktop, Boolean addTaskbar)
		{
			Win32.EnumWindows(delegate(IntPtr hwnd, int lParam)
				{
					if ((Win32.GetWindowLongA(hwnd, Win32.GWL.STYLE) & Win32.WS.TARGETWINDOW) == Win32.WS.TARGETWINDOW)  // 'normal' window;
					{
						Window window = new Window(hwnd);
						Add(window);
					}

					#region test to get DisplayFusion taskbar;
					//else if ((Win32.GetWindowLongA(hwnd, Win32.GWL.STYLE) & (Win32.WS.POPUP & Win32.WS.VISIBLE & Win32.WS.CLIPSIBLINGS & Win32.WS.CLIPCHILDREN)) == (Win32.WS.POPUP & Win32.WS.VISIBLE & Win32.WS.CLIPSIBLINGS & Win32.WS.CLIPCHILDREN))
					//{

					//    if ((Win32.GetWindowLongA(hwnd, Win32.GWL.EXSTYLE) & (Win32.WS.EX.LEFT & Win32.WS.EX.LTRREADING & Win32.WS.EX.RIGHTSCROLLBAR & Win32.WS.EX.TOOLWINDOW)) == (Win32.WS.EX.LEFT & Win32.WS.EX.LTRREADING & Win32.WS.EX.RIGHTSCROLLBAR & Win32.WS.EX.TOOLWINDOW))
					//    {
					// GetWindowLong(WindowHandle,GWL_EXstyle) | WS_EX_TOOLWINDOW) & ~WS_EX_APPWINDOW
					//else if (hwnd.Equals(Win32.FindWindow("Shell_TrayWnd", "")))
					//{
					//ulong x = Win32.GetWindowLongA(hwnd, Win32.GWL.EXSTYLE);
					//if ((x & Win32.WS.EX.LEFT) == Win32.WS.EX.LEFT)
					//{
					//    if ((x & Win32.WS.EX.LTRREADING) == Win32.WS.EX.LTRREADING)
					//    {
					//        if ((x & Win32.WS.EX.RIGHTSCROLLBAR) == Win32.WS.EX.RIGHTSCROLLBAR)
					//        {
					//            if ((x & Win32.WS.EX.TOPMOST) == Win32.WS.EX.TOPMOST)
					//            {
					//                if ((x & Win32.WS.EX.TOOLWINDOW) == Win32.WS.EX.TOOLWINDOW)
					//        //                {
					//        Debug.WriteLine(hwnd);
					//        //                }
					//        //            }
					//        //        }
					//        //    }
					//    }
					//}
					//if (( | Win32.WS.EX.RIGHTSCROLLBAR | Win32.WS.EX.TOOLWINDOW | Win32.WS.EX.RIGHTSCROLLBAR) == Win32.GetWindowLongA(hwnd, Win32.GWL.EXSTYLE))
					//{
					//    Debug.WriteLine(new Window(hwnd));
					//}
					//Debug.WriteLine(hwnd.ToInt64());
					//Debug.WriteLine(hwnd.ToInt32());
					//Debug.WriteLine(new IntPtr(0010060).ToInt32());
					//Debug.WriteLine("---");
					#endregion test to get DisplayFusion taskbar;

					return true;  // continue enumeration;
				}, 0);

			if (addDesktop)  // Add the desktop;
			{
				Desktop desktop = new Desktop();
				Add(desktop);
			}

			Reverse();  // topmost last;

			if (addTaskbar)
			{
				Taskbar taskbar = new Taskbar();
				Add(taskbar);
			}
		}

		#endregion Constructor;


		#region Override;

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override String ToString()
		{
			String[] rtrn = new String[Count];
			Int32 i = 0;
			ForEach(delegate(Window w)
			{
				rtrn[i] = String.Format(
@"Window {0}:
    Title: {1}
    Handle: {2}
    Bounds: {3}
	Maximized: {4}",
					++i, w.Title, w.Handle, w.Bounds, w.Maximized);
			});
			return String.Join("\r\n", rtrn);
		}

		#endregion Override;


		#region Methods;

		/// <summary>
		/// Add a window;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void Add(IntPtr hwnd)
		{
			if ((Win32.GetWindowLongA(hwnd, Win32.GWL.STYLE) & Win32.WS.TARGETWINDOW) == Win32.WS.TARGETWINDOW)  // Only add 'normal' windows;
			{
				Window window = new Window(hwnd);
				Add(window);
			}
		}

		/// <summary>
		/// Move a window;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void Move(IntPtr hwnd)
		{
			Window window2Move = Find(window => window.Handle == hwnd);
			if (window2Move != null)
			{
				Remove(window2Move);

				// Get index of last window in the list. We don't want the taskbar;
				Int32 index = FindLastIndex(window => window.GetType() == typeof(Window)) + 1;

				Insert(index, window2Move);

				// Set current window and all next ones (e.g. Taskbar) to force refresh;
				this.GetRange(index, this.Count - index).ForEach(window => window.ForceRefresh = true);
			}
		}

		/// <summary>
		/// Remove a window;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void Remove(IntPtr hwnd)
		{
			Window window2Remove = Find(window => window.Handle == hwnd);
			if (window2Remove != null)
			{
				window2Remove.Unregister();
				Remove(window2Remove);
			}
		}

		/// <summary>
		/// Remove all Dwm Thumbnails;
		/// </summary>
		/// <returns></returns>
		public Boolean Unregister()
		{
			return this.All(window => window.Unregister());
		}

		#endregion Methods;


		#region Interface;

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(Boolean disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					ForEach(window => window.Dispose());
				}

				_disposed = true;
			}
		}

		/// <summary>
		/// Allows an <see cref="T:System.Object"/> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object"/> is reclaimed by garbage collection.
		/// </summary>
		~Windows()
		{
			Dispose(false);
		}

		#endregion Interface;

	}
}