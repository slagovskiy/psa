namespace PSA.Lib.Interface.Templates
{
	partial class frmTMdiParent
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTMdiParent));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.txtInfo = new System.Windows.Forms.ToolStripStatusLabel();
			this.txtTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.tmrTime = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtInfo,
            this.txtTime});
			this.statusStrip1.Location = new System.Drawing.Point(0, 424);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(632, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip";
			// 
			// txtInfo
			// 
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.Size = new System.Drawing.Size(0, 17);
			// 
			// txtTime
			// 
			this.txtTime.Name = "txtTime";
			this.txtTime.Size = new System.Drawing.Size(0, 17);
			// 
			// tmrTime
			// 
			this.tmrTime.Interval = 1000;
			this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
			// 
			// frmTMdiParent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(632, 446);
			this.Controls.Add(this.statusStrip1);
			this.IsMdiContainer = true;
			this.Name = "frmTMdiParent";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel txtInfo;
		private System.Windows.Forms.ToolStripStatusLabel txtTime;
		private System.Windows.Forms.Timer tmrTime;
	}
}