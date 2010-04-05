namespace PSA.Lib.Interface
{
	partial class frmDeletePayment
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
			this.label2 = new System.Windows.Forms.Label();
			this.txtDate = new System.Windows.Forms.DateTimePicker();
			this.lblInfo = new System.Windows.Forms.Label();
			this.pb = new System.Windows.Forms.ProgressBar();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(369, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Укажите дату, раньше которой необходимо очистить журнал платежей";
			// 
			// txtDate
			// 
			this.txtDate.Location = new System.Drawing.Point(15, 76);
			this.txtDate.Name = "txtDate";
			this.txtDate.Size = new System.Drawing.Size(156, 20);
			this.txtDate.TabIndex = 3;
			// 
			// lblInfo
			// 
			this.lblInfo.Location = new System.Drawing.Point(12, 99);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(448, 32);
			this.lblInfo.TabIndex = 4;
			// 
			// pb
			// 
			this.pb.Location = new System.Drawing.Point(15, 134);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(445, 20);
			this.pb.TabIndex = 5;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(340, 164);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(120, 30);
			this.btnClose.TabIndex = 6;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(214, 164);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(120, 30);
			this.btnDelete.TabIndex = 7;
			this.btnDelete.Text = "Удалить";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// frmDeletePayment
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(472, 206);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtDate);
			this.Controls.Add(this.pb);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDelete);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmDeletePayment";
			this.Load += new System.EventHandler(this.frmDeletePayment_Load);
			this.Controls.SetChildIndex(this.btnDelete, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.lblInfo, 0);
			this.Controls.SetChildIndex(this.pb, 0);
			this.Controls.SetChildIndex(this.txtDate, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker txtDate;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.ProgressBar pb;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnDelete;
	}
}
