using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DisplayWindowManager.Factory;
using Microsoft.Win32;

namespace DisplayWindowManager.Controls
{

	using HelperFramework.PInvoke;

	/// <summary>
	/// Control that combines Displays and Windows;
	/// </summary>
	public partial class DisplayWindowManager : UserControl
	{

		#region Fields (constants);

		private const Int32 TIMERINTERVAL = 1;			// Timer interval (ms);

		#endregion Fields (constants);


		#region Fields;

#if NOSHELL
		private Boolean _showshell = false;
#else
		private Boolean _showshell = true;
#endif
#if NOBORDERS
		private Boolean _showborders = false;
#else
		private Boolean _showborders = true;
#endif
#if NOTIMER
		private Boolean _starttimer = false;
#else
		private Boolean _starttimer = true;
#endif

		private Rectangle _workingArea;					// Total display size;
		private Timer _timer;							// Update timer;
		private int _wmShellHook;						// Shell hook for getting windows messages;
		private Boolean _onDisplayAlready;				// Mouse is on displays;
		private IntPtr _handle;

		#endregion Fields;


		#region Property;

		/// <summary>
		/// All displays connected to this computer;
		/// </summary>
		private Displays Displays { get; set; }

		private Point _border = new Point(5, 5);
		/// <summary>
		/// Border width & height;
		/// </summary>
		public Point Border
		{
			get { return _border; }
			set { _border = value; }
		}

		private Color _transparent = Color.Magenta;  // Color.FromArgb(0, 255, 0, 255);
		/// <summary>
		/// Color used for transparency;
		/// </summary>
		public Color Transparent
		{
			get { return _transparent; }
			set { _transparent = value; }
		}

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

#if DEBUG
			cb_showShell.Checked = _showshell;
			cb_showShell.CheckedChanged += delegate { _showshell = cb_showShell.Checked; };
			cb_showBorders.Checked = _showborders;
			cb_showBorders.CheckedChanged += delegate { _showborders = cb_showBorders.Checked; };
			cb_startTimer.Checked = _starttimer;
			cb_startTimer.CheckedChanged += delegate { _starttimer = cb_startTimer.Checked; };
			btn_refresh.Click += delegate
									{
										if (_timer != null)
										{
											_timer.Stop();
										}
										Displays.Dispose();
										Init();
										Invalidate();
										if (_starttimer && _timer != null)
										{
											_timer.Start();
										}
									};
#else
			cb_showShell.Visible = false;
			cb_showBorders.Visible = false;
			cb_startTimer.Visible = false;
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

			_wmShellHook = Hook.RegisterWindowMessage("SHELLHOOK");
			Hook.RegisterShellHookWindow(Handle);

			SystemEvents.DisplaySettingsChanged += SystemEventsDisplaySettingsChanged;

			Init();

			if (_starttimer)
			{
				_timer = new Timer { Interval = TIMERINTERVAL };
				_timer.Tick += TimerTick;
				_timer.Start();
			}
		}

		private void DisplayWindowManagerPaint(Object sender, PaintEventArgs e)
		{
			Displays.PaintArea = Size;
			Displays.Graphics = e.Graphics;
#if !NOTIMER
			Displays.Paint(_showborders, false);
#else
			Displays.Paint(_showborders, _showshell);
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
			Point p = e.Location;
			if (Displays.Any(__display => __display.ScaledBounds.Outside.Contains(p)))
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
				Displays.Paint(false, _showshell);
			}
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
				Border = Border,
				Transparent = Transparent
			};

			//Debug.WriteLine(Displays.ToString());
			foreach (var display in Displays)
			{
				Debug.WriteLine(display.ToString());
				Debug.WriteLine(display.Windows.ToString());
				Debug.WriteLine("------------------------");
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
							Debug.WriteLine(String.Format("wParam {0}: {1}", wParam.ToInt32(), Window.GetWindowText(hwnd)));
							break;
						case Win32.HSHELL.WINDOWCREATED:
							Debug.WriteLine(String.Format("Create window: {0}", Window.GetWindowText(hwnd)));
							Displays.Windows.AddAll(hwnd);
							doRepaint = true;
							break;
						case Win32.HSHELL.WINDOWDESTROYED:
							if (!String.IsNullOrEmpty(Window.GetWindowText(hwnd)))  // a lot of unnamed windows are destroyed every # seconds;
							{
								Debug.WriteLine(String.Format("Destroy window: {0}", Window.GetWindowText(hwnd)));
								Displays.Windows.RemoveAll(hwnd);
								doRepaint = true;
							}
							break;
						case Win32.HSHELL.GETMINRECT:
							Debug.WriteLine(String.Format("Minimize window: {0}", Window.GetWindowText(hwnd)));
							doRepaint = true;
							break;
						case Win32.HSHELL.WINDOWACTIVATED:
						case Win32.HSHELL.RUDEAPPACTIVATED:
							String programTitle = Window.GetWindowText(hwnd);
							if (hwnd == Window.FindWindow("Shell_TrayWnd", "") || hwnd == Window.FindWindow("ReBarWindow32", ""))  // Taskbar hasn't got a title;
							{
								programTitle = "Taskbar";
							}
							Debug.WriteLine(String.Format("Activate window{1}: {0}", programTitle, wParam.ToInt32() == Win32.HSHELL.RUDEAPPACTIVATED ? " (rude)" : ""));
							Displays.Windows.MoveAll(hwnd);
							doRepaint = true;
							break;
						case Win32.HSHELL.REDRAW:
						case Win32.HSHELL.FLASH:
							Debug.WriteLine(String.Format("{1} window: {0}", Window.GetWindowText(hwnd), wParam.ToInt32() == Win32.HSHELL.REDRAW ? "Redraw" : "Flash"));
							doRepaint = true;
							break;
					}

					if (doRepaint)  // only do windows repaint when something has changed;
					{
						Graphics g = CreateGraphics();
						Displays.Graphics = g;
						Displays.Paint(_showborders, _showshell);
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
			return Displays.Any(__display => __display.ScaledBounds.Outside.Contains(p));
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

				Hook.DeregisterShellHookWindow(Handle);

				if (Displays != null)  // in case of not visible control;
				{
					Displays.Dispose();
				}

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
