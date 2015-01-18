using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;

using System.Diagnostics;

using DetectScreens.Controls;
using DetectScreens.Factory;
using DetectScreens.Properties;
using DetectScreens.Store;


namespace DetectScreens.Factory
{
	/// <summary>
	/// Class for program windows;
	/// </summary>
	internal class Window : IDisposable
	{
		private bool disposed = false;

		public string Title;
		public IntPtr Handle;
		public Rectangle Bounds;

		public IntPtr Thumb;

		public Window() { }

		public override string ToString()
		{
			return this.Title;
		}

		public Boolean Unregister()
		{
			Boolean succes = true;
			if (this.Thumb != IntPtr.Zero)
			{
				try
				{
					if (Win32.DwmUnregisterThumbnail(this.Thumb) != 0)
					{
						succes = false;
					}
				}
				catch
				{
					succes = false;
				}
			}
			if (succes)
			{
				this.Thumb = IntPtr.Zero;  // reset thumb;
			}
			return succes;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					Unregister();
				}

				this.Title = String.Empty;
				this.Handle = IntPtr.Zero;
				this.Bounds = Rectangle.Empty;
				this.Thumb = IntPtr.Zero;

				this.disposed = true;
			}
		}
		~Window()
		{
			Dispose(false);
		}
	}

	/// <summary>
	/// Class for the desktop;
	/// </summary>
	internal class Desktop : Window { }

	/// <summary>
	/// List of all windows;
	/// </summary>
	internal class Windows
	{
		private bool disposed = false;

		public List<Window> All;

		public Windows()
		{
			this.All = new List<Window>();

			Win32.EnumWindows(delegate(IntPtr hwnd, int lParam)
			{
				if ((Win32.GetWindowLongA(hwnd, Win32.GWL.STYLE) & Win32.WS.TARGETWINDOW) == Win32.WS.TARGETWINDOW  // normal window;
					|| hwnd == Win32.GetShellWindow())  // desktop;
				{
					StringBuilder sb = new StringBuilder(100);
					Win32.GetWindowText(hwnd, sb, sb.Capacity);

					Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
					placement.length = Marshal.SizeOf(placement);
					Win32.GetWindowPlacement(hwnd, ref placement);

					if (hwnd == Win32.GetShellWindow())
					{
						Desktop desktop = new Desktop();
						desktop.Handle = hwnd;
						desktop.Title = sb.ToString();
						desktop.Bounds = placement.rcNormalPosition;
						//this.All.Add(desktop);
					}
					else
					{
						Window window = new Window();
						window.Handle = hwnd;
						window.Title = sb.ToString();
						window.Bounds = placement.rcNormalPosition;
						this.All.Add(window);
					}
				}

				return true;  // continue enumeration;
			}, 0);

			this.All.Reverse();  // topmost last;
		}

		public override string ToString()
		{
			String[] rtrn = new String[this.All.Count];
			Int32 i = 0;
			this.All.ForEach(delegate(Window w)
			{
				rtrn[i] = String.Format(
@"Window {0}:
    Title: {1}
    Handle: {2}
    Bounds: {3}",
					++i, w.Title, w.Handle, w.Bounds);
			});
			return String.Join("\r\n", rtrn);
			//return base.ToString();
		}

		public void Add(IntPtr hwnd)
		{
			if ((Win32.GetWindowLongA(hwnd, Win32.GWL.STYLE) & Win32.WS.TARGETWINDOW) == Win32.WS.TARGETWINDOW)
			{
				StringBuilder sb = new StringBuilder(100);
				Win32.GetWindowText(hwnd, sb, sb.Capacity);

				Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
				placement.length = Marshal.SizeOf(placement);
				Win32.GetWindowPlacement(hwnd, ref placement);

				Window window = new Window();
				window.Handle = hwnd;
				window.Title = sb.ToString();
				window.Bounds = placement.rcNormalPosition;

				this.All.Add(window);
			}
		}
		public void Move(IntPtr hwnd)
		{
			Window window2move = this.All.Find(delegate(Window window)
			{
				return window.Handle == hwnd;
			});
			if (window2move != null)
			{
				this.All.Remove(window2move);
				this.All.Insert(this.All.Count, window2move);
			}
		}
		public void Remove(IntPtr hwnd)
		{
			Window window2remove = this.All.Find(delegate(Window window)
			{
				return window.Handle == hwnd;
			});
			if (window2remove != null)
			{
				window2remove.Unregister();
				this.All.Remove(window2remove);
			}

			/*
			this.All.ForEach(delegate(Window window)
			{
				if (window.Handle == hwnd)
				{
					window.Handle = IntPtr.Zero;
				}
			});
			*/
		}

		public Boolean Unregister()
		{
			Boolean succes = true;
			foreach (Window window in this.All)
			{
				if (!window.Unregister())
				{
					succes = false;
				}
			}
			return succes;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					foreach (Window window in this.All)
					{
						window.Dispose();
					}
				}

				this.All = null;

				this.disposed = true;
			}
		}
		~Windows()
		{
			Dispose(false);
		}
	}
}