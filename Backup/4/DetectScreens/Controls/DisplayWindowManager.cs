using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DetectScreens.Factory;
using Microsoft.Win32;

namespace DetectScreens.Controls
{
	/// <summary>
	/// Control that combines Displays and Windows;
	/// </summary>
	public partial class DisplayWindowManager : UserControl
	{

		#region Fields (constants);

		private const Boolean SHOWWINDOWS = true;		// Show Windows;
#if NOTIMER
		private const Boolean ACTIVATETIMER = false;	// Don't acivate timer;
#else
		private const Boolean ACTIVATETIMER = true;		// Acivate timer;
#endif
		private const Int32 TIMERINTERVAL = 1;			// Timer interval (ms);

		#endregion Fields (constants);


		#region Fields;

		private Displays _displays;						// All displays connected to this computer;
		private Int32 _panelScale, _panelScaleOld;		// Scale of display size versus screen size;
		private Rectangle _workingArea;					// Total display size;
		private Timer _timer;							// Update timer;
		private int _wmShellHook;						// Shell hook for getting windows messages;
		private Boolean _onDisplayAlready = false;		// Mouse is on displays;
		private Point _offset = new Point(0, 0);		// Display offset (x, y);
		private IntPtr _handle;

		#endregion Fields;


		#region Constructor;

		/// <summary>
		/// Control that combines Displays and Windows;
		/// </summary>
		public DisplayWindowManager()
		{
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			InitializeComponent();
		}

		#endregion Constructor;


		#region Events;

		private void DisplayWindowManagerLoad(Object sender, EventArgs e)
		{
			if (this.ParentForm == null)
			{
				throw new Exception("The parent form does not excits!");
			}
			_handle = ParentForm.Handle;

			_wmShellHook = Win32.RegisterWindowMessage("SHELLHOOK");
			Win32.RegisterShellHookWindow(Handle);

			SystemEvents.DisplaySettingsChanged += SystemEventsDisplaySettingsChanged;

			_timer = new Timer { Interval = TIMERINTERVAL };
			_timer.Tick += TimerTick;
			if (ACTIVATETIMER)
			{
				_timer.Start();
			}

			Init();
		}

		private void DisplayWindowManagerPaint(Object sender, PaintEventArgs e)
		{
			_displays.Scale = _panelScale;
			_displays.Graphics = e.Graphics;
			_displays.Paint(true, false);

			if (_panelScale != _panelScaleOld)  // only repaint when the scale has changed;
			{
				_panelScaleOld = _panelScale;

				_displays.Paint();
			}
		}

		private void DisplayWindowManagerResize(Object sender, EventArgs e)
		{
			if (Width > 0)  // weird error when width is 0;
			{
				_panelScale = (_workingArea.Width / Width) + 1;

				Invalidate();  // inmidiately repaint;
			}
		}

		private void DisplayWindowManagerMouseMove(Object sender, MouseEventArgs e)
		{
			Boolean onDisplay = false;
			if (_displays.Select(display => new Rectangle(
									((Math.Abs(_workingArea.Left) + display.Left) / _panelScale) + _offset.X,
									((Math.Abs(_workingArea.Top) + display.Top) / _panelScale) + _offset.Y,
									display.Width / _panelScale,
									display.Height / _panelScale
								)).Any(rectBorder => rectBorder.Contains(e.Location)))
			{
				onDisplay = true;
			}
			if (onDisplay != _onDisplayAlready)
			{
				_displays.Opacity = onDisplay ? (Byte)100 : Byte.MaxValue;
				this.Cursor = Cursors.Default;
			}
			_onDisplayAlready = onDisplay;
		}

		private void DisplayWindowManagerMouseLeave(Object sender, EventArgs e)
		{
			_onDisplayAlready = false;
			_displays.Opacity = Byte.MaxValue;
		}

		private void SystemEventsDisplaySettingsChanged(Object sender, EventArgs e)
		{
			_displays.Dispose();

			Init();

			Invalidate();  // inmidiately repaint;
		}

		private void TimerTick(Object sender, EventArgs e)
		{
			if (sender == _timer)
			{
				//Debug.WriteLine(String.Format("Timer update"));
				if (SHOWWINDOWS)
				{
					_displays.Paint(false, true);
				}
			}
		}

		#endregion Events;


		#region Methods (private);

		private void Init()
		{
#if NOSHELL
			_displays = new Displays(false, false)
#else
			_displays = new Displays
#endif
			{
				Graphics = CreateGraphics(),
				Handler = _handle,
				Offset = _offset,
				Scale = _panelScale
			};
			Debug.WriteLine(_displays.ToString());
			foreach (var display in _displays)
			{
				Debug.WriteLine(display.Windows.ToString());
			}

			_workingArea = Displays.WorkingArea();
			Debug.WriteLine(String.Format("Working Area: {0}", _workingArea));

			_panelScale = (_workingArea.Width / Width) + 1;
			Debug.WriteLine(String.Format("Panel Scale: {0}", _panelScale));
		}

		#endregion Methods (private);


		#region Override;

		/// <summary>
		/// The Windows System.Windows.Forms.Message.
		/// </summary>
		/// <param name="m">The Windows System.Windows.Forms.Message to process.</param>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == (int)Win32.WM.NCHITTEST && !MouseIsOnGlass(m.LParam.ToInt32()))  // If mouse is not on a display, sent the mouse command through to the next control;
			{
				m.Result = (IntPtr)Win32.HT.TRANSPARENT;
				return;
			}

			if (m.Msg == _wmShellHook)
			{
				if (m.LParam != _handle)
				{
					IntPtr wParam = m.WParam;
					IntPtr hwnd = m.LParam;
					Boolean doRepaint = false;

					switch (wParam.ToInt32())
					{
						default:
							Debug.WriteLine(String.Format("wParam {0}: {1}", wParam.ToInt32(), Win32.GetWindowText(hwnd)));
							break;
						case Win32.HSHELL.WINDOWCREATED:
							Debug.WriteLine(String.Format("Create window: {0}", Win32.GetWindowText(hwnd)));
							_displays.WindowAdd(hwnd);
							doRepaint = true;
							break;
						case Win32.HSHELL.WINDOWDESTROYED:
							if (!String.IsNullOrEmpty(Win32.GetWindowText(hwnd)))  // a lot of unnamed windows are destroyed every # seconds;
							{
								Debug.WriteLine(String.Format("Destroy window: {0}", Win32.GetWindowText(hwnd)));
								_displays.WindowRemove(hwnd);
								doRepaint = true;
							}
							break;
						case Win32.HSHELL.GETMINRECT:
							Debug.WriteLine(String.Format("Minimize window: {0}", Win32.GetWindowText(hwnd)));
							doRepaint = true;
							break;
						case Win32.HSHELL.WINDOWACTIVATED:
						case Win32.HSHELL.RUDEAPPACTIVATED:
							String programTitle = Win32.GetWindowText(hwnd);
							if (hwnd == Win32.FindWindow("Shell_TrayWnd", "") || hwnd == Win32.FindWindow("ReBarWindow32", ""))  // Taskbar hasn't got a title;
							{
								programTitle = "Taskbar";
							}
							Debug.WriteLine(String.Format("Activate window{1}: {0}", programTitle, wParam.ToInt32() == Win32.HSHELL.RUDEAPPACTIVATED ? " (rude)" : ""));
							_displays.WindowMove(hwnd);
							doRepaint = true;
							break;
						case Win32.HSHELL.REDRAW:
						case Win32.HSHELL.FLASH:
							Debug.WriteLine(String.Format("{1} window: {0}", Win32.GetWindowText(hwnd), wParam.ToInt32() == Win32.HSHELL.REDRAW ? "Redraw" : "Flash"));
							doRepaint = true;
							break;
					}

					if (doRepaint)  // only do windows repaint when something has changed;
					{
						if (SHOWWINDOWS)
						{
							_displays.Graphics = CreateGraphics();
							_displays.Paint();
						}
					}
				}
			}

			base.WndProc(ref m);
		}

		private Boolean MouseIsOnGlass(Int32 lParam)
		{
			Int32 x = (lParam << 16) >> 16;	 // lo order word;
			Int32 y = lParam >> 16;			 // hi order word;
			Point p = PointToClient(new Point(x, y));
			return _displays.Select(display => new Rectangle(
												((Math.Abs(_workingArea.Left) + display.Left) / _panelScale) + _offset.X,
												((Math.Abs(_workingArea.Top) + display.Top) / _panelScale) + _offset.Y,
												display.Width / _panelScale,
												display.Height / _panelScale
												)).Any(rectBorder => rectBorder.Contains(p));
		}



		/// <summary>
		/// Disposes of the resources (other than memory) used by the System.Windows.Forms.Form.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(Boolean disposing)
		{
			if (disposing)
			{
				SystemEvents.DisplaySettingsChanged -= SystemEventsDisplaySettingsChanged;

				Win32.DeregisterShellHookWindow(Handle);

				_displays.Dispose();

				_timer.Dispose();

				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#endregion Override;

	}
}
