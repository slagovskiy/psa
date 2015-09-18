namespace Photoland.Forms.Interface
{
	partial class frmSelectClient
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectClient));
			this.btnNew = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.GridClient = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.btnShowAll = new System.Windows.Forms.Button();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.lblLoad = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.GridClient)).BeginInit();
			this.SuspendLayout();
			// 
			// btnNew
			// 
			this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnNew.BackColor = System.Drawing.SystemColors.Control;
			this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnNew.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnNew.Location = new System.Drawing.Point(12, 404);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(115, 30);
			this.btnNew.TabIndex = 5;
			this.btnNew.Text = "Новый";
			this.btnNew.UseVisualStyleBackColor = false;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnEdit.BackColor = System.Drawing.SystemColors.Control;
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnEdit.Location = new System.Drawing.Point(133, 404);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(115, 30);
			this.btnEdit.TabIndex = 6;
			this.btnEdit.Text = "Изменить";
			this.btnEdit.UseVisualStyleBackColor = false;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// GridClient
			// 
			this.GridClient.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
			this.GridClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.GridClient.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
			this.GridClient.BackColor = System.Drawing.Color.Silver;
			this.GridClient.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
			this.GridClient.ColumnInfo = "0,0,0,0,0,85,Columns:";
			this.GridClient.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
			this.GridClient.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
			this.GridClient.Location = new System.Drawing.Point(12, 43);
			this.GridClient.Name = "GridClient";
			this.GridClient.Rows.Count = 10;
			this.GridClient.Rows.DefaultSize = 17;
			this.GridClient.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.GridClient.Size = new System.Drawing.Size(608, 355);
			this.GridClient.StyleInfo = resources.GetString("GridClient.StyleInfo");
			this.GridClient.TabIndex = 1;
			this.GridClient.DoubleClick += new System.EventHandler(this.GridClient_DoubleClick);
			this.GridClient.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GridClient_KeyPress);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.SystemColors.Control;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnOK.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnOK.Location = new System.Drawing.Point(384, 404);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(115, 30);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "ОК";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCancel.Location = new System.Drawing.Point(505, 404);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(115, 30);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Поиск:";
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(60, 12);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(439, 20);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// btnShowAll
			// 
			this.btnShowAll.Location = new System.Drawing.Point(505, 10);
			this.btnShowAll.Name = "btnShowAll";
			this.btnShowAll.Size = new System.Drawing.Size(115, 23);
			this.btnShowAll.TabIndex = 3;
			this.btnShowAll.Text = "Показать все";
			this.btnShowAll.UseVisualStyleBackColor = true;
			this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
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
			this.lblLoad.Location = new System.Drawing.Point(156, 195);
			this.lblLoad.Name = "lblLoad";
			this.lblLoad.Size = new System.Drawing.Size(320, 56);
			this.lblLoad.TabIndex = 16;
			this.lblLoad.Text = "ЗАГРУЗКА...";
			this.lblLoad.Visible = false;
			// 
			// frmSelectClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(632, 446);
			this.Controls.Add(this.lblLoad);
			this.Controls.Add(this.btnShowAll);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.GridClient);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.MaximizeBox = false;
			this.Name = "frmSelectClient";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmSelectClient";
			((System.ComponentModel.ISupportInitialize)(this.GridClient)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnEdit;
		public C1.Win.C1FlexGrid.C1FlexGrid GridClient;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.Label lblLoad;
	}
}