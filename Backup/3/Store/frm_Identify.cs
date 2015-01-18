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
	/// Overlay identifier for each monitor;
	/// </summary>
	public partial class frm_Identify : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Color of the border;
		/// </summary>
		public Color borderColor = Color.Lime;

		/// <summary>
		/// Number of the monitor to display;
		/// </summary>
		public String Nr { set; get; }

		/// <summary>
		/// Border size;
		/// </summary>
		private const Int32 border = 20;

		/// <summary>
		/// Overlay identifier for each monitor;
		/// </summary>
		public frm_Identify()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// frm_Identify
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Fuchsia;
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.ControlBox = false;
			this.ForeColor = System.Drawing.Color.Orange;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frm_Identify";
			this.Opacity = 0.4;
			this.ShowInTaskbar = false;
			this.Text = "frm_Identify";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.ResumeLayout(false);

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
			g.FillRectangle(new SolidBrush(this.borderColor), 0, 0, this.Width, this.Height);

			// transparent overlay within colored overlay;
			g.FillRectangle(new SolidBrush(this.TransparencyKey), border, border, this.Width - border * 2, this.Height - border * 2);

			// number;
			g.DrawText(
				Nr,
				new Point(this.Width / 2, this.Height / 2),
				new Font(new FontFamily("Arial"), 650f, FontStyle.Bold),
				Color.White,
				new Pen(Color.Black, 2.0f));
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
