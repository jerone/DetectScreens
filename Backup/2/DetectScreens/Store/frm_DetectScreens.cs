using System;
using System.Drawing;
using System.Windows.Forms;

using DetectScreens.Controls;
using DetectScreens.Factory;
using DetectScreens.Properties;
using DetectScreens.Store;


namespace DetectScreens.Store
{
	/// <summary>
	/// DetectScreens;
	/// </summary>
	public partial class frm_screens : Form
	{
		private Boolean showIdentifys = true;			// show Identify windows;

		private Displays displays;						// all displays connected to this computer;

		Win32.MARGINS glassMargins = new Win32.MARGINS(0, 0, 0, 0);  // glass margins in form;


		/// <summary>
		/// DetectScreens;
		/// </summary>
		public frm_screens()
		{
			InitializeComponent();

			//displayWindowManager1.Handler = this.Handle;

			//this.toolStripStatusLabel1.Text = String.Format("DetectScreens v{0}, made by Jerone", this.GetType().Assembly.GetName().Version.ToString());

			displays = new Displays();

			foreach (Display display in displays.All)
			{
				display.Preview.KeyUp += delegate(object sender, KeyEventArgs e)
				{
					if (e.KeyCode == Keys.Escape)
					{
						foreach (Display displayX in displays.All)
						{
							displayX.Preview.Hide();
						}
						showIdentifys = !showIdentifys;
					}
				};
			}
		}

		private void frm_screens_Load(object sender, EventArgs e)
		{
			if (!Utilities.IsSingleInstance(this.Text))
			{
				this.Close();
			}
		}

		private void frm_screens_Shown(object sender, EventArgs e)
		{
			this.MinimumSize = this.Size;
		}

		/// <summary>
		/// Paints the background of the control.
		/// </summary>
		/// <param name="e">A System.Windows.Forms.PaintEventArgs that contains the event data.</param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			glassMargins.Top = this.Height - 94;
			Win32.DwmExtendFrameIntoClientArea(this.Handle, glassMargins);
		}

		private void btn_preview_Click(object sender, EventArgs e)
		{
			foreach (Display display in displays.All)
			{
				if (showIdentifys)
				{
					Win32.SetWindowPos(display.Preview.Handle, IntPtr.Zero, 0, 0, 0, 0, Win32.SWP.NOSIZE | Win32.SWP.NOMOVE | Win32.SWP.NOACTIVATE);  // Allow preview rectangles to draw over the taskbar;
					display.Preview.Bounds = new Rectangle(display.Left, display.Top, display.Width, display.Height);
					display.Preview.Invalidate();
					display.Preview.Show();
				}
				else
				{
					display.Preview.Hide();
				}
			}
			showIdentifys = !showIdentifys;
		}
		private void btn_Resolution_Click(object sender, EventArgs e)
		{
			Win32.WinExec("control.exe desk.cpl,Settings,@Settings", Win32.SW.SHOW);
		}

		/// <summary>
		/// The Windows System.Windows.Forms.Message.
		/// </summary>
		/// <param name="m">The Windows System.Windows.Forms.Message to process.</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			/* this doesnt work, because of the panel thats on top.
			 * removing the panel fixes this issue; */
			if (m.Msg == Win32.WM.NCHITTEST && m.Result.ToInt32() == Win32.HT.CLIENT && this.IsOnGlass(m.LParam.ToInt32()))
			{
				m.Result = new IntPtr(Win32.HT.CAPTION);  // lie and say they clicked on the title bar
			}
		}
		private Boolean IsOnGlass(int lParam)
		{
			int x = (lParam << 16) >> 16;	// lo order word;
			int y = lParam >> 16;			// hi order word;
			Point p = this.PointToClient(new Point(x, y));
			return (p.Y < glassMargins.Top);
		}




		private void test_Click(object sender, EventArgs e)
		{

		}
	}
}