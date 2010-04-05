namespace Photoland.Forms.Admin
{
	partial class frmOrderLockTable
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderLockTable));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnClearDesigner = new System.Windows.Forms.Button();
			this.btnClearOperator = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.gridData = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.btnUpdate);
			this.groupBox2.Controls.Add(this.btnClearDesigner);
			this.groupBox2.Controls.Add(this.btnClearOperator);
			this.groupBox2.Controls.Add(this.btnClose);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox2.Location = new System.Drawing.Point(506, 458);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(474, 96);
			this.groupBox2.TabIndex = 17;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Действия";
			// 
			// btnClearDesigner
			// 
			this.btnClearDesigner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnClearDesigner.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnClearDesigner.Location = new System.Drawing.Point(6, 20);
			this.btnClearDesigner.Name = "btnClearDesigner";
			this.btnClearDesigner.Size = new System.Drawing.Size(160, 30);
			this.btnClearDesigner.TabIndex = 5;
			this.btnClearDesigner.Text = "Очистить дизайнера";
			this.btnClearDesigner.UseVisualStyleBackColor = true;
			this.btnClearDesigner.Click += new System.EventHandler(this.btnClearDesigner_Click);
			// 
			// btnClearOperator
			// 
			this.btnClearOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnClearOperator.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnClearOperator.Location = new System.Drawing.Point(6, 56);
			this.btnClearOperator.Name = "btnClearOperator";
			this.btnClearOperator.Size = new System.Drawing.Size(160, 30);
			this.btnClearOperator.TabIndex = 4;
			this.btnClearOperator.Text = "Очистить оператора";
			this.btnClearOperator.UseVisualStyleBackColor = true;
			this.btnClearOperator.Click += new System.EventHandler(this.btnClearOperator_Click);
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
			// 
			// gridData
			// 
			this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gridData.BackColor = System.Drawing.Color.White;
			this.gridData.ColumnInfo = "2,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:945;}\t";
			this.gridData.Location = new System.Drawing.Point(0, 0);
			this.gridData.Name = "gridData";
			this.gridData.Rows.Count = 2;
			this.gridData.Rows.DefaultSize = 17;
			this.gridData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.gridData.Size = new System.Drawing.Size(992, 452);
			this.gridData.StyleInfo = resources.GetString("gridData.StyleInfo");
			this.gridData.TabIndex = 16;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnUpdate.Location = new System.Drawing.Point(172, 20);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(130, 30);
			this.btnUpdate.TabIndex = 6;
			this.btnUpdate.Text = "Обновить";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// frmOrderLockTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 566);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.gridData);
			this.Name = "frmOrderLockTable";
			this.Text = "frmOrderLockTable";
			this.Load += new System.EventHandler(this.frmOrderLockTable_Load);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnClearDesigner;
		private System.Windows.Forms.Button btnClearOperator;
		private System.Windows.Forms.Button btnClose;
		private C1.Win.C1FlexGrid.C1FlexGrid gridData;
		private System.Windows.Forms.Button btnUpdate;
	}
}