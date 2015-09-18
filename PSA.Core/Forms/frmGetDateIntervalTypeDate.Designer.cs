namespace Photoland.Forms.Interface
{
	partial class frmGetDateIntervalTypeDate
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
			this.txtDateEnd = new System.Windows.Forms.DateTimePicker();
			this.txtDateBegin = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtDateType = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(141, 91);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 20;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(60, 91);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 19;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtDateEnd
			// 
			this.txtDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.txtDateEnd.Location = new System.Drawing.Point(106, 65);
			this.txtDateEnd.Name = "txtDateEnd";
			this.txtDateEnd.Size = new System.Drawing.Size(113, 20);
			this.txtDateEnd.TabIndex = 17;
			// 
			// txtDateBegin
			// 
			this.txtDateBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.txtDateBegin.Location = new System.Drawing.Point(106, 39);
			this.txtDateBegin.Name = "txtDateBegin";
			this.txtDateBegin.Size = new System.Drawing.Size(113, 20);
			this.txtDateBegin.TabIndex = 16;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Конечная дата";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Начальная дата";
			// 
			// txtDateType
			// 
			this.txtDateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtDateType.FormattingEnabled = true;
			this.txtDateType.Location = new System.Drawing.Point(12, 12);
			this.txtDateType.Name = "txtDateType";
			this.txtDateType.Size = new System.Drawing.Size(204, 21);
			this.txtDateType.TabIndex = 21;
			// 
			// frmGetDateIntervalTypeDate
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(233, 126);
			this.Controls.Add(this.txtDateType);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtDateEnd);
			this.Controls.Add(this.txtDateBegin);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "frmGetDateIntervalTypeDate";
			this.Text = "frmGetDateIntervalTypeDate";
			this.Load += new System.EventHandler(this.frmGetDateIntervalTypeDate_Load);
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
		public System.Windows.Forms.ComboBox txtDateType;
	}
}