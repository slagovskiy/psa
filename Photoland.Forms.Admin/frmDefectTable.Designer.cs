namespace Photoland.Forms.Admin
{
    partial class frmDefectTable
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDefectTable));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.gridData = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.date1 = new System.Windows.Forms.DateTimePicker();
			this.date2 = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtU2 = new System.Windows.Forms.ComboBox();
			this.txtU1 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtType = new System.Windows.Forms.ComboBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.checkOK = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtServ = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
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
			this.groupBox2.Location = new System.Drawing.Point(704, 454);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(281, 100);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Действия";
			// 
			// btnDelete
			// 
			this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDelete.Location = new System.Drawing.Point(142, 57);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(130, 30);
			this.btnDelete.TabIndex = 6;
			this.btnDelete.Text = "Удалить";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Visible = false;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnAdd.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnAdd.Location = new System.Drawing.Point(6, 57);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(130, 30);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "Добавить";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Visible = false;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnEdit.Location = new System.Drawing.Point(6, 22);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(130, 30);
			this.btnEdit.TabIndex = 4;
			this.btnEdit.Text = "Изменить";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnClose.Location = new System.Drawing.Point(142, 22);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(130, 30);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// gridData
			// 
			this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gridData.BackColor = System.Drawing.Color.White;
			this.gridData.ColumnInfo = "2,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:945;}\t";
			this.gridData.Location = new System.Drawing.Point(4, 3);
			this.gridData.Name = "gridData";
			this.gridData.Rows.Count = 2;
			this.gridData.Rows.DefaultSize = 17;
			this.gridData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.gridData.Size = new System.Drawing.Size(988, 445);
			this.gridData.StyleInfo = resources.GetString("gridData.StyleInfo");
			this.gridData.TabIndex = 10;
			this.gridData.DoubleClick += new System.EventHandler(this.btnEdit_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Дата";
			// 
			// date1
			// 
			this.date1.CustomFormat = "yyyy-MM-dd";
			this.date1.Location = new System.Drawing.Point(54, 17);
			this.date1.Name = "date1";
			this.date1.Size = new System.Drawing.Size(141, 20);
			this.date1.TabIndex = 1;
			// 
			// date2
			// 
			this.date2.CustomFormat = "yyyy-MM-dd";
			this.date2.Location = new System.Drawing.Point(54, 43);
			this.date2.Name = "date2";
			this.date2.Size = new System.Drawing.Size(141, 20);
			this.date2.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(212, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Добавил";
			this.label2.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(217, 47);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "На кого";
			// 
			// txtU2
			// 
			this.txtU2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtU2.FormattingEnabled = true;
			this.txtU2.Location = new System.Drawing.Point(272, 17);
			this.txtU2.Name = "txtU2";
			this.txtU2.Size = new System.Drawing.Size(252, 21);
			this.txtU2.TabIndex = 5;
			this.txtU2.Visible = false;
			// 
			// txtU1
			// 
			this.txtU1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtU1.FormattingEnabled = true;
			this.txtU1.Location = new System.Drawing.Point(272, 44);
			this.txtU1.Name = "txtU1";
			this.txtU1.Size = new System.Drawing.Size(252, 21);
			this.txtU1.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(210, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Причина";
			// 
			// txtType
			// 
			this.txtType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtType.FormattingEnabled = true;
			this.txtType.Location = new System.Drawing.Point(272, 71);
			this.txtType.Name = "txtType";
			this.txtType.Size = new System.Drawing.Size(252, 21);
			this.txtType.TabIndex = 8;
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(550, 61);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(130, 30);
			this.btnApply.TabIndex = 9;
			this.btnApply.Text = "Применить";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// checkOK
			// 
			this.checkOK.AutoSize = true;
			this.checkOK.Location = new System.Drawing.Point(54, 73);
			this.checkOK.Name = "checkOK";
			this.checkOK.Size = new System.Drawing.Size(109, 17);
			this.checkOK.TabIndex = 12;
			this.checkOK.Text = "Подтвежденные";
			this.checkOK.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.txtServ);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.date2);
			this.groupBox1.Controls.Add(this.checkOK);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.btnApply);
			this.groupBox1.Controls.Add(this.date1);
			this.groupBox1.Controls.Add(this.txtType);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtU2);
			this.groupBox1.Controls.Add(this.txtU1);
			this.groupBox1.Location = new System.Drawing.Point(12, 454);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(686, 100);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Фильтр";
			// 
			// txtServ
			// 
			this.txtServ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtServ.FormattingEnabled = true;
			this.txtServ.Location = new System.Drawing.Point(272, 17);
			this.txtServ.Name = "txtServ";
			this.txtServ.Size = new System.Drawing.Size(252, 21);
			this.txtServ.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(226, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(43, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Услуга";
			// 
			// frmDefectTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(997, 566);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.gridData);
			this.Name = "frmDefectTable";
			this.Text = "frmDefectTable";
			this.Load += new System.EventHandler(this.frmDefectTable_Load);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClose;
		private C1.Win.C1FlexGrid.C1FlexGrid gridData;
		private System.Windows.Forms.DateTimePicker date2;
		private System.Windows.Forms.DateTimePicker date1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox txtU1;
		private System.Windows.Forms.ComboBox txtU2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox txtType;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.CheckBox checkOK;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox txtServ;
		private System.Windows.Forms.Label label5;
    }
}