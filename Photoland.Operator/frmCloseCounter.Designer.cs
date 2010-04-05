namespace Photoland.Operator
{
	partial class frmCloseCounter
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
			this.lblInfo = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCounter = new System.Windows.Forms.NumericUpDown();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.txtCounter)).BeginInit();
			this.SuspendLayout();
			// 
			// lblInfo
			// 
			this.lblInfo.Location = new System.Drawing.Point(12, 60);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(280, 62);
			this.lblInfo.TabIndex = 2;
			this.lblInfo.Text = "Счетчик открыт: 00/00/0000 00:00:00\r\nНачальное значение счетчика: 0000000\r\nКоличе" +
				"ство отпечатков: 0000\r\nОжидаемое значение счетчика: 0000000";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 122);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(154, 32);
			this.label2.TabIndex = 3;
			this.label2.Text = "Значение счетчика для закрытия:";
			// 
			// txtCounter
			// 
			this.txtCounter.Location = new System.Drawing.Point(172, 125);
			this.txtCounter.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
			this.txtCounter.Name = "txtCounter";
			this.txtCounter.Size = new System.Drawing.Size(120, 20);
			this.txtCounter.TabIndex = 4;
			this.txtCounter.ValueChanged += new System.EventHandler(this.txtCounter_ValueChanged);
			this.txtCounter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCounter_KeyUp);
			// 
			// btnSave
			// 
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(136, 167);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(217, 167);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmCloseCounter
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(304, 202);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtCounter);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmCloseCounter";
			this.Text = "    - PSA v.9.0.21022.8";
			this.Title = "      ";
			this.Load += new System.EventHandler(this.frmCloseCounter_Load);
			this.Controls.SetChildIndex(this.txtCounter, 0);
			this.Controls.SetChildIndex(this.btnSave, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.lblInfo, 0);
			((System.ComponentModel.ISupportInitialize)(this.txtCounter)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.NumericUpDown txtCounter;
		public System.Windows.Forms.Label lblInfo;
	}
}
