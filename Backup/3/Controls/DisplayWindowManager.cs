using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using System.Diagnostics;

using DetectScreens.Controls;
using DetectScreens.Factory;
using DetectScreens.Properties;
using DetectScreens.Store;


namespace DetectScreens.Controls
{
	/// <summary>
	/// Control that combines Displays and Windows;
	/// </summary>
	public partial class DisplayWindowManager : UserControl
	{
		private Boolean showWindows = true;				// show Windows;
		private Boolean activateTimer = true;			// acivate timer;
		private Point offset = new Point(0, 0);		// display offset (x, y);

		private Displays displays;						// all displays connected to this computer;
		//private Windows windows;						// all program windows;
		private Int32 panelScale, panelScaleOld = 0;	// scale of display size versus screen size;
		private Rectangle workingArea;					// total display size;
		private Timer timer;							// update timer;
		private int wmShellHook;						// shell hook for getting windows messages;
		private Boolean onDisplayAlready = false;		// mouse is on displays;

		private IntPtr _handler;
		/// <summary>
		/// The Handle of this program.
		/// </summary>
		[Category("Custom Options"),
		 Description("Program Handle"),
		 Browsable(true)]
		public IntPtr Handler
		{
			set { _handler = value; }
			private get { return _handler; }
		}


		/// <summary>
		/// Control that combines Displays and Windows;
		/// </summary>
		public DisplayWindowManager(IntPtr handle)
		{
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			InitializeComponent();

			Handler = handle;

			wmShellHook = Win32.RegisterWindowMessage("SHELLHOOK");
			Win32.RegisterShellHookWindow(this.Handle);

			SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);

			timer = new Timer();
			timer.Interval = 100;
			timer.Tick += new EventHandler(timer_Tick);
			if (activateTimer)
			{
				timer.Start();
			}
		}

		private void DisplayWindowManager_Load(object sender, EventArgs e)
		{
			Init();
		}

		private void Init()
		{
			displays = new Displays();
			displays.Graphics = this.CreateGraphics();
			displays.Handler = Handler;
			displays.Offset = offset;
			displays.Scale = panelScale;
			Debug.WriteLine(displays.ToString());

			workingArea = Displays.WorkingArea();
			Debug.WriteLine(workingArea.ToString());

			panelScale = (workingArea.Width / this.Width) + 1;
			Debug.WriteLine(panelScale.ToString());
		}

		private void DisplayWindowManager_Paint(object sender, PaintEventArgs e)
		{
			displays.Scale = panelScale;
			displays.Graphics = e.Graphics;
			displays.Paint(true, false);

			if (panelScale != panelScaleOld)  // only repaint when the scale has changed;
			{
				panelScaleOld = panelScale;

				displays.Paint();
			}
		}

		private void DisplayWindowManager_Resize(object sender, EventArgs e)
		{
			if (this.Width > 0)  // weird error when width is 0;
			{
				panelScale = (workingArea.Width / this.Width) + 1;

				this.Invalidate();  // inmidiately repaint;
			}
		}

		private void DisplayWindowManager_MouseMove(object sender, MouseEventArgs e)
		{
			Boolean onDisplay = false;
			foreach (Display display in displays.All)
			{
				Rectangle rectBorder = new Rectangle(
					((Math.Abs(workingArea.Left) + display.Left) / panelScale) + offset.X,
					((Math.Abs(workingArea.Top) + display.Top) / panelScale) + offset.Y,
					display.Width / panelScale,
					display.Height / panelScale);
				if (rectBorder.Contains(e.Location))
				{
					onDisplay = true;
				}
			}
			if (onDisplay != onDisplayAlready)
			{
				displays.Opacity = onDisplay ? (byte)100 : (byte)255;
			}
			onDisplayAlready = onDisplay;
		}

		private void DisplayWindowManager_MouseLeave(object sender, EventArgs e)
		{
			onDisplayAlready = false;
			displays.Opacity = (byte)255;
		}

		private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
		{
			displays.Dispose();

			Init();

			this.Invalidate();  // inmidiately repaint;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (sender == timer)
			{
				//Debug.WriteLine(String.Format("Timer update"));
				if (showWindows)
				{
					displays.Paint(false, true);
				}
			}
		}

		/// <summary>
		/// The Windows System.Windows.Forms.Message.
		/// </summary>
		/// <param name="m">The Windows System.Windows.Forms.Message to process.</param>
		protected override void WndProc(ref Message m)
		{
			if (m.LParam != this.Handle && m.LParam != this._handler)
			{
				if (m.Msg == wmShellHook)
				{
					IntPtr wParam = m.WParam;
					IntPtr hwnd = m.LParam;
					Boolean doRepaint = false;

					switch (wParam.ToInt32())
					{
						default:
							Debug.WriteLine(String.Format("wParam {0}: {1}", wParam.ToInt32(), Win32.GetWindowText(m.LParam)));
							break;
						case Win32.HSHELL.WINDOWCREATED:
							Debug.WriteLine(String.Format("Create window: {0}", Win32.GetWindowText(m.LParam)));
							displays.WindowAdd(hwnd);
							doRepaint = true;
							break;
						case Win32.HSHELL.WINDOWDESTROYED:
							if (!String.IsNullOrEmpty(Win32.GetWindowText(m.LParam).ToString()))  // a lot of unnamed windows are destroyed every # seconds;
							{
								Debug.WriteLine(String.Format("Destroy window: {0}", Win32.GetWindowText(m.LParam)));
								displays.WindowRemove(hwnd);
								doRepaint = true;
							}
							break;
						case Win32.HSHELL.GETMINRECT:
							Debug.WriteLine(String.Format("Minimize window: {0}", Win32.GetWindowText(m.LParam)));
							doRepaint = true;
							break;
						case Win32.HSHELL.WINDOWACTIVATED:
						case Win32.HSHELL.RUDEAPPACTIVATED:
							Debug.WriteLine(String.Format("Activate window{1}: {0}", Win32.GetWindowText(m.LParam), wParam.ToInt32() == Win32.HSHELL.RUDEAPPACTIVATED ? " (rude)" : ""));
							displays.WindowMove(hwnd);
							doRepaint = true;
							break;
						case Win32.HSHELL.REDRAW:
						case Win32.HSHELL.FLASH:
							Debug.WriteLine(String.Format("{1} window: {0}", Win32.GetWindowText(m.LParam), wParam.ToInt32() == Win32.HSHELL.REDRAW ? "Redraw" : "Flash"));
							doRepaint = true;
							break;
					}

					if (doRepaint)  // only do windows repaint when something has changed;
					{
						if (showWindows)
						{
							displays.Graphics = this.CreateGraphics();
							displays.Paint();
						}
					}
				}
			}

			base.WndProc(ref m);
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the System.Windows.Forms.Form.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(Boolean disposing)
		{
			if (disposing)
			{
				SystemEvents.DisplaySettingsChanged -= new EventHandler(SystemEvents_DisplaySettingsChanged);

				Win32.DeregisterShellHookWindow(this.Handle);

				displays.Dispose();

				timer.Dispose();

				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
	}
}
