namespace PSA.Lib.Interface
{
	partial class frmMovePayment
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMovePayment));
			this.label2 = new System.Windows.Forms.Label();
			this.data = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.dateFrom = new System.Windows.Forms.DateTimePicker();
			this.dateTo = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textSum = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(12, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(214, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Выборка из журнала платежей:";
			// 
			// data
			// 
			this.data.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
			this.data.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
			this.data.ColumnInfo = resources.GetString("data.ColumnInfo");
			this.data.ForeColor = System.Drawing.Color.Navy;
			this.data.Location = new System.Drawing.Point(12, 79);
			this.data.Name = "data";
			this.data.Rows.Count = 1;
			this.data.Rows.DefaultSize = 17;
			this.data.Size = new System.Drawing.Size(419, 160);
			this.data.StyleInfo = resources.GetString("data.StyleInfo");
			this.data.TabIndex = 56;
			this.data.DoubleClick += new System.EventHandler(this.data_DoubleClick);
			// 
			// dateFrom
			// 
			this.dateFrom.Location = new System.Drawing.Point(146, 245);
			this.dateFrom.Name = "dateFrom";
			this.dateFrom.Size = new System.Drawing.Size(200, 20);
			this.dateFrom.TabIndex = 57;
			// 
			// dateTo
			// 
			this.dateTo.Location = new System.Drawing.Point(146, 271);
			this.dateTo.Name = "dateTo";
			this.dateTo.Size = new System.Drawing.Size(200, 20);
			this.dateTo.TabIndex = 58;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(12, 251);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 16);
			this.label3.TabIndex = 59;
			this.label3.Text = "Перенести с даты:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(12, 275);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 16);
			this.label4.TabIndex = 60;
			this.label4.Text = "На дату:";
			// 
			// textSum
			// 
			this.textSum.Location = new System.Drawing.Point(146, 297);
			this.textSum.Name = "textSum";
			this.textSum.Size = new System.Drawing.Size(100, 20);
			this.textSum.TabIndex = 61;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(12, 298);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 16);
			this.label5.TabIndex = 62;
			this.label5.Text = "Сумму:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.Location = new System.Drawing.Point(252, 298);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(19, 16);
			this.label6.TabIndex = 63;
			this.label6.Text = "р.";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(185, 337);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(120, 30);
			this.btnSave.TabIndex = 64;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(311, 337);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(120, 30);
			this.btnCancel.TabIndex = 65;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmMovePayment
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(443, 381);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textSum);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.dateTo);
			this.Controls.Add(this.dateFrom);
			this.Controls.Add(this.data);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmMovePayment";
			this.Load += new System.EventHandler(this.frmMovePayment_Load);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.data, 0);
			this.Controls.SetChildIndex(this.dateFrom, 0);
			this.Controls.SetChildIndex(this.dateTo, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.label4, 0);
			this.Controls.SetChildIndex(this.textSum, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.label6, 0);
			this.Controls.SetChildIndex(this.btnSave, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private C1.Win.C1FlexGrid.C1FlexGrid data;
		private System.Windows.Forms.DateTimePicker dateFrom;
		private System.Windows.Forms.DateTimePicker dateTo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textSum;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
	}
}
