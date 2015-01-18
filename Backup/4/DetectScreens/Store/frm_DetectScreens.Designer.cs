
namespace DetectScreens.Store
{
	partial class FrmDetectScreens
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetectScreens));
			this.imgl_32x32 = new System.Windows.Forms.ImageList(this.components);
			this.imgl_16x16 = new System.Windows.Forms.ImageList(this.components);
			this.btnResolution = new System.Windows.Forms.Button();
			this.lblVersionInfo = new System.Windows.Forms.Label();
			this.btnIdentify = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.displayWindowManager = new DetectScreens.Controls.DisplayWindowManager();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// imgl_32x32
			// 
			this.imgl_32x32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl_32x32.ImageStream")));
			this.imgl_32x32.TransparentColor = System.Drawing.Color.Transparent;
			this.imgl_32x32.Images.SetKeyName(0, "display_find.ico");
			this.imgl_32x32.Images.SetKeyName(1, "display_info.ico");
			this.imgl_32x32.Images.SetKeyName(2, "display_size.ico");
			// 
			// imgl_16x16
			// 
			this.imgl_16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl_16x16.ImageStream")));
			this.imgl_16x16.TransparentColor = System.Drawing.Color.Transparent;
			this.imgl_16x16.Images.SetKeyName(0, "display_find.ico");
			this.imgl_16x16.Images.SetKeyName(1, "display_size.ico");
			this.imgl_16x16.Images.SetKeyName(2, "display_info.ico");
			// 
			// btnResolution
			// 
			this.btnResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnResolution.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnResolution.ImageKey = "display_size.ico";
			this.btnResolution.ImageList = this.imgl_32x32;
			this.btnResolution.Location = new System.Drawing.Point(12, 14);
			this.btnResolution.Name = "btnResolution";
			this.btnResolution.Size = new System.Drawing.Size(150, 40);
			this.btnResolution.TabIndex = 0;
			this.btnResolution.Text = "Screen Resolution";
			this.btnResolution.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnResolution.UseVisualStyleBackColor = true;
			this.btnResolution.Click += new System.EventHandler(this.BtnResolutionClick);
			// 
			// lblVersionInfo
			// 
			this.lblVersionInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblVersionInfo.Location = new System.Drawing.Point(0, 66);
			this.lblVersionInfo.Name = "lblVersionInfo";
			this.lblVersionInfo.Size = new System.Drawing.Size(384, 16);
			this.lblVersionInfo.TabIndex = 1;
			this.lblVersionInfo.Text = "DetectScreens v{0}, made by Jerone";
			this.lblVersionInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnIdentify
			// 
			this.btnIdentify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnIdentify.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnIdentify.ImageKey = "display_find.ico";
			this.btnIdentify.ImageList = this.imgl_32x32;
			this.btnIdentify.Location = new System.Drawing.Point(272, 14);
			this.btnIdentify.Name = "btnIdentify";
			this.btnIdentify.Size = new System.Drawing.Size(100, 40);
			this.btnIdentify.TabIndex = 2;
			this.btnIdentify.Text = "Identify";
			this.btnIdentify.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnIdentify.UseVisualStyleBackColor = true;
			this.btnIdentify.Click += new System.EventHandler(this.BtnPreviewClick);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.btnIdentify);
			this.panel1.Controls.Add(this.lblVersionInfo);
			this.panel1.Controls.Add(this.btnResolution);
			this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 280);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 82);
			this.panel1.TabIndex = 0;
			// 
			// displayWindowManager
			// 
			this.displayWindowManager.BackColor = System.Drawing.Color.Red;
			this.displayWindowManager.Dock = System.Windows.Forms.DockStyle.Fill;
			this.displayWindowManager.Location = new System.Drawing.Point(0, 0);
			this.displayWindowManager.Name = "displayWindowManager";
			this.displayWindowManager.Size = new System.Drawing.Size(384, 280);
			this.displayWindowManager.TabIndex = 1;
			// 
			// FrmDetectScreens
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 362);
			this.Controls.Add(this.displayWindowManager);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmDetectScreens";
			this.Text = "DetectScreens";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList imgl_32x32;
		private System.Windows.Forms.ImageList imgl_16x16;
		private Controls.DisplayWindowManager displayWindowManager;
		private System.Windows.Forms.Button btnResolution;
		private System.Windows.Forms.Label lblVersionInfo;
		private System.Windows.Forms.Button btnIdentify;
		private System.Windows.Forms.Panel panel1;
	}
}