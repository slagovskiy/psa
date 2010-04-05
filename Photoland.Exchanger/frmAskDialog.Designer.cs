namespace Photoland.Exchanger
{
	partial class frmAskDialog
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
			this.lbl = new System.Windows.Forms.Label();
			this.btnYes = new System.Windows.Forms.Button();
			this.btnYesAll = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.btnNoAll = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lbl
			// 
			this.lbl.Location = new System.Drawing.Point(12, 9);
			this.lbl.Name = "lbl";
			this.lbl.Size = new System.Drawing.Size(398, 47);
			this.lbl.TabIndex = 0;
			this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnYes
			// 
			this.btnYes.Location = new System.Drawing.Point(12, 59);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(95, 33);
			this.btnYes.TabIndex = 1;
			this.btnYes.Text = "Да";
			this.btnYes.UseVisualStyleBackColor = true;
			this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
			// 
			// btnYesAll
			// 
			this.btnYesAll.Location = new System.Drawing.Point(113, 59);
			this.btnYesAll.Name = "btnYesAll";
			this.btnYesAll.Size = new System.Drawing.Size(95, 33);
			this.btnYesAll.TabIndex = 2;
			this.btnYesAll.Text = "Да для всех";
			this.btnYesAll.UseVisualStyleBackColor = true;
			this.btnYesAll.Click += new System.EventHandler(this.btnYesAll_Click);
			// 
			// btnNo
			// 
			this.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnNo.Location = new System.Drawing.Point(214, 59);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(95, 33);
			this.btnNo.TabIndex = 3;
			this.btnNo.Text = "Нет";
			this.btnNo.UseVisualStyleBackColor = true;
			this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
			// 
			// btnNoAll
			// 
			this.btnNoAll.Location = new System.Drawing.Point(315, 59);
			this.btnNoAll.Name = "btnNoAll";
			this.btnNoAll.Size = new System.Drawing.Size(95, 33);
			this.btnNoAll.TabIndex = 4;
			this.btnNoAll.Text = "Нет для всех";
			this.btnNoAll.UseVisualStyleBackColor = true;
			this.btnNoAll.Click += new System.EventHandler(this.btnNoAll_Click);
			// 
			// frmAskDialog
			// 
			this.AcceptButton = this.btnYes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnNo;
			this.ClientSize = new System.Drawing.Size(423, 104);
			this.Controls.Add(this.btnNoAll);
			this.Controls.Add(this.btnNo);
			this.Controls.Add(this.btnYesAll);
			this.Controls.Add(this.btnYes);
			this.Controls.Add(this.lbl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAskDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmAskDialog_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbl;
		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Button btnYesAll;
		private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.Button btnNoAll;
	}
}