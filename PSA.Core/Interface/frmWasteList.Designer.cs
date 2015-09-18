namespace PSA.Lib.Interface
{
	partial class frmWasteList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWasteList));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.date2 = new System.Windows.Forms.DateTimePicker();
			this.btnApply = new System.Windows.Forms.Button();
			this.date1 = new System.Windows.Forms.DateTimePicker();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.data = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.date2);
			this.groupBox1.Controls.Add(this.btnApply);
			this.groupBox1.Controls.Add(this.date1);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new System.Drawing.Point(3, 474);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(420, 100);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Фильтр";
			// 
			// date2
			// 
			this.date2.CustomFormat = "yyyy-MM-dd";
			this.date2.Location = new System.Drawing.Point(54, 43);
			this.date2.Name = "date2";
			this.date2.Size = new System.Drawing.Size(141, 20);
			this.date2.TabIndex = 2;
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(284, 19);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(130, 30);
			this.btnApply.TabIndex = 9;
			this.btnApply.Text = "Применить";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// date1
			// 
			this.date1.CustomFormat = "yyyy-MM-dd";
			this.date1.Location = new System.Drawing.Point(54, 17);
			this.date1.Name = "date1";
			this.date1.Size = new System.Drawing.Size(141, 20);
			this.date1.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 22);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(33, 13);
			this.label6.TabIndex = 0;
			this.label6.Text = "Дата";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.btnDelete);
			this.groupBox2.Controls.Add(this.btnAdd);
			this.groupBox2.Controls.Add(this.btnEdit);
			this.groupBox2.Controls.Add(this.btnClose);
			this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox2.Location = new System.Drawing.Point(429, 474);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(555, 100);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Действия";
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDelete.Location = new System.Drawing.Point(142, 22);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(130, 30);
			this.btnDelete.TabIndex = 6;
			this.btnDelete.Text = "Удалить";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Visible = false;
			// 
			// btnAdd
			// 
			this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnAdd.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnAdd.Location = new System.Drawing.Point(6, 22);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(130, 30);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "Добавить";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Enabled = false;
			this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnEdit.Location = new System.Drawing.Point(6, 58);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(130, 30);
			this.btnEdit.TabIndex = 4;
			this.btnEdit.Text = "Изменить";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Visible = false;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnClose.Location = new System.Drawing.Point(419, 22);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(130, 30);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// data
			// 
			this.data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.data.BackColor = System.Drawing.Color.White;
			this.data.ColumnInfo = "2,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:945;}\t";
			this.data.Location = new System.Drawing.Point(3, 63);
			this.data.Name = "data";
			this.data.Rows.Count = 2;
			this.data.Rows.DefaultSize = 17;
			this.data.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.data.Size = new System.Drawing.Size(993, 405);
			this.data.StyleInfo = resources.GetString("data.StyleInfo");
			this.data.TabIndex = 14;
			// 
			// frmWasteList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(996, 586);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.data);
			this.MaximizeBox = true;
			this.MinimizeBox = true;
			this.Name = "frmWasteList";
			this.Load += new System.EventHandler(this.frmWasteList_Load);
			this.Controls.SetChildIndex(this.data, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DateTimePicker date2;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.DateTimePicker date1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1FlexGrid.C1FlexGrid data;
	}
}
