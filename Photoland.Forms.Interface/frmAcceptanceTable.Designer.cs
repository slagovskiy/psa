namespace Photoland.Forms.Interface
{
	partial class frmAcceptanceTable
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAcceptanceTable));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkStatus = new System.Windows.Forms.CheckedListBox();
			this.checkFilterInput = new System.Windows.Forms.CheckBox();
			this.checkFilterOutput = new System.Windows.Forms.CheckBox();
			this.txtDateBeginPr = new System.Windows.Forms.DateTimePicker();
			this.txtDateBegin = new System.Windows.Forms.DateTimePicker();
			this.txtDateEndPr = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.txtDateEnd = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtOrderNoBarCode = new System.Windows.Forms.Label();
			this.btnOpenTable = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.GridOder = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.checkAutoUpdate = new System.Windows.Forms.CheckBox();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.tmrcls = new System.Windows.Forms.Timer(this.components);
			this.checkDouble = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridOder)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.checkDouble);
			this.groupBox1.Controls.Add(this.btnUpdate);
			this.groupBox1.Controls.Add(this.checkAutoUpdate);
			this.groupBox1.Controls.Add(this.checkStatus);
			this.groupBox1.Controls.Add(this.checkFilterInput);
			this.groupBox1.Controls.Add(this.checkFilterOutput);
			this.groupBox1.Controls.Add(this.txtDateBeginPr);
			this.groupBox1.Controls.Add(this.txtDateBegin);
			this.groupBox1.Controls.Add(this.txtDateEndPr);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtDateEnd);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox1.Location = new System.Drawing.Point(12, 586);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(819, 112);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Фильтр";
			// 
			// checkStatus
			// 
			this.checkStatus.FormattingEnabled = true;
			this.checkStatus.Location = new System.Drawing.Point(389, 20);
			this.checkStatus.Name = "checkStatus";
			this.checkStatus.Size = new System.Drawing.Size(207, 76);
			this.checkStatus.TabIndex = 12;
			// 
			// checkFilterInput
			// 
			this.checkFilterInput.AutoSize = true;
			this.checkFilterInput.ForeColor = System.Drawing.SystemColors.ControlText;
			this.checkFilterInput.Location = new System.Drawing.Point(204, 20);
			this.checkFilterInput.Name = "checkFilterInput";
			this.checkFilterInput.Size = new System.Drawing.Size(64, 19);
			this.checkFilterInput.TabIndex = 11;
			this.checkFilterInput.Text = "Прием";
			this.checkFilterInput.UseVisualStyleBackColor = true;
			this.checkFilterInput.CheckedChanged += new System.EventHandler(this.checkFilterInput_CheckedChanged);
			// 
			// checkFilterOutput
			// 
			this.checkFilterOutput.AutoSize = true;
			this.checkFilterOutput.ForeColor = System.Drawing.SystemColors.ControlText;
			this.checkFilterOutput.Location = new System.Drawing.Point(9, 20);
			this.checkFilterOutput.Name = "checkFilterOutput";
			this.checkFilterOutput.Size = new System.Drawing.Size(67, 19);
			this.checkFilterOutput.TabIndex = 10;
			this.checkFilterOutput.Text = "Выдача";
			this.checkFilterOutput.UseVisualStyleBackColor = true;
			this.checkFilterOutput.CheckedChanged += new System.EventHandler(this.checkFilterOutput_CheckedChanged);
			// 
			// txtDateBeginPr
			// 
			this.txtDateBeginPr.Enabled = false;
			this.txtDateBeginPr.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtDateBeginPr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.txtDateBeginPr.Location = new System.Drawing.Point(295, 44);
			this.txtDateBeginPr.Name = "txtDateBeginPr";
			this.txtDateBeginPr.Size = new System.Drawing.Size(88, 22);
			this.txtDateBeginPr.TabIndex = 0;
			// 
			// txtDateBegin
			// 
			this.txtDateBegin.Enabled = false;
			this.txtDateBegin.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtDateBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.txtDateBegin.Location = new System.Drawing.Point(98, 44);
			this.txtDateBegin.Name = "txtDateBegin";
			this.txtDateBegin.Size = new System.Drawing.Size(88, 22);
			this.txtDateBegin.TabIndex = 0;
			// 
			// txtDateEndPr
			// 
			this.txtDateEndPr.Enabled = false;
			this.txtDateEndPr.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtDateEndPr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.txtDateEndPr.Location = new System.Drawing.Point(295, 70);
			this.txtDateEndPr.Name = "txtDateEndPr";
			this.txtDateEndPr.Size = new System.Drawing.Size(88, 22);
			this.txtDateEndPr.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.Color.DarkBlue;
			this.label3.Location = new System.Drawing.Point(201, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(91, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Начальная дата";
			// 
			// txtDateEnd
			// 
			this.txtDateEnd.Enabled = false;
			this.txtDateEnd.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.txtDateEnd.Location = new System.Drawing.Point(98, 70);
			this.txtDateEnd.Name = "txtDateEnd";
			this.txtDateEnd.Size = new System.Drawing.Size(88, 22);
			this.txtDateEnd.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.ForeColor = System.Drawing.Color.DarkBlue;
			this.label5.Location = new System.Drawing.Point(201, 74);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(85, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Конечная дата";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.DarkBlue;
			this.label1.Location = new System.Drawing.Point(6, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Начальная дата";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.DarkBlue;
			this.label2.Location = new System.Drawing.Point(6, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Конечная дата";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.txtOrderNoBarCode);
			this.groupBox2.Controls.Add(this.btnOpenTable);
			this.groupBox2.Controls.Add(this.btnClose);
			this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox2.Location = new System.Drawing.Point(837, 586);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(143, 112);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Действия";
			// 
			// txtOrderNoBarCode
			// 
			this.txtOrderNoBarCode.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtOrderNoBarCode.Location = new System.Drawing.Point(6, 53);
			this.txtOrderNoBarCode.Name = "txtOrderNoBarCode";
			this.txtOrderNoBarCode.Size = new System.Drawing.Size(130, 20);
			this.txtOrderNoBarCode.TabIndex = 12;
			this.txtOrderNoBarCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnOpenTable
			// 
			this.btnOpenTable.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnOpenTable.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnOpenTable.Location = new System.Drawing.Point(6, 20);
			this.btnOpenTable.Name = "btnOpenTable";
			this.btnOpenTable.Size = new System.Drawing.Size(130, 30);
			this.btnOpenTable.TabIndex = 2;
			this.btnOpenTable.Text = "Открыть заказ";
			this.btnOpenTable.UseVisualStyleBackColor = true;
			this.btnOpenTable.Click += new System.EventHandler(this.btnOpenTable_Click);
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnClose.Location = new System.Drawing.Point(6, 76);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(130, 30);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// GridOder
			// 
			this.GridOder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.GridOder.BackColor = System.Drawing.Color.Gainsboro;
			this.GridOder.ColumnInfo = "7,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:85;Style:\"Format:\"\"d\"\";DataType:System." +
				"DateTime;TextAlign:LeftCenter;\";}\t2{Width:85;}\t3{Width:100;}\t4{Width:100;}\t5{Wid" +
				"th:100;}\t6{Width:450;}\t";
			this.GridOder.Location = new System.Drawing.Point(12, 12);
			this.GridOder.Name = "GridOder";
			this.GridOder.Rows.DefaultSize = 17;
			this.GridOder.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.GridOder.Size = new System.Drawing.Size(968, 568);
			this.GridOder.StyleInfo = resources.GetString("GridOder.StyleInfo");
			this.GridOder.TabIndex = 4;
			this.GridOder.DoubleClick += new System.EventHandler(this.GridOder_DoubleClick);
			// 
			// checkAutoUpdate
			// 
			this.checkAutoUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.checkAutoUpdate.Location = new System.Drawing.Point(629, 20);
			this.checkAutoUpdate.Name = "checkAutoUpdate";
			this.checkAutoUpdate.Size = new System.Drawing.Size(184, 19);
			this.checkAutoUpdate.TabIndex = 8;
			this.checkAutoUpdate.Text = "Обновлять автоматически";
			this.checkAutoUpdate.UseVisualStyleBackColor = true;
			this.checkAutoUpdate.CheckedChanged += new System.EventHandler(this.checkAutoUpdate_CheckedChanged);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnUpdate.Location = new System.Drawing.Point(683, 76);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(130, 30);
			this.btnUpdate.TabIndex = 7;
			this.btnUpdate.Text = "Обновить";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// tmr
			// 
			this.tmr.Interval = 10000;
			this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
			// 
			// tmrcls
			// 
			this.tmrcls.Interval = 10000;
			this.tmrcls.Tick += new System.EventHandler(this.tmrcls_Tick);
			// 
			// checkDouble
			// 
			this.checkDouble.AutoSize = true;
			this.checkDouble.Location = new System.Drawing.Point(629, 45);
			this.checkDouble.Name = "checkDouble";
			this.checkDouble.Size = new System.Drawing.Size(156, 19);
			this.checkDouble.TabIndex = 13;
			this.checkDouble.Text = "Дублированные заказы";
			this.checkDouble.UseVisualStyleBackColor = true;
			this.checkDouble.CheckedChanged += new System.EventHandler(this.checkDouble_CheckedChanged);
			// 
			// frmAcceptanceTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 710);
			this.Controls.Add(this.GridOder);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.KeyPreview = true;
			this.Name = "frmAcceptanceTable";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmAcceptanceTable";
			this.Load += new System.EventHandler(this.frmAcceptanceTable_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmAcceptanceTable_KeyPress);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GridOder)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker txtDateEnd;
		private System.Windows.Forms.DateTimePicker txtDateBegin;
		private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
		private C1.Win.C1FlexGrid.C1FlexGrid GridOder;
		private System.Windows.Forms.DateTimePicker txtDateBeginPr;
		private System.Windows.Forms.DateTimePicker txtDateEndPr;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkFilterOutput;
		private System.Windows.Forms.CheckBox checkFilterInput;
		private System.Windows.Forms.CheckBox checkAutoUpdate;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.Button btnOpenTable;
		private System.Windows.Forms.Label txtOrderNoBarCode;
		private System.Windows.Forms.Timer tmrcls;
		private System.Windows.Forms.CheckedListBox checkStatus;
		private System.Windows.Forms.CheckBox checkDouble;
	}
}