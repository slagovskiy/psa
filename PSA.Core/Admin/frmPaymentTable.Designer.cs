namespace Photoland.Forms.Admin
{
	partial class frmPaymentTable
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaymentTable));
			this.GridPayment = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnFilterReset = new System.Windows.Forms.Button();
			this.btnFilterApply = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.ComboBox();
			this.txtDateEnd = new System.Windows.Forms.DateTimePicker();
			this.txtDateBegin = new System.Windows.Forms.DateTimePicker();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button3 = new System.Windows.Forms.Button();
			this.mnuExportExcel = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuExportExcelNoGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuExportExcelGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.button2 = new System.Windows.Forms.Button();
			this.mnuExportPDF = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuExportPDFNoGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuExportPDFGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.mnuReportSelect = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnubtmNoGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnubtnGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.btnDeletePayment = new System.Windows.Forms.Button();
			this.btnAddPayment = new System.Windows.Forms.Button();
			this.btnEditPayment = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.checkAutoUpdate = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.lblSum = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.rep = new C1.Win.C1Report.C1Report();
			this.sdlg = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.GridPayment)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.mnuExportExcel.SuspendLayout();
			this.mnuExportPDF.SuspendLayout();
			this.mnuReportSelect.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rep)).BeginInit();
			this.SuspendLayout();
			// 
			// GridPayment
			// 
			this.GridPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.GridPayment.BackColor = System.Drawing.Color.Gainsboro;
			this.GridPayment.ColumnInfo = "7,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:85;Style:\"Format:\"\"d\"\";DataType:System." +
				"DateTime;TextAlign:LeftCenter;\";}\t2{Width:85;}\t3{Width:100;}\t4{Width:100;}\t5{Wid" +
				"th:100;}\t6{Width:450;}\t";
			this.GridPayment.Location = new System.Drawing.Point(12, 12);
			this.GridPayment.Name = "GridPayment";
			this.GridPayment.Rows.DefaultSize = 17;
			this.GridPayment.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.GridPayment.Size = new System.Drawing.Size(968, 545);
			this.GridPayment.StyleInfo = resources.GetString("GridPayment.StyleInfo");
			this.GridPayment.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.btnFilterReset);
			this.groupBox1.Controls.Add(this.btnFilterApply);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtUser);
			this.groupBox1.Controls.Add(this.txtDateEnd);
			this.groupBox1.Controls.Add(this.txtDateBegin);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox1.Location = new System.Drawing.Point(12, 563);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(285, 135);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Фильтр";
			// 
			// btnFilterReset
			// 
			this.btnFilterReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnFilterReset.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnFilterReset.Location = new System.Drawing.Point(8, 99);
			this.btnFilterReset.Name = "btnFilterReset";
			this.btnFilterReset.Size = new System.Drawing.Size(130, 30);
			this.btnFilterReset.TabIndex = 7;
			this.btnFilterReset.Text = "Сбросить";
			this.btnFilterReset.UseVisualStyleBackColor = true;
			this.btnFilterReset.Click += new System.EventHandler(this.btnFilterReset_Click);
			// 
			// btnFilterApply
			// 
			this.btnFilterApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnFilterApply.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnFilterApply.Location = new System.Drawing.Point(144, 99);
			this.btnFilterApply.Name = "btnFilterApply";
			this.btnFilterApply.Size = new System.Drawing.Size(130, 30);
			this.btnFilterApply.TabIndex = 6;
			this.btnFilterApply.Text = "Применить";
			this.btnFilterApply.UseVisualStyleBackColor = true;
			this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.Color.DarkBlue;
			this.label3.Location = new System.Drawing.Point(17, 75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Пользователь";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.DarkBlue;
			this.label2.Location = new System.Drawing.Point(17, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Конечная дата";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.DarkBlue;
			this.label1.Location = new System.Drawing.Point(17, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Начальная дата";
			// 
			// txtUser
			// 
			this.txtUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtUser.FormattingEnabled = true;
			this.txtUser.Location = new System.Drawing.Point(125, 72);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(149, 21);
			this.txtUser.TabIndex = 2;
			// 
			// txtDateEnd
			// 
			this.txtDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtDateEnd.Location = new System.Drawing.Point(125, 46);
			this.txtDateEnd.Name = "txtDateEnd";
			this.txtDateEnd.Size = new System.Drawing.Size(149, 20);
			this.txtDateEnd.TabIndex = 1;
			// 
			// txtDateBegin
			// 
			this.txtDateBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtDateBegin.Location = new System.Drawing.Point(125, 20);
			this.txtDateBegin.Name = "txtDateBegin";
			this.txtDateBegin.Size = new System.Drawing.Size(149, 20);
			this.txtDateBegin.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.btnDeletePayment);
			this.groupBox2.Controls.Add(this.btnAddPayment);
			this.groupBox2.Controls.Add(this.btnEditPayment);
			this.groupBox2.Controls.Add(this.btnClose);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox2.Location = new System.Drawing.Point(518, 563);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(462, 135);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Действия";
			// 
			// button3
			// 
			this.button3.ContextMenuStrip = this.mnuExportExcel;
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button3.Location = new System.Drawing.Point(6, 92);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(130, 30);
			this.button3.TabIndex = 9;
			this.button3.Text = "Экспорт в Excel";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// mnuExportExcel
			// 
			this.mnuExportExcel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportExcelNoGroup,
            this.mnuExportExcelGroup});
			this.mnuExportExcel.Name = "mnuReportSelect";
			this.mnuExportExcel.Size = new System.Drawing.Size(214, 48);
			// 
			// mnuExportExcelNoGroup
			// 
			this.mnuExportExcelNoGroup.Name = "mnuExportExcelNoGroup";
			this.mnuExportExcelNoGroup.Size = new System.Drawing.Size(213, 22);
			this.mnuExportExcelNoGroup.Text = "Без группировки по дате";
			this.mnuExportExcelNoGroup.Click += new System.EventHandler(this.mnuExportExcelNoGroup_Click);
			// 
			// mnuExportExcelGroup
			// 
			this.mnuExportExcelGroup.Name = "mnuExportExcelGroup";
			this.mnuExportExcelGroup.Size = new System.Drawing.Size(213, 22);
			this.mnuExportExcelGroup.Text = "С группировкой по дате";
			this.mnuExportExcelGroup.Click += new System.EventHandler(this.mnuExportExcelGroup_Click);
			// 
			// button2
			// 
			this.button2.ContextMenuStrip = this.mnuExportPDF;
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button2.Location = new System.Drawing.Point(6, 56);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(130, 30);
			this.button2.TabIndex = 8;
			this.button2.Text = "Экспорт в PDF";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// mnuExportPDF
			// 
			this.mnuExportPDF.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportPDFNoGroup,
            this.mnuExportPDFGroup});
			this.mnuExportPDF.Name = "mnuReportSelect";
			this.mnuExportPDF.Size = new System.Drawing.Size(214, 48);
			// 
			// mnuExportPDFNoGroup
			// 
			this.mnuExportPDFNoGroup.Name = "mnuExportPDFNoGroup";
			this.mnuExportPDFNoGroup.Size = new System.Drawing.Size(213, 22);
			this.mnuExportPDFNoGroup.Text = "Без группировки по дате";
			this.mnuExportPDFNoGroup.Click += new System.EventHandler(this.mnuExportPDFNoGroup_Click);
			// 
			// mnuExportPDFGroup
			// 
			this.mnuExportPDFGroup.Name = "mnuExportPDFGroup";
			this.mnuExportPDFGroup.Size = new System.Drawing.Size(213, 22);
			this.mnuExportPDFGroup.Text = "С группировкой по дате";
			this.mnuExportPDFGroup.Click += new System.EventHandler(this.mnuExportPDFGroup_Click);
			// 
			// button1
			// 
			this.button1.ContextMenuStrip = this.mnuReportSelect;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.button1.Location = new System.Drawing.Point(6, 20);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(130, 30);
			this.button1.TabIndex = 7;
			this.button1.Text = "Печать";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// mnuReportSelect
			// 
			this.mnuReportSelect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnubtmNoGroup,
            this.mnubtnGroup});
			this.mnuReportSelect.Name = "mnuReportSelect";
			this.mnuReportSelect.Size = new System.Drawing.Size(214, 48);
			// 
			// mnubtmNoGroup
			// 
			this.mnubtmNoGroup.Name = "mnubtmNoGroup";
			this.mnubtmNoGroup.Size = new System.Drawing.Size(213, 22);
			this.mnubtmNoGroup.Text = "Без группировки по дате";
			this.mnubtmNoGroup.Click += new System.EventHandler(this.mnubtmNoGroup_Click);
			// 
			// mnubtnGroup
			// 
			this.mnubtnGroup.Name = "mnubtnGroup";
			this.mnubtnGroup.Size = new System.Drawing.Size(213, 22);
			this.mnubtnGroup.Text = "С группировкой по дате";
			this.mnubtnGroup.Click += new System.EventHandler(this.mnubtnGroup_Click);
			// 
			// btnDeletePayment
			// 
			this.btnDeletePayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnDeletePayment.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDeletePayment.Location = new System.Drawing.Point(142, 92);
			this.btnDeletePayment.Name = "btnDeletePayment";
			this.btnDeletePayment.Size = new System.Drawing.Size(130, 30);
			this.btnDeletePayment.TabIndex = 6;
			this.btnDeletePayment.Text = "Удалить платеж";
			this.btnDeletePayment.UseVisualStyleBackColor = true;
			this.btnDeletePayment.Click += new System.EventHandler(this.btnDeletePayment_Click);
			// 
			// btnAddPayment
			// 
			this.btnAddPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnAddPayment.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnAddPayment.Location = new System.Drawing.Point(142, 56);
			this.btnAddPayment.Name = "btnAddPayment";
			this.btnAddPayment.Size = new System.Drawing.Size(130, 30);
			this.btnAddPayment.TabIndex = 5;
			this.btnAddPayment.Text = "Добавить платеж";
			this.btnAddPayment.UseVisualStyleBackColor = true;
			this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
			// 
			// btnEditPayment
			// 
			this.btnEditPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnEditPayment.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnEditPayment.Location = new System.Drawing.Point(142, 20);
			this.btnEditPayment.Name = "btnEditPayment";
			this.btnEditPayment.Size = new System.Drawing.Size(130, 30);
			this.btnEditPayment.TabIndex = 4;
			this.btnEditPayment.Text = "Изменить платеж";
			this.btnEditPayment.UseVisualStyleBackColor = true;
			this.btnEditPayment.Click += new System.EventHandler(this.btnEditPayment_Click);
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnClose.Location = new System.Drawing.Point(320, 20);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(130, 30);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox3.Controls.Add(this.btnUpdate);
			this.groupBox3.Controls.Add(this.checkAutoUpdate);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox3.Location = new System.Drawing.Point(306, 613);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(206, 85);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Обновление";
			// 
			// btnUpdate
			// 
			this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnUpdate.Location = new System.Drawing.Point(70, 49);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(130, 30);
			this.btnUpdate.TabIndex = 1;
			this.btnUpdate.Text = "Обновить";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// checkAutoUpdate
			// 
			this.checkAutoUpdate.AutoSize = true;
			this.checkAutoUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.checkAutoUpdate.Location = new System.Drawing.Point(17, 24);
			this.checkAutoUpdate.Name = "checkAutoUpdate";
			this.checkAutoUpdate.Size = new System.Drawing.Size(184, 19);
			this.checkAutoUpdate.TabIndex = 0;
			this.checkAutoUpdate.Text = "Обновлять автоматически";
			this.checkAutoUpdate.UseVisualStyleBackColor = true;
			this.checkAutoUpdate.CheckedChanged += new System.EventHandler(this.checkAutoUpdate_CheckedChanged);
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox4.Controls.Add(this.lblSum);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox4.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox4.Location = new System.Drawing.Point(306, 563);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(206, 50);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Информация";
			// 
			// lblSum
			// 
			this.lblSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblSum.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblSum.Location = new System.Drawing.Point(67, 20);
			this.lblSum.Name = "lblSum";
			this.lblSum.Size = new System.Drawing.Size(133, 20);
			this.lblSum.TabIndex = 1;
			this.lblSum.Text = "0,00";
			this.lblSum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(6, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Сумма:";
			// 
			// tmr
			// 
			this.tmr.Interval = 10000;
			this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
			// 
			// rep
			// 
			this.rep.ReportDefinition = resources.GetString("rep.ReportDefinition");
			// 
			// frmPaymentTable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 710);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.GridPayment);
			this.Name = "frmPaymentTable";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmPaymentTable";
			this.Load += new System.EventHandler(this.frmPaymentTable_Load);
			((System.ComponentModel.ISupportInitialize)(this.GridPayment)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.mnuExportExcel.ResumeLayout(false);
			this.mnuExportPDF.ResumeLayout(false);
			this.mnuReportSelect.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rep)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private C1.Win.C1FlexGrid.C1FlexGrid GridPayment;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox txtUser;
		private System.Windows.Forms.DateTimePicker txtDateEnd;
		private System.Windows.Forms.DateTimePicker txtDateBegin;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnFilterReset;
		private System.Windows.Forms.Button btnFilterApply;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.CheckBox checkAutoUpdate;
		private System.Windows.Forms.Button btnDeletePayment;
		private System.Windows.Forms.Button btnAddPayment;
		private System.Windows.Forms.Button btnEditPayment;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label lblSum;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Timer tmr;
		private C1.Win.C1Report.C1Report rep;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ContextMenuStrip mnuReportSelect;
		private System.Windows.Forms.ToolStripMenuItem mnubtmNoGroup;
		private System.Windows.Forms.ToolStripMenuItem mnubtnGroup;
		private System.Windows.Forms.SaveFileDialog sdlg;
		private System.Windows.Forms.ContextMenuStrip mnuExportPDF;
		private System.Windows.Forms.ToolStripMenuItem mnuExportPDFNoGroup;
		private System.Windows.Forms.ToolStripMenuItem mnuExportPDFGroup;
		private System.Windows.Forms.ContextMenuStrip mnuExportExcel;
		private System.Windows.Forms.ToolStripMenuItem mnuExportExcelNoGroup;
		private System.Windows.Forms.ToolStripMenuItem mnuExportExcelGroup;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
	}
}