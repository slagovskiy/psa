namespace Photoland.Exchanger
{
	partial class frmLoadOrders
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoadOrders));
			this.GridOder = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnSelectAll = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.rep = new C1.Win.C1Report.C1Report();
			this.button1 = new System.Windows.Forms.Button();
			this.checkPrintCheck = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.GridOder)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rep)).BeginInit();
			this.SuspendLayout();
			// 
			// GridOder
			// 
			this.GridOder.BackColor = System.Drawing.Color.WhiteSmoke;
			this.GridOder.ColumnInfo = "7,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:85;Style:\"Format:\"\"d\"\";DataType:System." +
				"DateTime;TextAlign:LeftCenter;\";}\t2{Width:85;}\t3{Width:100;}\t4{Width:100;}\t5{Wid" +
				"th:100;}\t6{Width:450;}\t";
			this.GridOder.Dock = System.Windows.Forms.DockStyle.Top;
			this.GridOder.Location = new System.Drawing.Point(0, 0);
			this.GridOder.Name = "GridOder";
			this.GridOder.Rows.DefaultSize = 17;
			this.GridOder.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.GridOder.Size = new System.Drawing.Size(580, 378);
			this.GridOder.StyleInfo = resources.GetString("GridOder.StyleInfo");
			this.GridOder.TabIndex = 5;
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(121, 384);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(153, 23);
			this.btnPrint.TabIndex = 2;
			this.btnPrint.Text = "Печать списка";
			this.btnPrint.UseVisualStyleBackColor = true;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnSelectAll
			// 
			this.btnSelectAll.Location = new System.Drawing.Point(12, 384);
			this.btnSelectAll.Name = "btnSelectAll";
			this.btnSelectAll.Size = new System.Drawing.Size(103, 23);
			this.btnSelectAll.TabIndex = 0;
			this.btnSelectAll.Text = "Выбрать все";
			this.btnSelectAll.UseVisualStyleBackColor = true;
			this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(12, 413);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(103, 23);
			this.btnClear.TabIndex = 1;
			this.btnClear.Text = "Очистить";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(280, 384);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(149, 23);
			this.btnLoad.TabIndex = 3;
			this.btnLoad.Text = "Загрузить выбранные";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(478, 384);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(98, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Отменить";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// rep
			// 
			this.rep.ReportDefinition = resources.GetString("rep.ReportDefinition");
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(423, 413);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(153, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Печать чеков";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// checkPrintCheck
			// 
			this.checkPrintCheck.AutoSize = true;
			this.checkPrintCheck.Checked = true;
			this.checkPrintCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkPrintCheck.Location = new System.Drawing.Point(121, 417);
			this.checkPrintCheck.Name = "checkPrintCheck";
			this.checkPrintCheck.Size = new System.Drawing.Size(160, 17);
			this.checkPrintCheck.TabIndex = 7;
			this.checkPrintCheck.Text = "Печатать чек при импорте";
			this.checkPrintCheck.UseVisualStyleBackColor = true;
			// 
			// frmLoadOrders
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(580, 447);
			this.Controls.Add(this.checkPrintCheck);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnSelectAll);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.GridOder);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frmLoadOrders";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Загрузка заказов";
			this.Load += new System.EventHandler(this.frmLoadOrders_Load);
			((System.ComponentModel.ISupportInitialize)(this.GridOder)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rep)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private C1.Win.C1FlexGrid.C1FlexGrid GridOder;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnSelectAll;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnCancel;
		private C1.Win.C1Report.C1Report rep;
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.CheckBox checkPrintCheck;
	}
}