using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetectScreens.Controls
{
	public partial class Window : Control
	{
		public Window()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		private void Window_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
