namespace PSA.Lib.Interface
{
	partial class frmInventoryScan
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
			this.txtScan = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lstScan = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnDeleteList = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.txtStatus = new System.Windows.Forms.Label();
			this.lblCount = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtScan
			// 
			this.txtScan.Enabled = false;
			this.txtScan.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtScan.Location = new System.Drawing.Point(12, 126);
			this.txtScan.Name = "txtScan";
			this.txtScan.Size = new System.Drawing.Size(351, 56);
			this.txtScan.TabIndex = 0;
			this.txtScan.Text = "9999999999999";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(12, 107);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Штих-код:";
			// 
			// lstScan
			// 
			this.lstScan.FormattingEnabled = true;
			this.lstScan.Location = new System.Drawing.Point(12, 201);
			this.lstScan.Name = "lstScan";
			this.lstScan.Size = new System.Drawing.Size(195, 251);
			this.lstScan.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 185);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Кода для добавления";
			// 
			// btnDeleteList
			// 
			this.btnDeleteList.Location = new System.Drawing.Point(213, 201);
			this.btnDeleteList.Name = "btnDeleteList";
			this.btnDeleteList.Size = new System.Drawing.Size(150, 30);
			this.btnDeleteList.TabIndex = 2;
			this.btnDeleteList.Text = "Удалить выбранный";
			this.btnDeleteList.UseVisualStyleBackColor = true;
			this.btnDeleteList.Click += new System.EventHandler(this.btnDeleteList_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(213, 237);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(150, 30);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "Очистить список";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(213, 351);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(150, 65);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Завершить сканирование и вставить номера в документ";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(213, 422);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(150, 30);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "Отмена";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// txtStatus
			// 
			this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtStatus.Location = new System.Drawing.Point(12, 60);
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.Size = new System.Drawing.Size(351, 47);
			this.txtStatus.TabIndex = 6;
			this.txtStatus.Text = "Выборка заказов.";
			// 
			// lblCount
			// 
			this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblCount.Location = new System.Drawing.Point(210, 270);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(153, 37);
			this.lblCount.TabIndex = 7;
			this.lblCount.Text = "Всего: 0шт";
			// 
			// frmInventoryScan
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(375, 465);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.txtStatus);
			this.Controls.Add(this.txtScan);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lstScan);
			this.Controls.Add(this.btnDeleteList);
			this.Controls.Add(this.btnOk);
			this.KeyPreview = true;
			this.Name = "frmInventoryScan";
			this.Load += new System.EventHandler(this.frmInventoryScan_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmInventoryScan_KeyPress);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnDeleteList, 0);
			this.Controls.SetChildIndex(this.lstScan, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.btnClear, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.txtScan, 0);
			this.Controls.SetChildIndex(this.txtStatus, 0);
			this.Controls.SetChildIndex(this.lblCount, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtScan;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lstScan;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnDeleteList;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label txtStatus;
		private System.Windows.Forms.Label lblCount;
	}
}
