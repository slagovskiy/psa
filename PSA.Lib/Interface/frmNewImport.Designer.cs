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
            this.btnClose = new System.Windows.Forms.Button();
            this.checkPrintCheck = new System.Windows.Forms.CheckBox();
            this.rep = new C1.Win.C1Report.C1Report();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.log = new System.Windows.Forms.ListBox();
            this.btnCheckPionts = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep)).BeginInit();
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
            this.data.Location = new System.Drawing.Point(12, 63);
            this.data.MultiSelect = false;
            this.data.Name = "data";
            this.data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.data.Size = new System.Drawing.Size(480, 390);
            this.data.TabIndex = 2;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.AutoSize = true;
            this.btnSelectAll.Location = new System.Drawing.Point(9, 544);
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
            this.linkLabel2.Location = new System.Drawing.Point(87, 544);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(96, 13);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Снять выделение";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(498, 63);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(115, 30);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Обновить список";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnLoadSelected
            // 
            this.btnLoadSelected.Location = new System.Drawing.Point(498, 99);
            this.btnLoadSelected.Name = "btnLoadSelected";
            this.btnLoadSelected.Size = new System.Drawing.Size(115, 49);
            this.btnLoadSelected.TabIndex = 6;
            this.btnLoadSelected.Text = "Загрузить выбранное";
            this.btnLoadSelected.UseVisualStyleBackColor = true;
            this.btnLoadSelected.Click += new System.EventHandler(this.btnLoadSelected_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(498, 262);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 30);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // checkPrintCheck
            // 
            this.checkPrintCheck.Checked = true;
            this.checkPrintCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkPrintCheck.Location = new System.Drawing.Point(498, 154);
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
            this.pb.Location = new System.Drawing.Point(498, 195);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(115, 23);
            this.pb.TabIndex = 10;
            // 
            // log
            // 
            this.log.FormattingEnabled = true;
            this.log.Location = new System.Drawing.Point(12, 459);
            this.log.Name = "log";
            this.log.ScrollAlwaysVisible = true;
            this.log.Size = new System.Drawing.Size(480, 82);
            this.log.TabIndex = 11;
            // 
            // btnCheckPionts
            // 
            this.btnCheckPionts.Location = new System.Drawing.Point(498, 298);
            this.btnCheckPionts.Name = "btnCheckPionts";
            this.btnCheckPionts.Size = new System.Drawing.Size(115, 49);
            this.btnCheckPionts.TabIndex = 12;
            this.btnCheckPionts.Text = "Проверить доступность точек";
            this.btnCheckPionts.UseVisualStyleBackColor = true;
            this.btnCheckPionts.Click += new System.EventHandler(this.btnCheckPionts_Click);
            // 
            // frmNewImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(625, 566);
            this.Controls.Add(this.btnCheckPionts);
            this.Controls.Add(this.log);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.checkPrintCheck);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLoadSelected);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmNewImport";
            this.Text = "                - PSA v.9.0.30729.1";
            this.Title = "                  ";
            this.Load += new System.EventHandler(this.frmNewImport_Load);
            this.Controls.SetChildIndex(this.data, 0);
            this.Controls.SetChildIndex(this.btnSelectAll, 0);
            this.Controls.SetChildIndex(this.linkLabel2, 0);
            this.Controls.SetChildIndex(this.btnUpdate, 0);
            this.Controls.SetChildIndex(this.btnLoadSelected, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.checkPrintCheck, 0);
            this.Controls.SetChildIndex(this.pb, 0);
            this.Controls.SetChildIndex(this.log, 0);
            this.Controls.SetChildIndex(this.btnCheckPionts, 0);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView data;
        private System.Windows.Forms.LinkLabel btnSelectAll;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnLoadSelected;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.CheckBox checkPrintCheck;
        private C1.Win.C1Report.C1Report rep;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.ListBox log;
        private System.Windows.Forms.Button btnCheckPionts;
    }
}
