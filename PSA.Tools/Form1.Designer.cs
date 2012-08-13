namespace PSA.Tools
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnAction1 = new System.Windows.Forms.Button();
            this.lstInventory = new System.Windows.Forms.ListBox();
            this.pb1 = new System.Windows.Forms.ProgressBar();
            this.btnAction1a = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAction1с = new System.Windows.Forms.Button();
            this.btnAction1b = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lstinfo = new System.Windows.Forms.ListBox();
            this.lblInfo = new System.Windows.Forms.RichTextBox();
            this.chlstOrderInventory = new System.Windows.Forms.CheckedListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.linkdeall2 = new System.Windows.Forms.LinkLabel();
            this.linkall2 = new System.Windows.Forms.LinkLabel();
            this.btnAction2b = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.linkdeall1 = new System.Windows.Forms.LinkLabel();
            this.linkall1 = new System.Windows.Forms.LinkLabel();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnAction2a = new System.Windows.Forms.Button();
            this.chlststatus = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateOldest = new System.Windows.Forms.MonthCalendar();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cstr = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.qy = new System.Windows.Forms.RichTextBox();
            this.datav = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.httpRezult = new System.Windows.Forms.RichTextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.httpStr = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.btnUnZip = new System.Windows.Forms.Button();
            this.txtZo = new System.Windows.Forms.TextBox();
            this.txtZ = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtSQLtoCSV = new System.Windows.Forms.RichTextBox();
            this.gridSQLtoCSV = new System.Windows.Forms.DataGridView();
            this.btnSQLtoCSV = new System.Windows.Forms.Button();
            this.txtSQL = new System.Windows.Forms.RichTextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ftpAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ftpUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ftpPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ftpSource = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ftpDestanation = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datav)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSQLtoCSV)).BeginInit();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(588, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Заказы, не найденные в выбранной инвентаризации, по которым не было дальнейшего д" +
                "вижения (с момента инвентаризации и по текущий момент) переводятся в статус \"уте" +
                "ряно\".";
            // 
            // btnAction1
            // 
            this.btnAction1.Location = new System.Drawing.Point(217, 152);
            this.btnAction1.Name = "btnAction1";
            this.btnAction1.Size = new System.Drawing.Size(188, 23);
            this.btnAction1.TabIndex = 1;
            this.btnAction1.Text = "Проверить инвентаризацию";
            this.btnAction1.UseVisualStyleBackColor = true;
            this.btnAction1.Click += new System.EventHandler(this.btnAction1_Click);
            // 
            // lstInventory
            // 
            this.lstInventory.FormattingEnabled = true;
            this.lstInventory.Location = new System.Drawing.Point(3, 51);
            this.lstInventory.Name = "lstInventory";
            this.lstInventory.Size = new System.Drawing.Size(208, 95);
            this.lstInventory.TabIndex = 2;
            // 
            // pb1
            // 
            this.pb1.Location = new System.Drawing.Point(217, 328);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(377, 21);
            this.pb1.TabIndex = 3;
            // 
            // btnAction1a
            // 
            this.btnAction1a.Location = new System.Drawing.Point(3, 152);
            this.btnAction1a.Name = "btnAction1a";
            this.btnAction1a.Size = new System.Drawing.Size(208, 23);
            this.btnAction1a.TabIndex = 4;
            this.btnAction1a.Text = "Загрузить список";
            this.btnAction1a.UseVisualStyleBackColor = true;
            this.btnAction1a.Click += new System.EventHandler(this.btnAction1a_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(608, 381);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAction1с);
            this.tabPage1.Controls.Add(this.btnAction1b);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lstinfo);
            this.tabPage1.Controls.Add(this.lblInfo);
            this.tabPage1.Controls.Add(this.chlstOrderInventory);
            this.tabPage1.Controls.Add(this.lstInventory);
            this.tabPage1.Controls.Add(this.pb1);
            this.tabPage1.Controls.Add(this.btnAction1a);
            this.tabPage1.Controls.Add(this.btnAction1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(600, 355);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Инвентаризация";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnAction1с
            // 
            this.btnAction1с.Location = new System.Drawing.Point(217, 226);
            this.btnAction1с.Name = "btnAction1с";
            this.btnAction1с.Size = new System.Drawing.Size(188, 40);
            this.btnAction1с.TabIndex = 12;
            this.btnAction1с.Text = "Восстановить из резервной копии";
            this.btnAction1с.UseVisualStyleBackColor = true;
            this.btnAction1с.Click += new System.EventHandler(this.btnAction1с_Click);
            // 
            // btnAction1b
            // 
            this.btnAction1b.Location = new System.Drawing.Point(217, 197);
            this.btnAction1b.Name = "btnAction1b";
            this.btnAction1b.Size = new System.Drawing.Size(188, 23);
            this.btnAction1b.TabIndex = 11;
            this.btnAction1b.Text = "Изменить статусы заказов";
            this.btnAction1b.UseVisualStyleBackColor = true;
            this.btnAction1b.Click += new System.EventHandler(this.btnAction1b_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Заказы в статус УТЕРЯНО";
            // 
            // lstinfo
            // 
            this.lstinfo.FormattingEnabled = true;
            this.lstinfo.Location = new System.Drawing.Point(217, 51);
            this.lstinfo.Name = "lstinfo";
            this.lstinfo.Size = new System.Drawing.Size(377, 30);
            this.lstinfo.TabIndex = 9;
            this.lstinfo.SelectedIndexChanged += new System.EventHandler(this.lstinfo_SelectedIndexChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(217, 87);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.ReadOnly = true;
            this.lblInfo.Size = new System.Drawing.Size(377, 59);
            this.lblInfo.TabIndex = 8;
            this.lblInfo.Text = "";
            // 
            // chlstOrderInventory
            // 
            this.chlstOrderInventory.FormattingEnabled = true;
            this.chlstOrderInventory.Location = new System.Drawing.Point(3, 181);
            this.chlstOrderInventory.Name = "chlstOrderInventory";
            this.chlstOrderInventory.Size = new System.Drawing.Size(208, 169);
            this.chlstOrderInventory.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.linkdeall2);
            this.tabPage2.Controls.Add(this.linkall2);
            this.tabPage2.Controls.Add(this.btnAction2b);
            this.tabPage2.Controls.Add(this.progressBar1);
            this.tabPage2.Controls.Add(this.linkdeall1);
            this.tabPage2.Controls.Add(this.linkall1);
            this.tabPage2.Controls.Add(this.c1FlexGrid1);
            this.tabPage2.Controls.Add(this.btnAction2a);
            this.tabPage2.Controls.Add(this.chlststatus);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.dateOldest);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(600, 355);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Удаление заказов";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkdeall2
            // 
            this.linkdeall2.AutoSize = true;
            this.linkdeall2.Enabled = false;
            this.linkdeall2.Location = new System.Drawing.Point(89, 314);
            this.linkdeall2.Name = "linkdeall2";
            this.linkdeall2.Size = new System.Drawing.Size(82, 13);
            this.linkdeall2.TabIndex = 13;
            this.linkdeall2.TabStop = true;
            this.linkdeall2.Text = "Снять отметки";
            // 
            // linkall2
            // 
            this.linkall2.AutoSize = true;
            this.linkall2.Enabled = false;
            this.linkall2.Location = new System.Drawing.Point(6, 314);
            this.linkall2.Name = "linkall2";
            this.linkall2.Size = new System.Drawing.Size(77, 13);
            this.linkall2.TabIndex = 12;
            this.linkall2.TabStop = true;
            this.linkall2.Text = "Отметить все";
            // 
            // btnAction2b
            // 
            this.btnAction2b.Enabled = false;
            this.btnAction2b.Location = new System.Drawing.Point(495, 258);
            this.btnAction2b.Name = "btnAction2b";
            this.btnAction2b.Size = new System.Drawing.Size(95, 23);
            this.btnAction2b.TabIndex = 11;
            this.btnAction2b.Text = "Удалить";
            this.btnAction2b.UseVisualStyleBackColor = true;
            this.btnAction2b.Click += new System.EventHandler(this.btnAction2b_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Enabled = false;
            this.progressBar1.Location = new System.Drawing.Point(9, 330);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(585, 22);
            this.progressBar1.TabIndex = 10;
            // 
            // linkdeall1
            // 
            this.linkdeall1.AutoSize = true;
            this.linkdeall1.Enabled = false;
            this.linkdeall1.Location = new System.Drawing.Point(512, 162);
            this.linkdeall1.Name = "linkdeall1";
            this.linkdeall1.Size = new System.Drawing.Size(82, 13);
            this.linkdeall1.TabIndex = 9;
            this.linkdeall1.TabStop = true;
            this.linkdeall1.Text = "Снять отметки";
            this.linkdeall1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkdeall1_LinkClicked);
            // 
            // linkall1
            // 
            this.linkall1.AutoSize = true;
            this.linkall1.Enabled = false;
            this.linkall1.Location = new System.Drawing.Point(429, 162);
            this.linkall1.Name = "linkall1";
            this.linkall1.Size = new System.Drawing.Size(77, 13);
            this.linkall1.TabIndex = 8;
            this.linkall1.TabStop = true;
            this.linkall1.Text = "Отметить все";
            this.linkall1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkall1_LinkClicked);
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:19;}\t";
            this.c1FlexGrid1.Enabled = false;
            this.c1FlexGrid1.Location = new System.Drawing.Point(9, 192);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.DefaultSize = 17;
            this.c1FlexGrid1.Size = new System.Drawing.Size(480, 119);
            this.c1FlexGrid1.TabIndex = 7;
            // 
            // btnAction2a
            // 
            this.btnAction2a.Enabled = false;
            this.btnAction2a.Location = new System.Drawing.Point(316, 157);
            this.btnAction2a.Name = "btnAction2a";
            this.btnAction2a.Size = new System.Drawing.Size(107, 23);
            this.btnAction2a.TabIndex = 6;
            this.btnAction2a.Text = "Загрузить";
            this.btnAction2a.UseVisualStyleBackColor = true;
            this.btnAction2a.Click += new System.EventHandler(this.btnAction2a_Click);
            // 
            // chlststatus
            // 
            this.chlststatus.Enabled = false;
            this.chlststatus.FormattingEnabled = true;
            this.chlststatus.Location = new System.Drawing.Point(316, 25);
            this.chlststatus.Name = "chlststatus";
            this.chlststatus.Size = new System.Drawing.Size(278, 124);
            this.chlststatus.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(261, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Статусы";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Дата приема";
            // 
            // dateOldest
            // 
            this.dateOldest.Enabled = false;
            this.dateOldest.Location = new System.Drawing.Point(92, 25);
            this.dateOldest.Name = "dateOldest";
            this.dateOldest.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(456, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Удаляются все заказы в у казанных статусах, дата приема которых меньше указанной";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cstr);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.qy);
            this.tabPage3.Controls.Add(this.datav);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(600, 355);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "FirebirdTest";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cstr
            // 
            this.cstr.Location = new System.Drawing.Point(87, 9);
            this.cstr.Name = "cstr";
            this.cstr.Size = new System.Drawing.Size(340, 20);
            this.cstr.TabIndex = 4;
            this.cstr.Text = "192.168.X.X@c:\\projects\\Avantime-kiosk\\bin\\base\\df.base";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "RUN!";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // qy
            // 
            this.qy.Location = new System.Drawing.Point(6, 35);
            this.qy.Name = "qy";
            this.qy.Size = new System.Drawing.Size(588, 107);
            this.qy.TabIndex = 2;
            this.qy.Text = "";
            // 
            // datav
            // 
            this.datav.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datav.Location = new System.Drawing.Point(6, 148);
            this.datav.Name = "datav";
            this.datav.Size = new System.Drawing.Size(588, 201);
            this.datav.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(433, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open ad execute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.httpRezult);
            this.tabPage4.Controls.Add(this.btnGo);
            this.tabPage4.Controls.Add(this.httpStr);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(600, 355);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "http";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // httpRezult
            // 
            this.httpRezult.Location = new System.Drawing.Point(3, 32);
            this.httpRezult.Name = "httpRezult";
            this.httpRezult.Size = new System.Drawing.Size(594, 320);
            this.httpRezult.TabIndex = 2;
            this.httpRezult.Text = "";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(522, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 1;
            this.btnGo.Text = "GO!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // httpStr
            // 
            this.httpStr.Location = new System.Drawing.Point(3, 6);
            this.httpStr.Name = "httpStr";
            this.httpStr.Size = new System.Drawing.Size(513, 20);
            this.httpStr.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.button4);
            this.tabPage5.Controls.Add(this.btnUnZip);
            this.tabPage5.Controls.Add(this.txtZo);
            this.tabPage5.Controls.Add(this.txtZ);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(600, 355);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "ZIP";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(113, 85);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Zip file";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnUnZip
            // 
            this.btnUnZip.Location = new System.Drawing.Point(32, 85);
            this.btnUnZip.Name = "btnUnZip";
            this.btnUnZip.Size = new System.Drawing.Size(75, 23);
            this.btnUnZip.TabIndex = 2;
            this.btnUnZip.Text = "UnZip";
            this.btnUnZip.UseVisualStyleBackColor = true;
            this.btnUnZip.Click += new System.EventHandler(this.btnUnZip_Click);
            // 
            // txtZo
            // 
            this.txtZo.Location = new System.Drawing.Point(32, 59);
            this.txtZo.Name = "txtZo";
            this.txtZo.Size = new System.Drawing.Size(423, 20);
            this.txtZo.TabIndex = 1;
            this.txtZo.Text = "c:\\Temp\\";
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(32, 33);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(423, 20);
            this.txtZ.TabIndex = 0;
            this.txtZ.Text = "c:\\Temp\\korder.xml.zip";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtSQLtoCSV);
            this.tabPage6.Controls.Add(this.gridSQLtoCSV);
            this.tabPage6.Controls.Add(this.btnSQLtoCSV);
            this.tabPage6.Controls.Add(this.txtSQL);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(600, 355);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "SQL2CSV";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtSQLtoCSV
            // 
            this.txtSQLtoCSV.Location = new System.Drawing.Point(302, 185);
            this.txtSQLtoCSV.Name = "txtSQLtoCSV";
            this.txtSQLtoCSV.Size = new System.Drawing.Size(292, 164);
            this.txtSQLtoCSV.TabIndex = 3;
            this.txtSQLtoCSV.Text = "";
            // 
            // gridSQLtoCSV
            // 
            this.gridSQLtoCSV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSQLtoCSV.Location = new System.Drawing.Point(6, 185);
            this.gridSQLtoCSV.Name = "gridSQLtoCSV";
            this.gridSQLtoCSV.Size = new System.Drawing.Size(290, 164);
            this.gridSQLtoCSV.TabIndex = 2;
            // 
            // btnSQLtoCSV
            // 
            this.btnSQLtoCSV.Location = new System.Drawing.Point(6, 149);
            this.btnSQLtoCSV.Name = "btnSQLtoCSV";
            this.btnSQLtoCSV.Size = new System.Drawing.Size(100, 30);
            this.btnSQLtoCSV.TabIndex = 1;
            this.btnSQLtoCSV.Text = "SQL 2 CSV";
            this.btnSQLtoCSV.UseVisualStyleBackColor = true;
            this.btnSQLtoCSV.Click += new System.EventHandler(this.btnSQLtoCSV_Click);
            // 
            // txtSQL
            // 
            this.txtSQL.Location = new System.Drawing.Point(6, 6);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(588, 137);
            this.txtSQL.TabIndex = 0;
            this.txtSQL.Text = "";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.button3);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(600, 355);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "SQLBACKUP";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "backup";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.groupBox1);
            this.tabPage8.Controls.Add(this.button5);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(600, 355);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Robot";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "Text Export";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(545, 414);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(227, 149);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 1;
            this.button6.Text = "Copy ftp";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.ftpDestanation);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.ftpSource);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.ftpPassword);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ftpUser);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ftpAddress);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Location = new System.Drawing.Point(6, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 186);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ftp";
            // 
            // ftpAddress
            // 
            this.ftpAddress.Location = new System.Drawing.Point(72, 19);
            this.ftpAddress.Name = "ftpAddress";
            this.ftpAddress.Size = new System.Drawing.Size(230, 20);
            this.ftpAddress.TabIndex = 2;
            this.ftpAddress.Text = "int.fotoland.ru";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "ftp Address";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "ftp user";
            // 
            // ftpUser
            // 
            this.ftpUser.Location = new System.Drawing.Point(72, 45);
            this.ftpUser.Name = "ftpUser";
            this.ftpUser.Size = new System.Drawing.Size(230, 20);
            this.ftpUser.TabIndex = 4;
            this.ftpUser.Text = "psa";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "ftp pass";
            // 
            // ftpPassword
            // 
            this.ftpPassword.Location = new System.Drawing.Point(72, 71);
            this.ftpPassword.Name = "ftpPassword";
            this.ftpPassword.Size = new System.Drawing.Size(230, 20);
            this.ftpPassword.TabIndex = 6;
            this.ftpPassword.Text = "solaris";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "ftp source";
            // 
            // ftpSource
            // 
            this.ftpSource.Location = new System.Drawing.Point(72, 97);
            this.ftpSource.Name = "ftpSource";
            this.ftpSource.Size = new System.Drawing.Size(230, 20);
            this.ftpSource.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "ftp dest";
            // 
            // ftpDestanation
            // 
            this.ftpDestanation.Location = new System.Drawing.Point(72, 123);
            this.ftpDestanation.Name = "ftpDestanation";
            this.ftpDestanation.Size = new System.Drawing.Size(230, 20);
            this.ftpDestanation.TabIndex = 10;
            this.ftpDestanation.Text = "/";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 456);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Tools";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datav)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSQLtoCSV)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAction1;
        private System.Windows.Forms.ListBox lstInventory;
        private System.Windows.Forms.ProgressBar pb1;
        private System.Windows.Forms.Button btnAction1a;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckedListBox chlstOrderInventory;
        private System.Windows.Forms.RichTextBox lblInfo;
        private System.Windows.Forms.ListBox lstinfo;
        private System.Windows.Forms.Button btnAction1b;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAction1с;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Button btnAction2a;
        private System.Windows.Forms.CheckedListBox chlststatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MonthCalendar dateOldest;
        private System.Windows.Forms.Label label3;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.LinkLabel linkdeall2;
        private System.Windows.Forms.LinkLabel linkall2;
        private System.Windows.Forms.Button btnAction2b;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.LinkLabel linkdeall1;
		private System.Windows.Forms.LinkLabel linkall1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView datav;
		private System.Windows.Forms.RichTextBox qy;
		private System.Windows.Forms.TextBox cstr;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.RichTextBox httpRezult;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.TextBox httpStr;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.Button btnUnZip;
		private System.Windows.Forms.TextBox txtZo;
		private System.Windows.Forms.TextBox txtZ;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView gridSQLtoCSV;
        private System.Windows.Forms.Button btnSQLtoCSV;
        private System.Windows.Forms.RichTextBox txtSQL;
        private System.Windows.Forms.RichTextBox txtSQLtoCSV;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ftpDestanation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ftpSource;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ftpPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ftpUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ftpAddress;
    }
}

