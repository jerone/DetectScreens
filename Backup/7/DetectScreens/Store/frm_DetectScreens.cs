using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DisplayWindowManager.Factory;
using HelperFramework;

namespace DetectScreens.Store
{
	/// <summary>
	/// DetectScreens;
	/// </summary>
	public partial class FrmDetectScreens : Form
	{

		#region Fields;

#if NOGLASS
		private const Boolean DRAWGLASS = false;		// Don't draw glass effect;
#else
		private const Boolean DRAWGLASS = true;			// Draw glass effect;
#endif

		private readonly List<FrmIdentify> _previews = new List<FrmIdentify>();  // All previews;

		private readonly Win32.MARGINS _glassMargins = new Win32.MARGINS(0, 0, 0, 0);  // glass margins in form;

		private Boolean _showIdentifys = true;			// show Identify windows;

		#endregion Fields;


		#region Constructor;

		/// <summary>
		/// DetectScreens form;
		/// </summary>
		public FrmDetectScreens()
		{
			InitializeComponent();

			lblVersionInfo.Text = String.Format(lblVersionInfo.Text, GetType().Assembly.GetName().Version);
		}

		#endregion Constructor;


		#region Override;

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (!Utilities.IsSingleInstance(Text))
			{
				Close();
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Shown"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data. </param>
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			MinimumSize = Size;
		}

		/// <summary>
		/// The Windows System.Windows.Forms.Message.
		/// </summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process. </param>
		protected override void WndProc(ref Message m)
		{
			if (DRAWGLASS)
			{
				if (m.Msg == Win32.WM.SETCURSOR)
				{
					if ((m.LParam.ToInt32() & 0xffff) == Win32.HT.CAPTION)
					{
						Cursor.Current = Cursors.Hand;
						m.Result = (IntPtr)1;  // Processed;
						return;
					}
				}
			}

			base.WndProc(ref m);

			if (DRAWGLASS)
			{
				switch ((uint)m.Msg)
				{
					case Win32.WM.ACTIVATE:
					case Win32.WM.CHILDACTIVATE:
					case Win32.WM.SIZING:
					case Win32.WM.EXITSIZEMOVE:
					case Win32.WM.NCACTIVATE:
					case Win32.WM.NCCALCSIZE:
					case Win32.WM.WINDOWPOSCHANGING:
					case Win32.WM.WINDOWPOSCHANGED:
						{
							_glassMargins.Top = ClientSize.Height - panel1.Height;
							Win32.DwmExtendFrameIntoClientArea(Handle, _glassMargins);
							break;
						}
					case Win32.WM.NCHITTEST:
						{
							if (m.Result.ToInt32() == Win32.HT.CLIENT && MouseIsOnGlass(m.LParam.ToInt32()))
							{
								m.Result = new IntPtr(Win32.HT.CAPTION);  // Lie and say they clicked on the title bar;
							}
							break;
						}
				}
			}
		}

		#endregion Override;


		#region Events;

		/// <summary>
		/// Click event for show preview windows;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPreviewClick(Object sender, EventArgs e)
		{
			if (_showIdentifys)
			{
				_previews.Clear();
				Displays displays = new Displays(true);
				foreach (Display display in displays)
				{
					FrmIdentify preview = new FrmIdentify
											{
												Nr = display.Identifier.ToString()
											};
					Win32.SetWindowPos(preview.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32.SWP.NOSIZE | Win32.SWP.NOMOVE | Win32.SWP.NOACTIVATE);  // Allow preview rectangles to draw over the taskbar (must be declared before setting the bounds!);
					preview.Bounds = new Rectangle(display.Left, display.Top, display.Width, display.Height);
					preview.KeyUp += delegate(Object sender2, KeyEventArgs e2)
					{
						if (e2.KeyCode == Keys.Escape)
						{
							_previews.ForEach(preview2 => preview2.Close());
							_showIdentifys = !_showIdentifys;
						}
					};
					preview.Invalidate();
					preview.Show();
					_previews.Add(preview);
				}
				displays.Dispose();
			}
			else
			{
				_previews.ForEach(preview => preview.Close());
			}
			_showIdentifys = !_showIdentifys;
		}

		/// <summary>
		/// Click event for showing Windows Resolution window;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void BtnResolutionClick(Object sender, EventArgs e)
		{
			Win32.WinExec("control.exe desk.cpl,Settings,@Settings", Win32.SW.SHOW);
		}

		#endregion Events;


		#region Method (private);

		/// <summary>
		/// Detect if mouse cursor is above the glass part;
		/// </summary>
		/// <param name="lParam"></param>
		/// <returns></returns>
		private Boolean MouseIsOnGlass(Int32 lParam)
		{
			//TODO: add all margin sides;
			Int32 x = (lParam << 16) >> 16;	 // lo order word;
			Int32 y = lParam >> 16;			 // hi order word;
			Point p = PointToClient(new Point(x, y));
			return (p.Y < _glassMargins.Top);
		}

		#endregion Method (private);

	}
}