using System;
using System.Drawing;
using System.Windows.Forms;

namespace DetectScreens.Store
{
	using DetectScreens.Properties;

	using HelperFramework.Drawing;

	/// <summary>
	/// Overlay identifier for each monitor;
	/// </summary>
	public class FrmIdentify : Form
	{
		/// <summary>
		/// Color of the border;
		/// </summary>
		public Color BorderColor = Color.Lime;

		/// <summary>
		/// Number of the monitor to display;
		/// </summary>
		public String Nr { set; get; }

		/// <summary>
		/// Border size;
		/// </summary>
		private const Int32 BORDER = 20;

		/// <summary>
		/// Overlay identifier for each monitor;
		/// </summary>
		public FrmIdentify()
		{
			Nr = "";
			InitializeComponent();
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			SuspendLayout();
			// 
			// frm_Identify
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Fuchsia;
			ClientSize = new Size(284, 264);
			ControlBox = false;
			ForeColor = Color.Orange;
			FormBorderStyle = FormBorderStyle.None;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FrmIdentify";
			Opacity = 0.4;
			ShowInTaskbar = false;
			Text = Resources.FrmIdentify_InitializeComponent_frm_Identify;
			TopMost = true;
			TransparencyKey = Color.Fuchsia;
			ResumeLayout(false);
		}

		/// <summary>
		/// Paint event;
		/// </summary>
		/// <param name="e">A System.Windows.Forms.PaintEventArgs that contains the event data;</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;  // smoother borders;

			// colored overlay;
			g.FillRectangle(new SolidBrush(BorderColor), 0, 0, Width, Height);

			// transparent overlay within colored overlay;
			g.FillRectangle(new SolidBrush(TransparencyKey), BORDER, BORDER, Width - BORDER * 2, Height - BORDER * 2);

			// number;
			g.DrawText(
				Nr,
				new Point(Width / 2, Height / 2),
				new Font(new FontFamily("Arial"), 650f, FontStyle.Bold),
				Color.White,
				new Pen(Color.Black, 2.0f));
		}
	}
}
