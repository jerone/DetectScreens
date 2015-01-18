namespace DisplayWindowManager.Controls
{
	partial class DisplayWindowManager
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btn_refresh = new System.Windows.Forms.Button();
			this.cb_showBorders = new System.Windows.Forms.CheckBox();
			this.cb_showShell = new System.Windows.Forms.CheckBox();
			this.cb_startTimer = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// btn_refresh
			// 
			this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_refresh.Location = new System.Drawing.Point(124, 124);
			this.btn_refresh.Name = "btn_refresh";
			this.btn_refresh.Size = new System.Drawing.Size(23, 23);
			this.btn_refresh.TabIndex = 0;
			this.btn_refresh.Text = "R";
			this.btn_refresh.UseVisualStyleBackColor = true;
			// 
			// cb_showBorders
			// 
			this.cb_showBorders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cb_showBorders.AutoSize = true;
			this.cb_showBorders.Location = new System.Drawing.Point(46, 128);
			this.cb_showBorders.Name = "cb_showBorders";
			this.cb_showBorders.Size = new System.Drawing.Size(33, 17);
			this.cb_showBorders.TabIndex = 1;
			this.cb_showBorders.Text = "B";
			this.cb_showBorders.UseVisualStyleBackColor = true;
			// 
			// cb_showShell
			// 
			this.cb_showShell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cb_showShell.AutoSize = true;
			this.cb_showShell.Location = new System.Drawing.Point(7, 128);
			this.cb_showShell.Name = "cb_showShell";
			this.cb_showShell.Size = new System.Drawing.Size(33, 17);
			this.cb_showShell.TabIndex = 2;
			this.cb_showShell.Text = "S";
			this.cb_showShell.UseVisualStyleBackColor = true;
			// 
			// cb_startTimer
			// 
			this.cb_startTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cb_startTimer.AutoSize = true;
			this.cb_startTimer.Location = new System.Drawing.Point(85, 128);
			this.cb_startTimer.Name = "cb_startTimer";
			this.cb_startTimer.Size = new System.Drawing.Size(33, 17);
			this.cb_startTimer.TabIndex = 3;
			this.cb_startTimer.Text = "T";
			this.cb_startTimer.UseVisualStyleBackColor = true;
			// 
			// DisplayWindowManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cb_startTimer);
			this.Controls.Add(this.cb_showShell);
			this.Controls.Add(this.cb_showBorders);
			this.Controls.Add(this.btn_refresh);
			this.Name = "DisplayWindowManager";
			this.Load += new System.EventHandler(this.DisplayWindowManagerLoad);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.DisplayWindowManagerPaint);
			this.MouseLeave += new System.EventHandler(this.DisplayWindowManagerMouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DisplayWindowManagerMouseMove);
			this.Resize += new System.EventHandler(this.DisplayWindowManagerResize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_refresh;
		private System.Windows.Forms.CheckBox cb_showBorders;
		private System.Windows.Forms.CheckBox cb_showShell;
		private System.Windows.Forms.CheckBox cb_startTimer;
		private System.Windows.Forms.ToolTip toolTip1;

	}
}
