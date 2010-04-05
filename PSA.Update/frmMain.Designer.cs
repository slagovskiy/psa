namespace PSA.Update
{
    partial class frmMain
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
			this.p = new System.Windows.Forms.ProgressBar();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnInfo = new System.Windows.Forms.Button();
			this.txtInfo = new System.Windows.Forms.ListBox();
			this.btnSaveLog = new System.Windows.Forms.Button();
			this.dlg = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// p
			// 
			this.p.Location = new System.Drawing.Point(12, 12);
			this.p.Name = "p";
			this.p.Size = new System.Drawing.Size(600, 45);
			this.p.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(492, 151);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(120, 30);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "ОК";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnInfo
			// 
			this.btnInfo.Location = new System.Drawing.Point(305, 151);
			this.btnInfo.Name = "btnInfo";
			this.btnInfo.Size = new System.Drawing.Size(181, 30);
			this.btnInfo.TabIndex = 3;
			this.btnInfo.Text = "Информация к обновлению";
			this.btnInfo.UseVisualStyleBackColor = true;
			this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
			// 
			// txtInfo
			// 
			this.txtInfo.FormattingEnabled = true;
			this.txtInfo.Location = new System.Drawing.Point(12, 63);
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.Size = new System.Drawing.Size(600, 82);
			this.txtInfo.TabIndex = 4;
			// 
			// btnSaveLog
			// 
			this.btnSaveLog.Location = new System.Drawing.Point(161, 151);
			this.btnSaveLog.Name = "btnSaveLog";
			this.btnSaveLog.Size = new System.Drawing.Size(138, 30);
			this.btnSaveLog.TabIndex = 5;
			this.btnSaveLog.Text = "Сохранить журнал";
			this.btnSaveLog.UseVisualStyleBackColor = true;
			this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
			// 
			// dlg
			// 
			this.dlg.Filter = "Log files|*.log|All files|*.*";
			this.dlg.Title = "Сохранить журнал";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 195);
			this.Controls.Add(this.btnSaveLog);
			this.Controls.Add(this.txtInfo);
			this.Controls.Add(this.btnInfo);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.p);
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar p;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnInfo;
		private System.Windows.Forms.ListBox txtInfo;
		private System.Windows.Forms.Button btnSaveLog;
		private System.Windows.Forms.SaveFileDialog dlg;
    }
}

