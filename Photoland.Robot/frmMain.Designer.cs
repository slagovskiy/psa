namespace Photoland.Robot
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHandStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHandImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHandExport = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuService = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.semaphoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.icoTray1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.icoTray2 = new System.Windows.Forms.NotifyIcon(this.components);
            this.icoTray3 = new System.Windows.Forms.NotifyIcon(this.components);
            this.icoTray4 = new System.Windows.Forms.NotifyIcon(this.components);
            this.icoTray5 = new System.Windows.Forms.NotifyIcon(this.components);
            this.icoTray6 = new System.Windows.Forms.NotifyIcon(this.components);
            this.icoTray7 = new System.Windows.Forms.NotifyIcon(this.components);
            this.icoTray8 = new System.Windows.Forms.NotifyIcon(this.components);
            this.icoTray9 = new System.Windows.Forms.NotifyIcon(this.components);
            this.icoTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmrRobot = new System.Windows.Forms.Timer(this.components);
            this.tmrRobotExport = new System.Windows.Forms.Timer(this.components);
            this.pb = new System.Windows.Forms.ProgressBar();
            this.tmrClear = new System.Windows.Forms.Timer(this.components);
            this.tmrCls = new System.Windows.Forms.Timer(this.components);
            this.mnuMain.SuspendLayout();
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
            this.mnuMain.Size = new System.Drawing.Size(314, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHandStart,
            this.ExitToolStripMenuItem});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(45, 20);
            this.mnuFile.Text = "Файл";
            // 
            // mnuHandStart
            // 
            this.mnuHandStart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHandImport,
            this.mnuHandExport,
            this.deleteToolStripMenuItem});
            this.mnuHandStart.Name = "mnuHandStart";
            this.mnuHandStart.Size = new System.Drawing.Size(158, 22);
            this.mnuHandStart.Text = "Ручной запуск";
            // 
            // mnuHandImport
            // 
            this.mnuHandImport.Name = "mnuHandImport";
            this.mnuHandImport.Size = new System.Drawing.Size(218, 22);
            this.mnuHandImport.Text = "Импорт справочников";
            this.mnuHandImport.Click += new System.EventHandler(this.mnuHandImport_Click);
            // 
            // mnuHandExport
            // 
            this.mnuHandExport.Name = "mnuHandExport";
            this.mnuHandExport.Size = new System.Drawing.Size(218, 22);
            this.mnuHandExport.Text = "Экспорт данных";
            this.mnuHandExport.Click += new System.EventHandler(this.mnuHandExport_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.deleteToolStripMenuItem.Text = "Удаление старых заказов";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.ExitToolStripMenuItem.Text = "Выход";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // mnuService
            // 
            this.mnuService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetup,
            this.semaphoresToolStripMenuItem});
            this.mnuService.Name = "mnuService";
            this.mnuService.Size = new System.Drawing.Size(55, 20);
            this.mnuService.Text = "Сервис";
            // 
            // mnuSetup
            // 
            this.mnuSetup.Name = "mnuSetup";
            this.mnuSetup.Size = new System.Drawing.Size(139, 22);
            this.mnuSetup.Text = "Настройки";
            this.mnuSetup.Click += new System.EventHandler(this.mnuSetup_Click);
            // 
            // semaphoresToolStripMenuItem
            // 
            this.semaphoresToolStripMenuItem.Name = "semaphoresToolStripMenuItem";
            this.semaphoresToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.semaphoresToolStripMenuItem.Text = "Семафоры";
            this.semaphoresToolStripMenuItem.Click += new System.EventHandler(this.semaphoresToolStripMenuItem_Click);
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
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(1, 56);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(312, 368);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // icoTray1
            // 
            this.icoTray1.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray1.Icon")));
            this.icoTray1.Text = "Фотолэнд: Робот";
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "Robot1.ico");
            this.imgList.Images.SetKeyName(1, "Robot2.ico");
            this.imgList.Images.SetKeyName(2, "Robot3.ico");
            this.imgList.Images.SetKeyName(3, "Robot4.ico");
            this.imgList.Images.SetKeyName(4, "Robot5.ico");
            this.imgList.Images.SetKeyName(5, "Robot6.ico");
            this.imgList.Images.SetKeyName(6, "Robot7.ico");
            this.imgList.Images.SetKeyName(7, "Robot8.ico");
            this.imgList.Images.SetKeyName(8, "Robot9.ico");
            // 
            // tmr
            // 
            this.tmr.Interval = 1000;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // icoTray2
            // 
            this.icoTray2.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray2.Icon")));
            this.icoTray2.Text = "Фотолэнд: Робот";
            // 
            // icoTray3
            // 
            this.icoTray3.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray3.Icon")));
            this.icoTray3.Text = "Фотолэнд: Робот";
            // 
            // icoTray4
            // 
            this.icoTray4.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray4.Icon")));
            this.icoTray4.Text = "Фотолэнд: Робот";
            // 
            // icoTray5
            // 
            this.icoTray5.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray5.Icon")));
            this.icoTray5.Text = "Фотолэнд: Робот";
            // 
            // icoTray6
            // 
            this.icoTray6.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray6.Icon")));
            this.icoTray6.Text = "Фотолэнд: Робот";
            // 
            // icoTray7
            // 
            this.icoTray7.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray7.Icon")));
            this.icoTray7.Text = "Фотолэнд: Робот";
            // 
            // icoTray8
            // 
            this.icoTray8.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray8.Icon")));
            this.icoTray8.Text = "Фотолэнд: Робот";
            // 
            // icoTray9
            // 
            this.icoTray9.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray9.Icon")));
            this.icoTray9.Text = "Фотолэнд: Робот";
            // 
            // icoTray
            // 
            this.icoTray.Icon = ((System.Drawing.Icon)(resources.GetObject("icoTray.Icon")));
            this.icoTray.Text = "Фотолэнд: Робот";
            this.icoTray.Visible = true;
            this.icoTray.Click += new System.EventHandler(this.icoTray_Click);
            this.icoTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.icoTray_Click);
            // 
            // tmrRobot
            // 
            this.tmrRobot.Interval = 360000;
            this.tmrRobot.Tick += new System.EventHandler(this.tmrRobot_Tick);
            // 
            // tmrRobotExport
            // 
            this.tmrRobotExport.Interval = 360000;
            this.tmrRobotExport.Tick += new System.EventHandler(this.tmrRobotExport_Tick);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(1, 27);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(312, 23);
            this.pb.TabIndex = 3;
            // 
            // tmrClear
            // 
            this.tmrClear.Interval = 240000;
            this.tmrClear.Tick += new System.EventHandler(this.tmrClear_Tick);
            // 
            // tmrCls
            // 
            this.tmrCls.Interval = 600000;
            this.tmrCls.Tick += new System.EventHandler(this.tmrCls_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 426);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.mnuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
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
		private System.Windows.Forms.RichTextBox txtLog;
		private System.Windows.Forms.NotifyIcon icoTray1;
		private System.Windows.Forms.ImageList imgList;
		private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.NotifyIcon icoTray2;
		private System.Windows.Forms.NotifyIcon icoTray3;
		private System.Windows.Forms.NotifyIcon icoTray4;
		private System.Windows.Forms.NotifyIcon icoTray5;
		private System.Windows.Forms.NotifyIcon icoTray6;
		private System.Windows.Forms.NotifyIcon icoTray7;
		private System.Windows.Forms.NotifyIcon icoTray8;
		private System.Windows.Forms.NotifyIcon icoTray9;
		private System.Windows.Forms.NotifyIcon icoTray;
		private System.Windows.Forms.Timer tmrRobot;
		private System.Windows.Forms.ToolStripMenuItem mnuHandStart;
		private System.Windows.Forms.ToolStripMenuItem mnuHandImport;
		private System.Windows.Forms.ToolStripMenuItem mnuHandExport;
		private System.Windows.Forms.Timer tmrRobotExport;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ProgressBar pb;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.Timer tmrClear;
		private System.Windows.Forms.Timer tmrCls;
		private System.Windows.Forms.ToolStripMenuItem semaphoresToolStripMenuItem;
	}
}

