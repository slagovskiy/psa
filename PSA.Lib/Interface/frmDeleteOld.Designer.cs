namespace PSA.Lib.Interface
{
    partial class frmDeleteOld
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeleteOld));
			this.data = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtStatus = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.btnApply = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.txtDateFiler = new System.Windows.Forms.DateTimePicker();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblAction = new System.Windows.Forms.Label();
			this.pb = new System.Windows.Forms.ProgressBar();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnDeleteSelected = new System.Windows.Forms.Button();
			this.btnOpenOrder = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// data
			// 
			this.data.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
			this.data.AllowEditing = false;
			this.data.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
			this.data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.data.ColumnInfo = "1,1,0,0,0,85,Columns:0{Width:20;}\t";
			this.data.Location = new System.Drawing.Point(3, 63);
			this.data.Name = "data";
			this.data.Rows.DefaultSize = 17;
			this.data.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.data.Size = new System.Drawing.Size(786, 362);
			this.data.StyleInfo = resources.GetString("data.StyleInfo");
			this.data.TabIndex = 5;
			this.data.Click += new System.EventHandler(this.data_Click);
			this.data.DoubleClick += new System.EventHandler(this.data_DoubleClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.txtStatus);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.linkLabel2);
			this.groupBox1.Controls.Add(this.btnApply);
			this.groupBox1.Controls.Add(this.linkLabel1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtDateFiler);
			this.groupBox1.Location = new System.Drawing.Point(12, 446);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(376, 108);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Фильтр";
			// 
			// txtStatus
			// 
			this.txtStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtStatus.FormattingEnabled = true;
			this.txtStatus.Location = new System.Drawing.Point(101, 57);
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.Size = new System.Drawing.Size(269, 21);
			this.txtStatus.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "В статусе";
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(6, 83);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(103, 13);
			this.linkLabel2.TabIndex = 6;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Снять все отметки";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(250, 19);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(120, 30);
			this.btnApply.TabIndex = 3;
			this.btnApply.Text = "Применить";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(115, 83);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(77, 13);
			this.linkLabel1.TabIndex = 5;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Отметить все";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(89, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Заказы до даты";
			// 
			// txtDateFiler
			// 
			this.txtDateFiler.Location = new System.Drawing.Point(101, 24);
			this.txtDateFiler.Name = "txtDateFiler";
			this.txtDateFiler.Size = new System.Drawing.Size(132, 20);
			this.txtDateFiler.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.lblAction);
			this.groupBox2.Controls.Add(this.pb);
			this.groupBox2.Controls.Add(this.btnClose);
			this.groupBox2.Controls.Add(this.btnDeleteSelected);
			this.groupBox2.Controls.Add(this.btnOpenOrder);
			this.groupBox2.Location = new System.Drawing.Point(394, 446);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(386, 108);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Действия";
			// 
			// lblAction
			// 
			this.lblAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblAction.Location = new System.Drawing.Point(3, 53);
			this.lblAction.Name = "lblAction";
			this.lblAction.Size = new System.Drawing.Size(375, 27);
			this.lblAction.TabIndex = 4;
			// 
			// pb
			// 
			this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pb.Location = new System.Drawing.Point(9, 83);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(369, 19);
			this.pb.TabIndex = 3;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(258, 20);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(120, 30);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnDeleteSelected
			// 
			this.btnDeleteSelected.Location = new System.Drawing.Point(132, 20);
			this.btnDeleteSelected.Name = "btnDeleteSelected";
			this.btnDeleteSelected.Size = new System.Drawing.Size(120, 30);
			this.btnDeleteSelected.TabIndex = 1;
			this.btnDeleteSelected.Text = "Удалить выбранные";
			this.btnDeleteSelected.UseVisualStyleBackColor = true;
			this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
			// 
			// btnOpenOrder
			// 
			this.btnOpenOrder.Location = new System.Drawing.Point(6, 19);
			this.btnOpenOrder.Name = "btnOpenOrder";
			this.btnOpenOrder.Size = new System.Drawing.Size(120, 30);
			this.btnOpenOrder.TabIndex = 0;
			this.btnOpenOrder.Text = "Открыть заказ";
			this.btnOpenOrder.UseVisualStyleBackColor = true;
			this.btnOpenOrder.Click += new System.EventHandler(this.btnOpenOrder_Click);
			// 
			// frmDeleteOld
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(792, 566);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.data);
			this.Name = "frmDeleteOld";
			this.Load += new System.EventHandler(this.frmDeleteOld_Load);
			this.Controls.SetChildIndex(this.data, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
			((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid data;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtDateFiler;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.Button btnOpenOrder;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.Label lblAction;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.ComboBox txtStatus;
		private System.Windows.Forms.Label label3;
    }
}
