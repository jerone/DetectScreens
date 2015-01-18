using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DetectScreens.Factory
{
	/// <summary>
	/// Class for program windows;
	/// </summary>
	internal class Window : IDisposable
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
		public IntPtr Handle { get; set; }

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
			return this.Title;
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
			if (this.Thumb != IntPtr.Zero)
			{
				try
				{
					if (Win32.DwmUnregisterThumbnail(this.Thumb) != 0)
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
				this.Thumb = IntPtr.Zero;  // reset thumb;
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
			if (!this._disposed)
			{
				if (disposing)
				{
					Unregister();
				}

				this.Title = String.Empty;
				this.Handle = IntPtr.Zero;
				this.Bounds = Rectangle.Empty;
				this.Thumb = IntPtr.Zero;

				this._disposed = true;
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
	internal class Desktop : Window
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
	internal class Taskbar : Window
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
	internal class Windows : List<Window>, IDisposable
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
					this.Add(window);
				}

				return true;  // continue enumeration;
			}, 0);

			if (addDesktop)  // Add the desktop;
			{
				Desktop desktop = new Desktop();
				this.Add(desktop);
			}

			this.Reverse();  // topmost last;

			if (addTaskbar)
			{
				Taskbar taskbar = new Taskbar();
				this.Add(taskbar);
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
			String[] rtrn = new String[this.Count];
			Int32 i = 0;
			this.ForEach(delegate(Window w)
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

				this.Add(window);
			}
		}

		/// <summary>
		/// Move a window;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void Move(IntPtr hwnd)
		{
			Window window2Move = this.Find(window => window.Handle == hwnd);
			if (window2Move != null)
			{
				this.Remove(window2Move);

				// Get index of last window in the list. We don't want the taskbar;
				Int32 index = this.FindLastIndex(window => window.GetType() == typeof(Window));

				this.Insert(index + 1, window2Move);
			}
		}

		/// <summary>
		/// Remove a window;
		/// </summary>
		/// <param name="hwnd">Window handle</param>
		public void Remove(IntPtr hwnd)
		{
			Window window2Remove = this.Find(window => window.Handle == hwnd);
			if (window2Remove != null)
			{
				window2Remove.Unregister();
				this.Remove(window2Remove);
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
			if (!this._disposed)
			{
				if (disposing)
				{
					this.ForEach(window => window.Dispose());
				}

				this._disposed = true;
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