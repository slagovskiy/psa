namespace PSA.Lib.Interface
{
	partial class frmSelectLamdaFile
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
			this.files = new System.Windows.Forms.ListBox();
			this.pic = new System.Windows.Forms.PictureBox();
			this.checkAutoPreview = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
			this.SuspendLayout();
			// 
			// files
			// 
			this.files.FormattingEnabled = true;
			this.files.Location = new System.Drawing.Point(12, 63);
			this.files.Name = "files";
			this.files.Size = new System.Drawing.Size(202, 355);
			this.files.TabIndex = 2;
			this.files.SelectedIndexChanged += new System.EventHandler(this.files_SelectedIndexChanged);
			// 
			// pic
			// 
			this.pic.Location = new System.Drawing.Point(220, 63);
			this.pic.Name = "pic";
			this.pic.Size = new System.Drawing.Size(350, 350);
			this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pic.TabIndex = 3;
			this.pic.TabStop = false;
			// 
			// checkAutoPreview
			// 
			this.checkAutoPreview.AutoSize = true;
			this.checkAutoPreview.Checked = true;
			this.checkAutoPreview.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkAutoPreview.Location = new System.Drawing.Point(12, 424);
			this.checkAutoPreview.Name = "checkAutoPreview";
			this.checkAutoPreview.Size = new System.Drawing.Size(101, 17);
			this.checkAutoPreview.TabIndex = 4;
			this.checkAutoPreview.Text = "Предпросмотр";
			this.checkAutoPreview.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(470, 419);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 30);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(364, 419);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(100, 30);
			this.btnOpen.TabIndex = 6;
			this.btnOpen.Text = "Выбрать";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// frmSelectLamdaFile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(583, 460);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.files);
			this.Controls.Add(this.pic);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.checkAutoPreview);
			this.Name = "frmSelectLamdaFile";
			this.Load += new System.EventHandler(this.frmSelectLamdaFile_Load);
			this.Controls.SetChildIndex(this.checkAutoPreview, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.pic, 0);
			this.Controls.SetChildIndex(this.files, 0);
			this.Controls.SetChildIndex(this.btnOpen, 0);
			((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox files;
		private System.Windows.Forms.PictureBox pic;
		private System.Windows.Forms.CheckBox checkAutoPreview;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOpen;
	}
}
