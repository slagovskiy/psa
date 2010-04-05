namespace PSA.Lib.Interface
{
	partial class frmOrderDiscardList
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderDiscardList));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnOpenOrder = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.data = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.btnOpenOrder);
			this.groupBox1.Controls.Add(this.btnClose);
			this.groupBox1.Controls.Add(this.btnUpdate);
			this.groupBox1.Location = new System.Drawing.Point(392, 454);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(388, 100);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Действия";
			// 
			// btnOpenOrder
			// 
			this.btnOpenOrder.Location = new System.Drawing.Point(6, 55);
			this.btnOpenOrder.Name = "btnOpenOrder";
			this.btnOpenOrder.Size = new System.Drawing.Size(160, 30);
			this.btnOpenOrder.TabIndex = 3;
			this.btnOpenOrder.Text = "Открыть заказ";
			this.btnOpenOrder.UseVisualStyleBackColor = true;
			this.btnOpenOrder.Click += new System.EventHandler(this.btnOpenOrder_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(267, 19);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(115, 30);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(6, 19);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(160, 30);
			this.btnUpdate.TabIndex = 1;
			this.btnUpdate.Text = "Обновить журнал";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// data
			// 
			this.data.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
			this.data.AllowEditing = false;
			this.data.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
			this.data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.data.ColumnInfo = "1,1,0,0,0,85,Columns:0{Width:20;}\t";
			this.data.Location = new System.Drawing.Point(2, 63);
			this.data.Name = "data";
			this.data.Rows.DefaultSize = 17;
			this.data.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.data.Size = new System.Drawing.Size(790, 385);
			this.data.StyleInfo = resources.GetString("data.StyleInfo");
			this.data.TabIndex = 8;
			this.data.DoubleClick += new System.EventHandler(this.data_DoubleClick);
			// 
			// frmOrderDiscardList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(792, 566);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.data);
			this.Name = "frmOrderDiscardList";
			this.Load += new System.EventHandler(this.frmOrderDiscardList_Load);
			this.Controls.SetChildIndex(this.data, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnOpenOrder;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnUpdate;
		private C1.Win.C1FlexGrid.C1FlexGrid data;
	}
}
