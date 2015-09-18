namespace Photoland.Forms.Interface
{
	partial class frmGetDateIntervalUserCheck
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.checkCurentUser = new System.Windows.Forms.CheckBox();
            this.txtDateEnd = new System.Windows.Forms.DateTimePicker();
            this.txtDateBegin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(144, 87);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(63, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkCurentUser
            // 
            this.checkCurentUser.AutoSize = true;
            this.checkCurentUser.Checked = true;
            this.checkCurentUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkCurentUser.Location = new System.Drawing.Point(12, 64);
            this.checkCurentUser.Name = "checkCurentUser";
            this.checkCurentUser.Size = new System.Drawing.Size(207, 17);
            this.checkCurentUser.TabIndex = 11;
            this.checkCurentUser.Text = "Только по текущему пользователю";
            this.checkCurentUser.UseVisualStyleBackColor = true;
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateEnd.Location = new System.Drawing.Point(106, 38);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(113, 20);
            this.txtDateEnd.TabIndex = 10;
            // 
            // txtDateBegin
            // 
            this.txtDateBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateBegin.Location = new System.Drawing.Point(106, 12);
            this.txtDateBegin.Name = "txtDateBegin";
            this.txtDateBegin.Size = new System.Drawing.Size(113, 20);
            this.txtDateBegin.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Конечная дата";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Начальная дата";
            // 
            // frmGetDateIntervalUserCheck
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(231, 121);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.checkCurentUser);
            this.Controls.Add(this.txtDateEnd);
            this.Controls.Add(this.txtDateBegin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGetDateIntervalUserCheck";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGetDateIntervalUserCheck";
            this.Load += new System.EventHandler(this.frmGetDateIntervalUserCheck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.DateTimePicker txtDateEnd;
		private System.Windows.Forms.DateTimePicker txtDateBegin;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox checkCurentUser;
	}
}