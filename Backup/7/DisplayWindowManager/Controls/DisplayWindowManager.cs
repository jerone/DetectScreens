using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DisplayWindowManager.Factory;
using HelperFramework;
using Microsoft.Win32;
using Math = System.Math;

namespace DisplayWindowManager.Controls
{
	/// <summary>
	/// Control that combines Displays and Windows;
	/// </summary>
	public partial class DisplayWindowManager : UserControl
	{

		#region Fields (constants);

		private const Int32 TIMERINTERVAL = 1;			// Timer interval (ms);

		#endregion Fields (constants);


		#region Fields;

		private Rectangle _workingArea;					// Total display size;
		private Timer _timer;							// Update timer;
		private int _wmShellHook;						// Shell hook for getting windows messages;
		private Boolean _onDisplayAlready = false;		// Mouse is on displays;
		private Point _offset = new Point(5, 15);		// Display offset (x, y);
		private IntPtr _handle;

		#endregion Fields;


		#region Property;

		/// <summary>
		/// All displays connected to this computer;
		/// </summary>
		private Displays Displays { get; set; }
		/*
		/// <summary>
		/// Scale of display size versus screen size;
		/// </summary>
		private new Double Scale
		{
			get
			{
				return (Double)(Width) / _workingArea.Width;
			}
		}
		*/
		#endregion Property;


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

#if !DEBUG
			btn_refresh.Visible = false;
#endif
		}

		#endregion Constructor;


		#region Events;

		private void DisplayWindowManagerLoad(Object sender, EventArgs e)
		{
			if (ParentForm == null) return;

			_handle = ParentForm.Handle;
			ParentForm.HandleCreated += delegate
			{
				if (ParentForm != null)
				{
					_handle = ParentForm.Handle;
					if (Displays != null)
					{
						Displays.Handler = _handle;
					}
				}
			};

			// If form is activated again, do a repaint;
			ParentForm.Activated += delegate { Invalidate(); };

			_wmShellHook = Win32.RegisterWindowMessage("SHELLHOOK");
			Win32.RegisterShellHookWindow(Handle);

			SystemEvents.DisplaySettingsChanged += SystemEventsDisplaySettingsChanged;

			Init();

#if !NOTIMER
			_timer = new Timer { Interval = TIMERINTERVAL };
			_timer.Tick += TimerTick;
			_timer.Start();
#endif
		}

		private void DisplayWindowManagerPaint(Object sender, PaintEventArgs e)
		{
			Displays.PaintArea = this.Size;
			Displays.Graphics = e.Graphics;
#if !NOTIMER
			Displays.Paint(true, false);
#else
			Displays.Paint(true, true);
#endif
		}

		private void DisplayWindowManagerResize(Object sender, EventArgs e)
		{
			if (Width > 0 && _workingArea.Width > 0)  // weird error when width is 0;
			{
				Invalidate();  // inmidiately repaint;
			}
		}

		private void DisplayWindowManagerMouseMove(Object sender, MouseEventArgs e)
		{
			Boolean onDisplay = false;
			Double scale = Displays.Scale;
			if (Displays.Select(display => new Rectangle(
									(Int32)((Math.Abs(_workingArea.Left) + display.Left) * scale) + _offset.X,
									(Int32)((Math.Abs(_workingArea.Top) + display.Top) * scale) + _offset.Y,
									(Int32)(display.Width * scale),
									(Int32)(display.Height * scale)
								)).Any(rectBorder => rectBorder.Contains(e.Location)))
			{
				onDisplay = true;
			}
			if (onDisplay != _onDisplayAlready)
			{
				Displays.Opacity = onDisplay ? (Byte)100 : Byte.MaxValue;
				Cursor = Cursors.Default;
			}
			_onDisplayAlready = onDisplay;
		}

		private void DisplayWindowManagerMouseLeave(Object sender, EventArgs e)
		{
			_onDisplayAlready = false;
			Displays.Opacity = Byte.MaxValue;
		}

		private void SystemEventsDisplaySettingsChanged(Object sender, EventArgs e)
		{
			Displays.Dispose();

			Init();

			Invalidate();  // inmidiately repaint;
		}

		private void TimerTick(Object sender, EventArgs e)
		{
			if (sender == _timer)
			{
				Displays.Paint(false, true);
			}
		}

		private void BtnRefreshClick(Object sender, EventArgs e)
		{
			Init();

			Invalidate();
		}

		#endregion Events;


		#region Methods (private);

		private void Init()
		{
#if NOSHELL
			Displays = new Displays(false, false)
#else
			Displays = new Displays
#endif
			{
				Graphics = CreateGraphics(),
				Handler = _handle,
				Border = _offset
				// Scale = Scale <-- can't define Scale here, as the control isn't completely rendered!
			};
			Debug.WriteLine(Displays.ToString());
			foreach (var display in Displays)
			{
				Debug.WriteLine(display.Windows.ToString());
			}

			_workingArea = Displays.WorkingArea();
			Debug.WriteLine(String.Format("Working Area: {0}", _workingArea));

			//Debug.WriteLine(String.Format("Panel Scale: {0}", Scale));
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
							Displays.Windows.AddAll(hwnd);
							doRepaint = true;
							break;
						case Win32.HSHELL.WINDOWDESTROYED:
							if (!String.IsNullOrEmpty(Win32.GetWindowText(hwnd)))  // a lot of unnamed windows are destroyed every # seconds;
							{
								Debug.WriteLine(String.Format("Destroy window: {0}", Win32.GetWindowText(hwnd)));
								Displays.Windows.RemoveAll(hwnd);
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
							Displays.Windows.MoveAll(hwnd);
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
						Graphics g = CreateGraphics();
						Displays.Graphics = g;
						Displays.Paint();
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
			Double scale = Displays.Scale;
			return Displays.Select(display => new Rectangle(
												(Int32)((Math.Abs(_workingArea.Left) + display.Left) * scale) + _offset.X,
												(Int32)((Math.Abs(_workingArea.Top) + display.Top) * scale) + _offset.Y,
												(Int32)(display.Width * scale),
												(Int32)(display.Height * scale)
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

				Displays.Dispose();

				if (_timer != null)
				{
					_timer.Dispose();
				}

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
