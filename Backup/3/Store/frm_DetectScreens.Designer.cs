namespace DetectScreens.Store
{
	partial class frm_screens
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_screens));
			this.pnl_controls = new System.Windows.Forms.Panel();
			this.test = new System.Windows.Forms.Button();
			this.btn_preview = new System.Windows.Forms.Button();
			this.imgl_24x24x32 = new System.Windows.Forms.ImageList(this.components);
			this.btn_Resolution = new System.Windows.Forms.Button();
			this.imgl_16x16x32 = new System.Windows.Forms.ImageList(this.components);
			this.displayWindowManager1 = new DetectScreens.Controls.DisplayWindowManager(this.Handle);
			this.pnl_controls.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnl_controls
			// 
			this.pnl_controls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnl_controls.BackColor = System.Drawing.SystemColors.Control;
			this.pnl_controls.Controls.Add(this.test);
			this.pnl_controls.Controls.Add(this.btn_preview);
			this.pnl_controls.Controls.Add(this.btn_Resolution);
			this.pnl_controls.Location = new System.Drawing.Point(0, 306);
			this.pnl_controls.Margin = new System.Windows.Forms.Padding(0);
			this.pnl_controls.Name = "pnl_controls";
			this.pnl_controls.Size = new System.Drawing.Size(384, 56);
			this.pnl_controls.TabIndex = 10;
			// 
			// test
			// 
			this.test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.test.Location = new System.Drawing.Point(306, 30);
			this.test.Name = "test";
			this.test.Size = new System.Drawing.Size(75, 23);
			this.test.TabIndex = 11;
			this.test.Text = "test";
			this.test.UseVisualStyleBackColor = true;
			this.test.Click += new System.EventHandler(this.test_Click);
			// 
			// btn_preview
			// 
			this.btn_preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_preview.AutoSize = true;
			this.btn_preview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_preview.ImageIndex = 1;
			this.btn_preview.ImageList = this.imgl_24x24x32;
			this.btn_preview.Location = new System.Drawing.Point(12, 12);
			this.btn_preview.Name = "btn_preview";
			this.btn_preview.Size = new System.Drawing.Size(117, 32);
			this.btn_preview.TabIndex = 10;
			this.btn_preview.Text = "Identify Screens";
			this.btn_preview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_preview.UseVisualStyleBackColor = true;
			this.btn_preview.Click += new System.EventHandler(this.btn_preview_Click);
			// 
			// imgl_24x24x32
			// 
			this.imgl_24x24x32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl_24x24x32.ImageStream")));
			this.imgl_24x24x32.TransparentColor = System.Drawing.Color.Transparent;
			this.imgl_24x24x32.Images.SetKeyName(0, "openscreenresolution");
			this.imgl_24x24x32.Images.SetKeyName(1, "searchscreens");
			// 
			// btn_Resolution
			// 
			this.btn_Resolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_Resolution.AutoSize = true;
			this.btn_Resolution.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btn_Resolution.ImageIndex = 0;
			this.btn_Resolution.ImageList = this.imgl_24x24x32;
			this.btn_Resolution.Location = new System.Drawing.Point(135, 12);
			this.btn_Resolution.Name = "btn_Resolution";
			this.btn_Resolution.Size = new System.Drawing.Size(157, 32);
			this.btn_Resolution.TabIndex = 12;
			this.btn_Resolution.Text = "Open Screen Resolution";
			this.btn_Resolution.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btn_Resolution.UseVisualStyleBackColor = true;
			this.btn_Resolution.Click += new System.EventHandler(this.btn_Resolution_Click);
			// 
			// imgl_16x16x32
			// 
			this.imgl_16x16x32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl_16x16x32.ImageStream")));
			this.imgl_16x16x32.TransparentColor = System.Drawing.Color.Transparent;
			this.imgl_16x16x32.Images.SetKeyName(0, "openscreenresolution");
			// 
			// displayWindowManager1
			// 
			this.displayWindowManager1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.displayWindowManager1.Location = new System.Drawing.Point(0, 0);
			this.displayWindowManager1.Name = "displayWindowManager1";
			this.displayWindowManager1.Size = new System.Drawing.Size(384, 303);
			this.displayWindowManager1.TabIndex = 11;
			// 
			// frm_screens
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(384, 362);
			this.Controls.Add(this.displayWindowManager1);
			this.Controls.Add(this.pnl_controls);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frm_screens";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DetectScreens";
			this.Load += new System.EventHandler(this.frm_screens_Load);
			this.Shown += new System.EventHandler(this.frm_screens_Shown);
			this.pnl_controls.ResumeLayout(false);
			this.pnl_controls.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_preview;
		private System.Windows.Forms.Button test;
		private System.Windows.Forms.Button btn_Resolution;
		private System.Windows.Forms.Panel pnl_controls;
		private System.Windows.Forms.ImageList imgl_24x24x32;
		private System.Windows.Forms.ImageList imgl_16x16x32;
		private DetectScreens.Controls.DisplayWindowManager displayWindowManager1;
	}
}

