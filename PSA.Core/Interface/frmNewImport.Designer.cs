namespace PSA.Lib.Interface
{
    partial class frmNewImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewImport));
            this.data = new System.Windows.Forms.DataGridView();
            this.btnSelectAll = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnLoadSelected = new System.Windows.Forms.Button();
            this.checkPrintCheck = new System.Windows.Forms.CheckBox();
            this.rep = new C1.Win.C1Report.C1Report();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.log = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pb2 = new System.Windows.Forms.ProgressBar();
            this.btnStart = new System.Windows.Forms.Button();
            this.data2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnUpdateSendOrder = new System.Windows.Forms.Button();
            this.data3 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.dateFilterStop1 = new System.Windows.Forms.DateTimePicker();
            this.dateFilterStart1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.data4 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnShowCancelExport = new System.Windows.Forms.Button();
            this.dateCancelExport = new System.Windows.Forms.DateTimePicker();
            this.btnCancelExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data3)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data4)).BeginInit();
            this.SuspendLayout();
            // 
            // data
            // 
            this.data.AllowDrop = true;
            this.data.AllowUserToAddRows = false;
            this.data.AllowUserToDeleteRows = false;
            this.data.AllowUserToResizeColumns = false;
            this.data.AllowUserToResizeRows = false;
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data.Location = new System.Drawing.Point(8, 6);
            this.data.MultiSelect = false;
            this.data.Name = "data";
            this.data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.data.Size = new System.Drawing.Size(508, 390);
            this.data.TabIndex = 2;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.AutoSize = true;
            this.btnSelectAll.Location = new System.Drawing.Point(5, 487);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(72, 13);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.TabStop = true;
            this.btnSelectAll.Text = "Выбрать все";
            this.btnSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSelectAll_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(83, 487);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(96, 13);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Снять выделение";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(522, 6);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(115, 30);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Обновить список";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnLoadSelected
            // 
            this.btnLoadSelected.Location = new System.Drawing.Point(522, 42);
            this.btnLoadSelected.Name = "btnLoadSelected";
            this.btnLoadSelected.Size = new System.Drawing.Size(115, 49);
            this.btnLoadSelected.TabIndex = 6;
            this.btnLoadSelected.Text = "Загрузить выбранное";
            this.btnLoadSelected.UseVisualStyleBackColor = true;
            this.btnLoadSelected.Click += new System.EventHandler(this.btnLoadSelected_Click);
            // 
            // checkPrintCheck
            // 
            this.checkPrintCheck.Checked = true;
            this.checkPrintCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPrintCheck.Location = new System.Drawing.Point(522, 97);
            this.checkPrintCheck.Name = "checkPrintCheck";
            this.checkPrintCheck.Size = new System.Drawing.Size(115, 35);
            this.checkPrintCheck.TabIndex = 9;
            this.checkPrintCheck.Text = "Печатать чек при импорте";
            this.checkPrintCheck.UseVisualStyleBackColor = true;
            // 
            // rep
            // 
            this.rep.ReportDefinition = resources.GetString("rep.ReportDefinition");
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(522, 138);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(115, 23);
            this.pb.TabIndex = 10;
            // 
            // log
            // 
            this.log.FormattingEnabled = true;
            this.log.Location = new System.Drawing.Point(8, 402);
            this.log.Name = "log";
            this.log.ScrollAlwaysVisible = true;
            this.log.Size = new System.Drawing.Size(508, 82);
            this.log.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(651, 542);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.data);
            this.tabPage1.Controls.Add(this.log);
            this.tabPage1.Controls.Add(this.btnSelectAll);
            this.tabPage1.Controls.Add(this.pb);
            this.tabPage1.Controls.Add(this.linkLabel2);
            this.tabPage1.Controls.Add(this.checkPrintCheck);
            this.tabPage1.Controls.Add(this.btnLoadSelected);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(643, 516);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Импорт заказов";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pb2);
            this.tabPage2.Controls.Add(this.btnStart);
            this.tabPage2.Controls.Add(this.data2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(643, 516);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Доступность точек";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pb2
            // 
            this.pb2.Location = new System.Drawing.Point(142, 487);
            this.pb2.Name = "pb2";
            this.pb2.Size = new System.Drawing.Size(488, 23);
            this.pb2.TabIndex = 7;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 487);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(130, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Начать проверку";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // data2
            // 
            this.data2.AllowUserToAddRows = false;
            this.data2.AllowUserToDeleteRows = false;
            this.data2.AllowUserToOrderColumns = true;
            this.data2.AllowUserToResizeRows = false;
            this.data2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data2.Location = new System.Drawing.Point(6, 6);
            this.data2.Name = "data2";
            this.data2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.data2.Size = new System.Drawing.Size(624, 475);
            this.data2.TabIndex = 4;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnCancelExport);
            this.tabPage3.Controls.Add(this.dateCancelExport);
            this.tabPage3.Controls.Add(this.btnShowCancelExport);
            this.tabPage3.Controls.Add(this.btnUpdateSendOrder);
            this.tabPage3.Controls.Add(this.data3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(643, 516);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Заказы на отправку";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnUpdateSendOrder
            // 
            this.btnUpdateSendOrder.Location = new System.Drawing.Point(6, 487);
            this.btnUpdateSendOrder.Name = "btnUpdateSendOrder";
            this.btnUpdateSendOrder.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateSendOrder.TabIndex = 1;
            this.btnUpdateSendOrder.Text = "Обновить";
            this.btnUpdateSendOrder.UseVisualStyleBackColor = true;
            this.btnUpdateSendOrder.Click += new System.EventHandler(this.btnUpdateSendOrder_Click);
            // 
            // data3
            // 
            this.data3.AllowUserToAddRows = false;
            this.data3.AllowUserToDeleteRows = false;
            this.data3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data3.Location = new System.Drawing.Point(6, 3);
            this.data3.Name = "data3";
            this.data3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.data3.Size = new System.Drawing.Size(634, 478);
            this.data3.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.dateFilterStop1);
            this.tabPage4.Controls.Add(this.dateFilterStart1);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.data4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(643, 516);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Отправленные заказы";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 492);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "по";
            // 
            // dateFilterStop1
            // 
            this.dateFilterStop1.Location = new System.Drawing.Point(369, 488);
            this.dateFilterStop1.Name = "dateFilterStop1";
            this.dateFilterStop1.Size = new System.Drawing.Size(136, 20);
            this.dateFilterStop1.TabIndex = 16;
            // 
            // dateFilterStart1
            // 
            this.dateFilterStart1.Location = new System.Drawing.Point(202, 488);
            this.dateFilterStart1.Name = "dateFilterStart1";
            this.dateFilterStart1.Size = new System.Drawing.Size(136, 20);
            this.dateFilterStart1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 492);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Дата приема:  с";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 487);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Обновить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // data4
            // 
            this.data4.AllowUserToAddRows = false;
            this.data4.AllowUserToDeleteRows = false;
            this.data4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data4.Location = new System.Drawing.Point(6, 3);
            this.data4.Name = "data4";
            this.data4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.data4.Size = new System.Drawing.Size(634, 478);
            this.data4.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(588, 611);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnShowCancelExport
            // 
            this.btnShowCancelExport.Location = new System.Drawing.Point(612, 487);
            this.btnShowCancelExport.Name = "btnShowCancelExport";
            this.btnShowCancelExport.Size = new System.Drawing.Size(26, 23);
            this.btnShowCancelExport.TabIndex = 2;
            this.btnShowCancelExport.Text = "...";
            this.btnShowCancelExport.UseVisualStyleBackColor = true;
            this.btnShowCancelExport.Click += new System.EventHandler(this.btnShowCancelExport_Click);
            // 
            // dateCancelExport
            // 
            this.dateCancelExport.Location = new System.Drawing.Point(487, 490);
            this.dateCancelExport.Name = "dateCancelExport";
            this.dateCancelExport.Size = new System.Drawing.Size(150, 20);
            this.dateCancelExport.TabIndex = 3;
            this.dateCancelExport.Visible = false;
            // 
            // btnCancelExport
            // 
            this.btnCancelExport.Location = new System.Drawing.Point(229, 487);
            this.btnCancelExport.Name = "btnCancelExport";
            this.btnCancelExport.Size = new System.Drawing.Size(252, 23);
            this.btnCancelExport.TabIndex = 4;
            this.btnCancelExport.Text = "Отменить все экспорты до выбранной даты";
            this.btnCancelExport.UseVisualStyleBackColor = true;
            this.btnCancelExport.Visible = false;
            this.btnCancelExport.Click += new System.EventHandler(this.btnCancelExport_Click);
            // 
            // frmNewImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(675, 642);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmNewImport";
            this.Text = "                                  - PSA v.9.0.30729.1";
            this.Title = "                                    ";
            this.Load += new System.EventHandler(this.frmNewImport_Load);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView data;
        private System.Windows.Forms.LinkLabel btnSelectAll;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnLoadSelected;
        public System.Windows.Forms.CheckBox checkPrintCheck;
        private C1.Win.C1Report.C1Report rep;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.ListBox log;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ProgressBar pb2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView data2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnUpdateSendOrder;
        private System.Windows.Forms.DataGridView data3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateFilterStop1;
        private System.Windows.Forms.DateTimePicker dateFilterStart1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView data4;
        private System.Windows.Forms.Button btnCancelExport;
        private System.Windows.Forms.DateTimePicker dateCancelExport;
        private System.Windows.Forms.Button btnShowCancelExport;
    }
}
