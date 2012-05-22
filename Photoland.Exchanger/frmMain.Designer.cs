namespace Photoland.Exchanger
{
	partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuService = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.semaphoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.GridOder = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnExport2 = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFilterClear = new System.Windows.Forms.LinkLabel();
            this.btnFilterAll = new System.Windows.Forms.LinkLabel();
            this.pb = new System.Windows.Forms.ProgressBar();
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
            this.btnFilterApply = new System.Windows.Forms.Button();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.rep = new C1.Win.C1Report.C1Report();
            this.btnImportTerminal = new System.Windows.Forms.Button();
            this.lblSaving = new System.Windows.Forms.Label();
            this.btnImportMFoto = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridOder)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rep)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuService,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1005, 24);
            this.mnuMain.TabIndex = 2;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(45, 20);
            this.mnuFile.Text = "Файл";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.ExitToolStripMenuItem.Text = "Выход";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // mnuService
            // 
            this.mnuService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetup,
            this.semaphoresToolStripMenuItem,
            this.toolStripSeparator1,
            this.loadNewToolStripMenuItem});
            this.mnuService.Name = "mnuService";
            this.mnuService.Size = new System.Drawing.Size(55, 20);
            this.mnuService.Text = "Сервис";
            // 
            // mnuSetup
            // 
            this.mnuSetup.Name = "mnuSetup";
            this.mnuSetup.Size = new System.Drawing.Size(265, 22);
            this.mnuSetup.Text = "Настройки";
            this.mnuSetup.Click += new System.EventHandler(this.mnuSetup_Click);
            // 
            // semaphoresToolStripMenuItem
            // 
            this.semaphoresToolStripMenuItem.Name = "semaphoresToolStripMenuItem";
            this.semaphoresToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.semaphoresToolStripMenuItem.Text = "Семафоры";
            this.semaphoresToolStripMenuItem.Click += new System.EventHandler(this.semaphoresToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(262, 6);
            // 
            // loadNewToolStripMenuItem
            // 
            this.loadNewToolStripMenuItem.Name = "loadNewToolStripMenuItem";
            this.loadNewToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.loadNewToolStripMenuItem.Text = "Установить последнее обновление";
            this.loadNewToolStripMenuItem.Click += new System.EventHandler(this.loadNewToolStripMenuItem_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(59, 20);
            this.mnuHelp.Text = "Помощь";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpdate.Location = new System.Drawing.Point(889, 438);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(104, 30);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // GridOder
            // 
            this.GridOder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridOder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.GridOder.ColumnInfo = "7,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:85;Style:\"DataType:System.Boolean;TextA" +
                "lign:LeftCenter;ImageAlign:CenterCenter;\";}\t2{Width:85;}\t3{Width:100;}\t4{Width:1" +
                "00;}\t5{Width:100;}\t6{Width:450;}\t";
            this.GridOder.Enabled = false;
            this.GridOder.Location = new System.Drawing.Point(0, 24);
            this.GridOder.Name = "GridOder";
            this.GridOder.Rows.Count = 2;
            this.GridOder.Rows.DefaultSize = 17;
            this.GridOder.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.GridOder.Size = new System.Drawing.Size(1005, 404);
            this.GridOder.StyleInfo = resources.GetString("GridOder.StyleInfo");
            this.GridOder.TabIndex = 8;
            this.GridOder.Click += new System.EventHandler(this.GridOder_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(889, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 30);
            this.button1.TabIndex = 10;
            this.button1.Text = "Открыть заказ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnImport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImport.Location = new System.Drawing.Point(735, 438);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(148, 35);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "ПРИНЯТЬ ЗАКАЗ";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClose.Location = new System.Drawing.Point(889, 510);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSelect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSelect.Location = new System.Drawing.Point(755, 518);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(169, 30);
            this.btnSelect.TabIndex = 9;
            this.btnSelect.Text = "Выделить все";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Visible = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnExport2
            // 
            this.btnExport2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnExport2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExport2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExport2.Location = new System.Drawing.Point(12, 505);
            this.btnExport2.Name = "btnExport2";
            this.btnExport2.Size = new System.Drawing.Size(137, 60);
            this.btnExport2.TabIndex = 11;
            this.btnExport2.Text = "ГОТОВЫЕ отправить";
            this.btnExport2.UseVisualStyleBackColor = false;
            this.btnExport2.Click += new System.EventHandler(this.btnExport2_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExport.Location = new System.Drawing.Point(12, 438);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(137, 60);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "НА ИЗГОТОВЛЕНИЕ отправить";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnFilterClear);
            this.groupBox1.Controls.Add(this.btnFilterAll);
            this.groupBox1.Controls.Add(this.pb);
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
            this.groupBox1.Controls.Add(this.btnFilterApply);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.groupBox1.Location = new System.Drawing.Point(155, 434);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 131);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр";
            // 
            // btnFilterClear
            // 
            this.btnFilterClear.AutoSize = true;
            this.btnFilterClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFilterClear.Location = new System.Drawing.Point(466, 72);
            this.btnFilterClear.Name = "btnFilterClear";
            this.btnFilterClear.Size = new System.Drawing.Size(81, 13);
            this.btnFilterClear.TabIndex = 14;
            this.btnFilterClear.TabStop = true;
            this.btnFilterClear.Text = "Снять отметку";
            this.btnFilterClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnFilterClear_LinkClicked);
            // 
            // btnFilterAll
            // 
            this.btnFilterAll.AutoSize = true;
            this.btnFilterAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFilterAll.Location = new System.Drawing.Point(383, 72);
            this.btnFilterAll.Name = "btnFilterAll";
            this.btnFilterAll.Size = new System.Drawing.Size(77, 13);
            this.btnFilterAll.TabIndex = 13;
            this.btnFilterAll.TabStop = true;
            this.btnFilterAll.Text = "Отметить все";
            this.btnFilterAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnFilterAll_LinkClicked);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(9, 96);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(421, 23);
            this.pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb.TabIndex = 11;
            this.pb.Visible = false;
            // 
            // checkStatus
            // 
            this.checkStatus.FormattingEnabled = true;
            this.checkStatus.Location = new System.Drawing.Point(383, 20);
            this.checkStatus.Name = "checkStatus";
            this.checkStatus.Size = new System.Drawing.Size(183, 52);
            this.checkStatus.TabIndex = 12;
            // 
            // checkFilterInput
            // 
            this.checkFilterInput.AutoSize = true;
            this.checkFilterInput.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkFilterInput.Location = new System.Drawing.Point(204, 20);
            this.checkFilterInput.Name = "checkFilterInput";
            this.checkFilterInput.Size = new System.Drawing.Size(65, 19);
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
            this.checkFilterOutput.Size = new System.Drawing.Size(70, 19);
            this.checkFilterOutput.TabIndex = 10;
            this.checkFilterOutput.Text = "Выдача";
            this.checkFilterOutput.UseVisualStyleBackColor = true;
            this.checkFilterOutput.CheckedChanged += new System.EventHandler(this.checkFilterOutput_CheckedChanged);
            // 
            // txtDateBeginPr
            // 
            this.txtDateBeginPr.Enabled = false;
            this.txtDateBeginPr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDateBeginPr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateBeginPr.Location = new System.Drawing.Point(289, 44);
            this.txtDateBeginPr.Name = "txtDateBeginPr";
            this.txtDateBeginPr.Size = new System.Drawing.Size(88, 20);
            this.txtDateBeginPr.TabIndex = 0;
            // 
            // txtDateBegin
            // 
            this.txtDateBegin.Enabled = false;
            this.txtDateBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDateBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateBegin.Location = new System.Drawing.Point(98, 44);
            this.txtDateBegin.Name = "txtDateBegin";
            this.txtDateBegin.Size = new System.Drawing.Size(88, 20);
            this.txtDateBegin.TabIndex = 0;
            // 
            // txtDateEndPr
            // 
            this.txtDateEndPr.Enabled = false;
            this.txtDateEndPr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDateEndPr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateEndPr.Location = new System.Drawing.Point(289, 70);
            this.txtDateEndPr.Name = "txtDateEndPr";
            this.txtDateEndPr.Size = new System.Drawing.Size(88, 20);
            this.txtDateEndPr.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(192, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Начальная дата";
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.Enabled = false;
            this.txtDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateEnd.Location = new System.Drawing.Point(98, 70);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(88, 20);
            this.txtDateEnd.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(192, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Конечная дата";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(6, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Начальная дата";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Конечная дата";
            // 
            // btnFilterApply
            // 
            this.btnFilterApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFilterApply.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFilterApply.Location = new System.Drawing.Point(436, 89);
            this.btnFilterApply.Name = "btnFilterApply";
            this.btnFilterApply.Size = new System.Drawing.Size(130, 30);
            this.btnFilterApply.TabIndex = 6;
            this.btnFilterApply.Text = "Применить";
            this.btnFilterApply.UseVisualStyleBackColor = true;
            this.btnFilterApply.Click += new System.EventHandler(this.btnFilterApply_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "PSA 1.0 Export files (*.export)|*.export|All files (*.*)|*.*";
            this.dlgSave.Title = "Сохранить файл экспорта";
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "PSA 1.0 Export files (*.export)|*.export|All files (*.*)|*.*";
            this.dlgOpen.Title = "Открыть файл экспорта";
            // 
            // rep
            // 
            this.rep.ReportDefinition = resources.GetString("rep.ReportDefinition");
            // 
            // btnImportTerminal
            // 
            this.btnImportTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportTerminal.BackColor = System.Drawing.Color.Khaki;
            this.btnImportTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnImportTerminal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImportTerminal.Location = new System.Drawing.Point(735, 476);
            this.btnImportTerminal.Name = "btnImportTerminal";
            this.btnImportTerminal.Size = new System.Drawing.Size(148, 45);
            this.btnImportTerminal.TabIndex = 11;
            this.btnImportTerminal.Text = "ПРИНЯТЬ\r\nс терминалов";
            this.btnImportTerminal.UseVisualStyleBackColor = false;
            this.btnImportTerminal.Click += new System.EventHandler(this.btnImportTerminal_Click);
            // 
            // lblSaving
            // 
            this.lblSaving.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSaving.AutoSize = true;
            this.lblSaving.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSaving.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSaving.Location = new System.Drawing.Point(233, 382);
            this.lblSaving.Name = "lblSaving";
            this.lblSaving.Size = new System.Drawing.Size(229, 37);
            this.lblSaving.TabIndex = 12;
            this.lblSaving.Text = "Сохранение...";
            this.lblSaving.Visible = false;
            // 
            // btnImportMFoto
            // 
            this.btnImportMFoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportMFoto.BackColor = System.Drawing.Color.LimeGreen;
            this.btnImportMFoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnImportMFoto.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImportMFoto.Location = new System.Drawing.Point(735, 525);
            this.btnImportMFoto.Name = "btnImportMFoto";
            this.btnImportMFoto.Size = new System.Drawing.Size(148, 45);
            this.btnImportMFoto.TabIndex = 13;
            this.btnImportMFoto.Text = "ПРИНЯТЬ\r\nс M-ФОТО";
            this.btnImportMFoto.UseVisualStyleBackColor = false;
            this.btnImportMFoto.Click += new System.EventHandler(this.btnImportMFoto_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(439, 197);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 577);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnImportMFoto);
            this.Controls.Add(this.lblSaving);
            this.Controls.Add(this.btnImportTerminal);
            this.Controls.Add(this.btnExport2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GridOder);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.btnSelect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridOder)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mnuMain;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuService;
		private System.Windows.Forms.ToolStripMenuItem mnuSetup;
		private System.Windows.Forms.ToolStripMenuItem mnuHelp;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.Button btnUpdate;
		private C1.Win.C1FlexGrid.C1FlexGrid GridOder;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckedListBox checkStatus;
		private System.Windows.Forms.CheckBox checkFilterInput;
		private System.Windows.Forms.CheckBox checkFilterOutput;
		private System.Windows.Forms.DateTimePicker txtDateBeginPr;
		private System.Windows.Forms.DateTimePicker txtDateBegin;
		private System.Windows.Forms.DateTimePicker txtDateEndPr;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DateTimePicker txtDateEnd;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnFilterApply;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.SaveFileDialog dlgSave;
		private System.Windows.Forms.OpenFileDialog dlgOpen;
		private System.Windows.Forms.ProgressBar pb;
		private System.Windows.Forms.ToolStripMenuItem semaphoresToolStripMenuItem;
        private System.Windows.Forms.Button btnExport2;
		private C1.Win.C1Report.C1Report rep;
		private System.Windows.Forms.Button btnImportTerminal;
		private System.Windows.Forms.Label lblSaving;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem loadNewToolStripMenuItem;
		private System.Windows.Forms.Button btnImportMFoto;
		private System.Windows.Forms.LinkLabel btnFilterClear;
        private System.Windows.Forms.LinkLabel btnFilterAll;
        private System.Windows.Forms.Button button2;
	}
}

