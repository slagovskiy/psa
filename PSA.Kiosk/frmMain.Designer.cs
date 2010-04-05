namespace PSA.Kiosk
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
			this.icon = new System.Windows.Forms.NotifyIcon(this.components);
			this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.handStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.search1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuKiosk = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.semaphoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnMinimize = new System.Windows.Forms.LinkLabel();
			this.btnClose = new System.Windows.Forms.LinkLabel();
			this.lblInfo = new System.Windows.Forms.Label();
			this.tmr = new System.Windows.Forms.Timer(this.components);
			this.rep = new C1.Win.C1Report.C1Report();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblVer = new System.Windows.Forms.Label();
			this.wicor = new System.Windows.Forms.NotifyIcon(this.components);
			this.wicoe = new System.Windows.Forms.NotifyIcon(this.components);
			this.wicob = new System.Windows.Forms.NotifyIcon(this.components);
			this.wicog = new System.Windows.Forms.NotifyIcon(this.components);
			this.btnCallTerminal = new System.Windows.Forms.Button();
			this.mnu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rep)).BeginInit();
			this.SuspendLayout();
			// 
			// icon
			// 
			this.icon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.icon.BalloonTipTitle = "Робот для фототерминалов";
			this.icon.ContextMenuStrip = this.mnu;
			this.icon.Icon = ((System.Drawing.Icon)(resources.GetObject("icon.Icon")));
			this.icon.Text = "Робот для фототерминалов";
			this.icon.Visible = true;
			this.icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.icon_MouseDoubleClick);
			// 
			// mnu
			// 
			this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.toolStripSeparator3,
            this.handStartToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuKiosk,
            this.settingsToolStripMenuItem,
            this.semaphoresToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
			this.mnu.Name = "mnu";
			this.mnu.Size = new System.Drawing.Size(213, 154);
			// 
			// showToolStripMenuItem
			// 
			this.showToolStripMenuItem.Name = "showToolStripMenuItem";
			this.showToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.showToolStripMenuItem.Text = "Показать";
			this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(209, 6);
			// 
			// handStartToolStripMenuItem
			// 
			this.handStartToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.search1ToolStripMenuItem});
			this.handStartToolStripMenuItem.Name = "handStartToolStripMenuItem";
			this.handStartToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.handStartToolStripMenuItem.Text = "Ручной запуск";
			// 
			// search1ToolStripMenuItem
			// 
			this.search1ToolStripMenuItem.Name = "search1ToolStripMenuItem";
			this.search1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.search1ToolStripMenuItem.Text = "Опрос терминалов";
			this.search1ToolStripMenuItem.Click += new System.EventHandler(this.search1ToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(209, 6);
			// 
			// toolStripMenuKiosk
			// 
			this.toolStripMenuKiosk.Name = "toolStripMenuKiosk";
			this.toolStripMenuKiosk.Size = new System.Drawing.Size(212, 22);
			this.toolStripMenuKiosk.Text = "Справочник терминалов";
			this.toolStripMenuKiosk.Click += new System.EventHandler(this.toolStripMenuKiosk_Click);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.settingsToolStripMenuItem.Text = "Настройки";
			this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
			// 
			// semaphoresToolStripMenuItem
			// 
			this.semaphoresToolStripMenuItem.Name = "semaphoresToolStripMenuItem";
			this.semaphoresToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.semaphoresToolStripMenuItem.Text = "Семафоры";
			this.semaphoresToolStripMenuItem.Click += new System.EventHandler(this.semaphoresToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(209, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.exitToolStripMenuItem.Text = "Выход";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// btnMinimize
			// 
			this.btnMinimize.AutoSize = true;
			this.btnMinimize.Location = new System.Drawing.Point(106, 1);
			this.btnMinimize.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.btnMinimize.Name = "btnMinimize";
			this.btnMinimize.Size = new System.Drawing.Size(53, 13);
			this.btnMinimize.TabIndex = 1;
			this.btnMinimize.TabStop = true;
			this.btnMinimize.Text = "свернуть";
			this.btnMinimize.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnMinimize_LinkClicked);
			// 
			// btnClose
			// 
			this.btnClose.AutoSize = true;
			this.btnClose.Location = new System.Drawing.Point(165, 1);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(50, 13);
			this.btnClose.TabIndex = 2;
			this.btnClose.TabStop = true;
			this.btnClose.Text = "закрыть";
			this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
			// 
			// lblInfo
			// 
			this.lblInfo.AutoSize = true;
			this.lblInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblInfo.Location = new System.Drawing.Point(68, 85);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(147, 13);
			this.lblInfo.TabIndex = 3;
			this.lblInfo.Text = "Опрос каталога через: 60с.";
			this.lblInfo.Visible = false;
			// 
			// tmr
			// 
			this.tmr.Interval = 1000;
			this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
			// 
			// rep
			// 
			this.rep.ReportDefinition = resources.GetString("rep.ReportDefinition");
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.Green;
			this.label1.Location = new System.Drawing.Point(1, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(214, 35);
			this.label1.TabIndex = 4;
			this.label1.Text = "ФОТОЛЭНД";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.Green;
			this.label2.Location = new System.Drawing.Point(3, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(212, 18);
			this.label2.TabIndex = 5;
			this.label2.Text = "ФОТОТЕРМИНАЛЫ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblVer
			// 
			this.lblVer.AutoSize = true;
			this.lblVer.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblVer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.lblVer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblVer.Location = new System.Drawing.Point(101, 66);
			this.lblVer.Name = "lblVer";
			this.lblVer.Size = new System.Drawing.Size(89, 12);
			this.lblVer.TabIndex = 6;
			this.lblVer.Text = "PSA version 0.0.0.0";
			// 
			// wicor
			// 
			this.wicor.Icon = ((System.Drawing.Icon)(resources.GetObject("wicor.Icon")));
			// 
			// wicoe
			// 
			this.wicoe.Icon = ((System.Drawing.Icon)(resources.GetObject("wicoe.Icon")));
			// 
			// wicob
			// 
			this.wicob.Icon = ((System.Drawing.Icon)(resources.GetObject("wicob.Icon")));
			// 
			// wicog
			// 
			this.wicog.Icon = ((System.Drawing.Icon)(resources.GetObject("wicog.Icon")));
			this.wicog.Text = "notifyIcon1";
			// 
			// btnCallTerminal
			// 
			this.btnCallTerminal.Location = new System.Drawing.Point(12, 81);
			this.btnCallTerminal.Name = "btnCallTerminal";
			this.btnCallTerminal.Size = new System.Drawing.Size(193, 23);
			this.btnCallTerminal.TabIndex = 7;
			this.btnCallTerminal.Text = "Опросить терминалы";
			this.btnCallTerminal.UseVisualStyleBackColor = true;
			this.btnCallTerminal.Click += new System.EventHandler(this.btnCallTerminal_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(217, 107);
			this.ControlBox = false;
			this.Controls.Add(this.lblVer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCallTerminal);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.btnMinimize);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMain";
			this.Opacity = 0.75;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.mnu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rep)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon icon;
		private System.Windows.Forms.LinkLabel btnMinimize;
		private System.Windows.Forms.LinkLabel btnClose;
		private System.Windows.Forms.ContextMenuStrip mnu;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem handStartToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem semaphoresToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuKiosk;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.Timer tmr;
		private System.Windows.Forms.ToolStripMenuItem search1ToolStripMenuItem;
		private C1.Win.C1Report.C1Report rep;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblVer;
		private System.Windows.Forms.NotifyIcon wicor;
		private System.Windows.Forms.NotifyIcon wicoe;
		private System.Windows.Forms.NotifyIcon wicob;
		private System.Windows.Forms.NotifyIcon wicog;
		private System.Windows.Forms.Button btnCallTerminal;
	}
}

