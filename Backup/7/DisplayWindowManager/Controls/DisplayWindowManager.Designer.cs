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
			this.btn_refresh = new System.Windows.Forms.Button();
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
			this.btn_refresh.Click += new System.EventHandler(this.BtnRefreshClick);
			// 
			// DisplayWindowManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn_refresh);
			this.Name = "DisplayWindowManager";
			this.Load += new System.EventHandler(this.DisplayWindowManagerLoad);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.DisplayWindowManagerPaint);
			this.MouseLeave += new System.EventHandler(this.DisplayWindowManagerMouseLeave);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DisplayWindowManagerMouseMove);
			this.Resize += new System.EventHandler(this.DisplayWindowManagerResize);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_refresh;

	}
}
