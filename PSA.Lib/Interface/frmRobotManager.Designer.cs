namespace PSA.Lib.Interface
{
	partial class frmRobotManager
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
			this.data = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
			this.SuspendLayout();
			// 
			// data
			// 
			this.data.ColumnInfo = "3,1,0,0,0,85,Columns:0{Width:22;}\t1{Width:245;}\t2{Width:153;}\t";
			this.data.Location = new System.Drawing.Point(3, 63);
			this.data.Name = "data";
			this.data.Rows.DefaultSize = 17;
			this.data.Size = new System.Drawing.Size(444, 263);
			this.data.TabIndex = 2;
			// 
			// btnImport
			// 
			this.btnImport.Location = new System.Drawing.Point(12, 332);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(297, 30);
			this.btnImport.TabIndex = 3;
			this.btnImport.Text = "Начать импорт справочников";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(12, 368);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(297, 30);
			this.btnExport.TabIndex = 4;
			this.btnExport.Text = "Выгрузить данные";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(315, 445);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(120, 30);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "Закрыть";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(189, 445);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(120, 30);
			this.btnUpdate.TabIndex = 6;
			this.btnUpdate.Text = "Обновить";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// tmr
			// 
			this.tmr.Interval = 10000;
			this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 404);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(297, 30);
			this.button1.TabIndex = 7;
			this.button1.Text = "Обновить кэш заказов терминалов";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// frmRobotManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(447, 487);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.data);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnUpdate);
			this.Name = "frmRobotManager";
			this.Load += new System.EventHandler(this.frmRobotManager_Load);
			this.Controls.SetChildIndex(this.btnUpdate, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnImport, 0);
			this.Controls.SetChildIndex(this.btnExport, 0);
			this.Controls.SetChildIndex(this.data, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private C1.Win.C1FlexGrid.C1FlexGrid data;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.Button button1;
	}
}
