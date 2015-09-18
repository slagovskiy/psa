namespace Photoland.Forms.Interface
{
	partial class frmApplyService
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApplyService));
			this.preview = new System.Windows.Forms.PictureBox();
			this.txtFiles = new System.Windows.Forms.CheckedListBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.preview)).BeginInit();
			this.SuspendLayout();
			// 
			// preview
			// 
			this.preview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.preview.Image = ((System.Drawing.Image)(resources.GetObject("preview.Image")));
			this.preview.Location = new System.Drawing.Point(270, 12);
			this.preview.Name = "preview";
			this.preview.Size = new System.Drawing.Size(350, 357);
			this.preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.preview.TabIndex = 0;
			this.preview.TabStop = false;
			// 
			// txtFiles
			// 
			this.txtFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.txtFiles.FormattingEnabled = true;
			this.txtFiles.Location = new System.Drawing.Point(12, 12);
			this.txtFiles.Name = "txtFiles";
			this.txtFiles.Size = new System.Drawing.Size(252, 424);
			this.txtFiles.TabIndex = 0;
			this.txtFiles.SelectedIndexChanged += new System.EventHandler(this.txtFiles_SelectedIndexChanged);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Location = new System.Drawing.Point(374, 401);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(120, 35);
			this.btnApply.TabIndex = 20;
			this.btnApply.Text = "Применить";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(500, 401);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(120, 35);
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "Отменить";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtComment
			// 
			this.txtComment.Location = new System.Drawing.Point(270, 375);
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(314, 20);
			this.txtComment.TabIndex = 21;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(590, 373);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(30, 23);
			this.button1.TabIndex = 22;
			this.button1.Text = "ok";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// frmApplyService
			// 
			this.AcceptButton = this.btnApply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(632, 446);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtFiles);
			this.Controls.Add(this.preview);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frmApplyService";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmApplyService";
			this.Load += new System.EventHandler(this.frmApplyService_Load);
			((System.ComponentModel.ISupportInitialize)(this.preview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox preview;
		private System.Windows.Forms.CheckedListBox txtFiles;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Button button1;
	}
}