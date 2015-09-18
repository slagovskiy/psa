namespace Photoland.Forms.Admin
{
	partial class frmClientTable
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientTable));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.gridData = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.btnShowAll = new System.Windows.Forms.Button();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.lblLoad = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.btnDelete);
			this.groupBox2.Controls.Add(this.btnAdd);
			this.groupBox2.Controls.Add(this.btnEdit);
			this.groupBox2.Controls.Add(this.btnClose);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox2.Location = new System.Drawing.Point(506, 458);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(474, 96);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Действия";
			// 
			// btnDelete
			// 
			this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnDelete.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDelete.Location = new System.Drawing.Point(142, 20);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(130, 30);
			this.btnDelete.TabIndex = 6;
			this.btnDelete.Text = "Удалить";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnAdd.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnAdd.Location = new System.Drawing.Point(6, 20);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(130, 30);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "Добавить";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnEdit.Location = new System.Drawing.Point(6, 56);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(130, 30);
			this.btnEdit.TabIndex = 4;
			this.btnEdit.Text = "Изменить";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnClose.Location = new System.Drawing.Point(338, 20);
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
			this.gridData.Location = new System.Drawing.Point(1, 38);
			this.gridData.Name = "gridData";
			this.gridData.Rows.Count = 2;
			this.gridData.Rows.DefaultSize = 17;
			this.gridData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.gridData.Size = new System.Drawing.Size(992, 414);
			this.gridData.StyleInfo = resources.GetString("gridData.StyleInfo");
			this.gridData.TabIndex = 6;
			this.gridData.DoubleClick += new System.EventHandler(this.gridData_DoubleClick);
			// 
			// btnShowAll
			// 
			this.btnShowAll.Location = new System.Drawing.Point(521, 10);
			this.btnShowAll.Name = "btnShowAll";
			this.btnShowAll.Size = new System.Drawing.Size(115, 23);
			this.btnShowAll.TabIndex = 13;
			this.btnShowAll.Text = "Показать все";
			this.btnShowAll.UseVisualStyleBackColor = true;
			this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(68, 12);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(447, 20);
			this.txtSearch.TabIndex = 12;
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 13);
			this.label1.TabIndex = 14;
			this.label1.Text = "Фильтр:";
			// 
			// tmr
			// 
			this.tmr.Interval = 2000;
			this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
			// 
			// lblLoad
			// 
			this.lblLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lblLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lblLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblLoad.Location = new System.Drawing.Point(316, 208);
			this.lblLoad.Name = "lblLoad";
			this.lblLoad.Size = new System.Drawing.Size(320, 56);
			this.lblLoad.TabIndex = 15;
			this.lblLoad.Text = "ЗАГРУЗКА...";
			this.lblLoad.Visible = false;
			// 
			// frmClientTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 566);
			this.Controls.Add(this.lblLoad);
			this.Controls.Add(this.btnShowAll);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridData);
			this.Controls.Add(this.groupBox2);
			this.Name = "frmClientTable";
			this.Text = "frmClientTable";
			this.Load += new System.EventHandler(this.frmClientTable_Load);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1FlexGrid.C1FlexGrid gridData;
		private System.Windows.Forms.Button btnShowAll;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.Label lblLoad;
	}
}