namespace Photoland.CrashInfo
{
	partial class frmApplicationCrash
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
			this.txtInfo = new System.Windows.Forms.TextBox();
			this.checkRestart = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSendMail = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtInfo
			// 
			this.txtInfo.Location = new System.Drawing.Point(12, 92);
			this.txtInfo.Multiline = true;
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ReadOnly = true;
			this.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInfo.Size = new System.Drawing.Size(448, 169);
			this.txtInfo.TabIndex = 0;
			// 
			// checkRestart
			// 
			this.checkRestart.AutoSize = true;
			this.checkRestart.Checked = true;
			this.checkRestart.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkRestart.Location = new System.Drawing.Point(12, 267);
			this.checkRestart.Name = "checkRestart";
			this.checkRestart.Size = new System.Drawing.Size(162, 17);
			this.checkRestart.TabIndex = 1;
			this.checkRestart.Text = "Перезапустить программу";
			this.checkRestart.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(451, 80);
			this.label1.TabIndex = 2;
			this.label1.Text = "Произошла критическая ошибка в программе. Информация об ошибке приведена ниже. Вы" +
				" можете отправить ее на почту технической поддержки.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnSendMail
			// 
			this.btnSendMail.Location = new System.Drawing.Point(12, 291);
			this.btnSendMail.Name = "btnSendMail";
			this.btnSendMail.Size = new System.Drawing.Size(162, 23);
			this.btnSendMail.TabIndex = 3;
			this.btnSendMail.Text = "Отправить письмо";
			this.btnSendMail.UseVisualStyleBackColor = true;
			this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(180, 291);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(162, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmApplicationCrash
			// 
			this.AcceptButton = this.btnSendMail;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(472, 326);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSendMail);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkRestart);
			this.Controls.Add(this.txtInfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmApplicationCrash";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "В программе возникла критическая ошибка";
			this.Load += new System.EventHandler(this.frmApplicationCrash_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtInfo;
		private System.Windows.Forms.CheckBox checkRestart;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSendMail;
		private System.Windows.Forms.Button btnClose;
	}
}

