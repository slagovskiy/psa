namespace PSA.Lib.Interface
{
	partial class frmInventoryDoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventoryDoc));
            this.btnDoScan = new System.Windows.Forms.Button();
            this.data = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.lstScan = new System.Windows.Forms.ListBox();
            this.pb1 = new System.Windows.Forms.ProgressBar();
            this.lblLoad = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCloseInvent = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkExport = new System.Windows.Forms.CheckBox();
            this.radioNotFound = new System.Windows.Forms.CheckBox();
            this.radioFound = new System.Windows.Forms.CheckBox();
            this.btnFilterDeSelectAll = new System.Windows.Forms.LinkLabel();
            this.btnFilterSelectAll = new System.Windows.Forms.LinkLabel();
            this.checkStatus = new System.Windows.Forms.CheckedListBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpenOrder = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSave = new System.Windows.Forms.Label();
            this.checkSovpOk = new System.Windows.Forms.CheckBox();
            this.checkSovp = new System.Windows.Forms.CheckBox();
            this.radioBase = new System.Windows.Forms.RadioButton();
            this.radioScan = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDoScan
            // 
            this.btnDoScan.Location = new System.Drawing.Point(6, 96);
            this.btnDoScan.Name = "btnDoScan";
            this.btnDoScan.Size = new System.Drawing.Size(150, 40);
            this.btnDoScan.TabIndex = 2;
            this.btnDoScan.Text = "Сканирование заказов";
            this.btnDoScan.UseVisualStyleBackColor = true;
            this.btnDoScan.Click += new System.EventHandler(this.btnDoScan_Click);
            // 
            // data
            // 
            this.data.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.data.AllowEditing = false;
            this.data.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this.data.ColumnInfo = resources.GetString("data.ColumnInfo");
            this.data.Location = new System.Drawing.Point(12, 63);
            this.data.Name = "data";
            this.data.Rows.DefaultSize = 17;
            this.data.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.data.Size = new System.Drawing.Size(770, 241);
            this.data.StyleInfo = resources.GetString("data.StyleInfo");
            this.data.TabIndex = 3;
            this.data.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.data_AfterSort);
            this.data.GridChanged += new C1.Win.C1FlexGrid.GridChangedEventHandler(this.data_GridChanged);
            this.data.DoubleClick += new System.EventHandler(this.data_DoubleClick);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(6, 19);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(150, 40);
            this.btnLoadData.TabIndex = 4;
            this.btnLoadData.Text = "Сверка данных";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // lstScan
            // 
            this.lstScan.FormattingEnabled = true;
            this.lstScan.Location = new System.Drawing.Point(6, 19);
            this.lstScan.Name = "lstScan";
            this.lstScan.Size = new System.Drawing.Size(333, 69);
            this.lstScan.TabIndex = 5;
            // 
            // pb1
            // 
            this.pb1.Location = new System.Drawing.Point(12, 543);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(345, 20);
            this.pb1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb1.TabIndex = 7;
            this.pb1.Click += new System.EventHandler(this.pb1_Click);
            // 
            // lblLoad
            // 
            this.lblLoad.BackColor = System.Drawing.Color.LightBlue;
            this.lblLoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoad.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoad.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblLoad.Location = new System.Drawing.Point(326, 160);
            this.lblLoad.Name = "lblLoad";
            this.lblLoad.Size = new System.Drawing.Size(165, 46);
            this.lblLoad.TabIndex = 8;
            this.lblLoad.Text = "Загрузка...";
            this.lblLoad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoad.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteSelected);
            this.groupBox1.Controls.Add(this.btnDoScan);
            this.groupBox1.Controls.Add(this.lstScan);
            this.groupBox1.Location = new System.Drawing.Point(12, 310);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 145);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Шаг 1";
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Location = new System.Drawing.Point(162, 96);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(150, 40);
            this.btnDeleteSelected.TabIndex = 6;
            this.btnDeleteSelected.Text = "Удалить выбранный";
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoadData);
            this.groupBox2.Location = new System.Drawing.Point(12, 461);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 71);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Шаг 2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCloseInvent);
            this.groupBox3.Location = new System.Drawing.Point(188, 464);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(169, 68);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Шаг 3";
            // 
            // btnCloseInvent
            // 
            this.btnCloseInvent.Location = new System.Drawing.Point(6, 19);
            this.btnCloseInvent.Name = "btnCloseInvent";
            this.btnCloseInvent.Size = new System.Drawing.Size(150, 40);
            this.btnCloseInvent.TabIndex = 5;
            this.btnCloseInvent.Text = "Закрытие инвентаризации";
            this.btnCloseInvent.UseVisualStyleBackColor = true;
            this.btnCloseInvent.Click += new System.EventHandler(this.btnCloseInvent_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioScan);
            this.groupBox4.Controls.Add(this.radioBase);
            this.groupBox4.Controls.Add(this.checkSovp);
            this.groupBox4.Controls.Add(this.checkSovpOk);
            this.groupBox4.Controls.Add(this.checkExport);
            this.groupBox4.Controls.Add(this.radioNotFound);
            this.groupBox4.Controls.Add(this.radioFound);
            this.groupBox4.Controls.Add(this.btnFilterDeSelectAll);
            this.groupBox4.Controls.Add(this.btnFilterSelectAll);
            this.groupBox4.Controls.Add(this.checkStatus);
            this.groupBox4.Controls.Add(this.btnApply);
            this.groupBox4.Location = new System.Drawing.Point(363, 310);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(419, 171);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Фильтры статусов";
            // 
            // checkExport
            // 
            this.checkExport.AutoSize = true;
            this.checkExport.Location = new System.Drawing.Point(289, 65);
            this.checkExport.Name = "checkExport";
            this.checkExport.Size = new System.Drawing.Size(124, 17);
            this.checkExport.TabIndex = 22;
            this.checkExport.Text = "Экспортированные";
            this.checkExport.UseVisualStyleBackColor = true;
            // 
            // radioNotFound
            // 
            this.radioNotFound.AutoSize = true;
            this.radioNotFound.Checked = true;
            this.radioNotFound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radioNotFound.Location = new System.Drawing.Point(289, 42);
            this.radioNotFound.Name = "radioNotFound";
            this.radioNotFound.Size = new System.Drawing.Size(99, 17);
            this.radioNotFound.TabIndex = 21;
            this.radioNotFound.Text = "Не найденные";
            this.radioNotFound.UseVisualStyleBackColor = true;
            // 
            // radioFound
            // 
            this.radioFound.AutoSize = true;
            this.radioFound.Checked = true;
            this.radioFound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radioFound.Location = new System.Drawing.Point(289, 19);
            this.radioFound.Name = "radioFound";
            this.radioFound.Size = new System.Drawing.Size(84, 17);
            this.radioFound.TabIndex = 20;
            this.radioFound.Text = "Найденные";
            this.radioFound.UseVisualStyleBackColor = true;
            // 
            // btnFilterDeSelectAll
            // 
            this.btnFilterDeSelectAll.AutoSize = true;
            this.btnFilterDeSelectAll.Location = new System.Drawing.Point(88, 144);
            this.btnFilterDeSelectAll.Name = "btnFilterDeSelectAll";
            this.btnFilterDeSelectAll.Size = new System.Drawing.Size(123, 13);
            this.btnFilterDeSelectAll.TabIndex = 19;
            this.btnFilterDeSelectAll.TabStop = true;
            this.btnFilterDeSelectAll.Text = "Снять отметки со всех";
            this.btnFilterDeSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnFilterDeSelectAll_LinkClicked);
            // 
            // btnFilterSelectAll
            // 
            this.btnFilterSelectAll.AutoSize = true;
            this.btnFilterSelectAll.Location = new System.Drawing.Point(5, 144);
            this.btnFilterSelectAll.Name = "btnFilterSelectAll";
            this.btnFilterSelectAll.Size = new System.Drawing.Size(77, 13);
            this.btnFilterSelectAll.TabIndex = 18;
            this.btnFilterSelectAll.TabStop = true;
            this.btnFilterSelectAll.Text = "Отметить все";
            this.btnFilterSelectAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnFilterSelectAll_LinkClicked);
            // 
            // checkStatus
            // 
            this.checkStatus.FormattingEnabled = true;
            this.checkStatus.Location = new System.Drawing.Point(8, 19);
            this.checkStatus.Name = "checkStatus";
            this.checkStatus.Size = new System.Drawing.Size(275, 94);
            this.checkStatus.TabIndex = 13;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(289, 135);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(124, 30);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnOpenOrder);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.Location = new System.Drawing.Point(363, 487);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(419, 76);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Действия";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(263, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 25);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpenOrder
            // 
            this.btnOpenOrder.Location = new System.Drawing.Point(6, 15);
            this.btnOpenOrder.Name = "btnOpenOrder";
            this.btnOpenOrder.Size = new System.Drawing.Size(150, 30);
            this.btnOpenOrder.TabIndex = 3;
            this.btnOpenOrder.Text = "Открыть заказ";
            this.btnOpenOrder.UseVisualStyleBackColor = true;
            this.btnOpenOrder.Click += new System.EventHandler(this.btnOpenOrder_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(263, 43);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 25);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblSave
            // 
            this.lblSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSave.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSave.Location = new System.Drawing.Point(326, 160);
            this.lblSave.Name = "lblSave";
            this.lblSave.Size = new System.Drawing.Size(165, 46);
            this.lblSave.TabIndex = 15;
            this.lblSave.Text = "Сохранение";
            this.lblSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSave.Visible = false;
            // 
            // checkSovpOk
            // 
            this.checkSovpOk.AutoSize = true;
            this.checkSovpOk.Checked = true;
            this.checkSovpOk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSovpOk.Location = new System.Drawing.Point(289, 88);
            this.checkSovpOk.Name = "checkSovpOk";
            this.checkSovpOk.Size = new System.Drawing.Size(99, 17);
            this.checkSovpOk.TabIndex = 23;
            this.checkSovpOk.Text = "Статус совпал";
            this.checkSovpOk.UseVisualStyleBackColor = true;
            // 
            // checkSovp
            // 
            this.checkSovp.AutoSize = true;
            this.checkSovp.Checked = true;
            this.checkSovp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSovp.Location = new System.Drawing.Point(289, 111);
            this.checkSovp.Name = "checkSovp";
            this.checkSovp.Size = new System.Drawing.Size(114, 17);
            this.checkSovp.TabIndex = 24;
            this.checkSovp.Text = "Статус не совпал";
            this.checkSovp.UseVisualStyleBackColor = true;
            // 
            // radioBase
            // 
            this.radioBase.AutoSize = true;
            this.radioBase.Checked = true;
            this.radioBase.Location = new System.Drawing.Point(8, 119);
            this.radioBase.Name = "radioBase";
            this.radioBase.Size = new System.Drawing.Size(92, 17);
            this.radioBase.TabIndex = 25;
            this.radioBase.TabStop = true;
            this.radioBase.Text = "Статус (база)";
            this.radioBase.UseVisualStyleBackColor = true;
            // 
            // radioScan
            // 
            this.radioScan.AutoSize = true;
            this.radioScan.Location = new System.Drawing.Point(106, 119);
            this.radioScan.Name = "radioScan";
            this.radioScan.Size = new System.Drawing.Size(92, 17);
            this.radioScan.TabIndex = 26;
            this.radioScan.Text = "Статус (скан)";
            this.radioScan.UseVisualStyleBackColor = true;
            // 
            // frmInventoryDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(794, 575);
            this.ControlBox = false;
            this.Controls.Add(this.lblSave);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.pb1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblLoad);
            this.Controls.Add(this.data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmInventoryDoc";
            this.Load += new System.EventHandler(this.frmInventoryDoc_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInventoryDoc_FormClosing);
            this.Controls.SetChildIndex(this.data, 0);
            this.Controls.SetChildIndex(this.lblLoad, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.pb1, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.groupBox5, 0);
            this.Controls.SetChildIndex(this.lblSave, 0);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnDoScan;
		private C1.Win.C1FlexGrid.C1FlexGrid data;
		private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.ListBox lstScan;
		private System.Windows.Forms.ProgressBar pb1;
        private System.Windows.Forms.Label lblLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCloseInvent;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckedListBox checkStatus;
        private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel btnFilterDeSelectAll;
        private System.Windows.Forms.LinkLabel btnFilterSelectAll;
        private System.Windows.Forms.Button btnOpenOrder;
        private System.Windows.Forms.Label lblSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.CheckBox radioFound;
		private System.Windows.Forms.CheckBox checkExport;
		private System.Windows.Forms.CheckBox radioNotFound;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.RadioButton radioScan;
        private System.Windows.Forms.RadioButton radioBase;
        private System.Windows.Forms.CheckBox checkSovp;
        private System.Windows.Forms.CheckBox checkSovpOk;
	}
}
