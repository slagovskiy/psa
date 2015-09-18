namespace Photoland.Acceptance.Wizard
{
	partial class frmStep2
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStep2));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblOrderNo = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lblUserInfo = new System.Windows.Forms.Label();
			this.GridClient = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblcategory = new System.Windows.Forms.Label();
			this.lbladdon = new System.Windows.Forms.Label();
			this.lblicq = new System.Windows.Forms.Label();
			this.lblemail = new System.Windows.Forms.Label();
			this.lblAddress = new System.Windows.Forms.Label();
			this.lblPhone2 = new System.Windows.Forms.Label();
			this.lblPhone1 = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridClient)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(-2, -1);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(801, 110);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// btnNext
			// 
			this.btnNext.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnNext.Enabled = false;
			this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnNext.ForeColor = System.Drawing.Color.DarkGreen;
			this.btnNext.Location = new System.Drawing.Point(667, 526);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(115, 30);
			this.btnNext.TabIndex = 1;
			this.btnNext.Text = "Далее";
			this.btnNext.UseVisualStyleBackColor = false;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnBack
			// 
			this.btnBack.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnBack.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnBack.Location = new System.Drawing.Point(546, 526);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(115, 30);
			this.btnBack.TabIndex = 3;
			this.btnBack.Text = "Назад";
			this.btnBack.UseVisualStyleBackColor = false;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCancel.Location = new System.Drawing.Point(425, 526);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(115, 30);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblOrderNo
			// 
			this.lblOrderNo.AutoSize = true;
			this.lblOrderNo.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblOrderNo.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblOrderNo.Location = new System.Drawing.Point(495, 115);
			this.lblOrderNo.Name = "lblOrderNo";
			this.lblOrderNo.Size = new System.Drawing.Size(125, 30);
			this.lblOrderNo.TabIndex = 8;
			this.lblOrderNo.Text = "00000000";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(381, 117);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 27);
			this.label1.TabIndex = 7;
			this.label1.Text = "Заказ №";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.lblUserInfo);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox3.Location = new System.Drawing.Point(384, 425);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(400, 95);
			this.groupBox3.TabIndex = 10;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Вас обслуживает";
			// 
			// lblUserInfo
			// 
			this.lblUserInfo.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblUserInfo.Location = new System.Drawing.Point(8, 22);
			this.lblUserInfo.Name = "lblUserInfo";
			this.lblUserInfo.Size = new System.Drawing.Size(386, 70);
			this.lblUserInfo.TabIndex = 0;
			// 
			// GridClient
			// 
			this.GridClient.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
			this.GridClient.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
			this.GridClient.BackColor = System.Drawing.Color.Silver;
			this.GridClient.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
			this.GridClient.ColumnInfo = resources.GetString("GridClient.ColumnInfo");
			this.GridClient.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
			this.GridClient.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveDown;
			this.GridClient.Location = new System.Drawing.Point(12, 117);
			this.GridClient.Name = "GridClient";
			this.GridClient.Rows.Count = 10;
			this.GridClient.Rows.DefaultSize = 17;
			this.GridClient.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.GridClient.Size = new System.Drawing.Size(363, 439);
			this.GridClient.StyleInfo = resources.GetString("GridClient.StyleInfo");
			this.GridClient.TabIndex = 0;
			this.GridClient.AfterResizeColumn += new C1.Win.C1FlexGrid.RowColEventHandler(this.GridClient_AfterResizeColumn);
			this.GridClient.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GridClient_KeyPress);
			this.GridClient.DoubleClick += new System.EventHandler(this.GridClient_DoubleClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblcategory);
			this.groupBox1.Controls.Add(this.lbladdon);
			this.groupBox1.Controls.Add(this.lblicq);
			this.groupBox1.Controls.Add(this.lblemail);
			this.groupBox1.Controls.Add(this.lblAddress);
			this.groupBox1.Controls.Add(this.lblPhone2);
			this.groupBox1.Controls.Add(this.lblPhone1);
			this.groupBox1.Controls.Add(this.lblName);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.groupBox1.Location = new System.Drawing.Point(386, 148);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(396, 235);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Информация о клиенте";
			// 
			// lblcategory
			// 
			this.lblcategory.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblcategory.Location = new System.Drawing.Point(6, 189);
			this.lblcategory.Name = "lblcategory";
			this.lblcategory.Size = new System.Drawing.Size(381, 17);
			this.lblcategory.TabIndex = 7;
			this.lblcategory.Text = "Категория";
			// 
			// lbladdon
			// 
			this.lbladdon.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lbladdon.Location = new System.Drawing.Point(6, 151);
			this.lbladdon.Name = "lbladdon";
			this.lbladdon.Size = new System.Drawing.Size(381, 38);
			this.lbladdon.TabIndex = 6;
			this.lbladdon.Text = "Дополнительно";
			// 
			// lblicq
			// 
			this.lblicq.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblicq.Location = new System.Drawing.Point(6, 134);
			this.lblicq.Name = "lblicq";
			this.lblicq.Size = new System.Drawing.Size(381, 17);
			this.lblicq.TabIndex = 5;
			this.lblicq.Text = "ICQ";
			// 
			// lblemail
			// 
			this.lblemail.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblemail.Location = new System.Drawing.Point(6, 117);
			this.lblemail.Name = "lblemail";
			this.lblemail.Size = new System.Drawing.Size(381, 17);
			this.lblemail.TabIndex = 4;
			this.lblemail.Text = "Электронный адрес";
			// 
			// lblAddress
			// 
			this.lblAddress.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblAddress.Location = new System.Drawing.Point(6, 79);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(381, 38);
			this.lblAddress.TabIndex = 3;
			this.lblAddress.Text = "Адрес";
			// 
			// lblPhone2
			// 
			this.lblPhone2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPhone2.Location = new System.Drawing.Point(6, 62);
			this.lblPhone2.Name = "lblPhone2";
			this.lblPhone2.Size = new System.Drawing.Size(381, 17);
			this.lblPhone2.TabIndex = 2;
			this.lblPhone2.Text = "Доп телефон";
			// 
			// lblPhone1
			// 
			this.lblPhone1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPhone1.Location = new System.Drawing.Point(6, 45);
			this.lblPhone1.Name = "lblPhone1";
			this.lblPhone1.Size = new System.Drawing.Size(381, 17);
			this.lblPhone1.TabIndex = 1;
			this.lblPhone1.Text = "Телефон";
			// 
			// lblName
			// 
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblName.ForeColor = System.Drawing.Color.DarkBlue;
			this.lblName.Location = new System.Drawing.Point(6, 18);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(381, 27);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Имя";
			// 
			// btnEdit
			// 
			this.btnEdit.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnEdit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnEdit.Location = new System.Drawing.Point(507, 389);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(115, 30);
			this.btnEdit.TabIndex = 5;
			this.btnEdit.Text = "Изменить";
			this.btnEdit.UseVisualStyleBackColor = false;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnNew
			// 
			this.btnNew.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnNew.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnNew.Location = new System.Drawing.Point(386, 389);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(115, 30);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "Новый";
			this.btnNew.UseVisualStyleBackColor = false;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// btnClear
			// 
			this.btnClear.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnClear.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnClear.Location = new System.Drawing.Point(628, 389);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(115, 30);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Очистить";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnClear.Visible = false;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// frmStep2
			// 
			this.AcceptButton = this.btnNext;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 568);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.GridClient);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.lblOrderNo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frmStep2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmStep2";
			this.Load += new System.EventHandler(this.frmStep2_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GridClient)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.Label lblOrderNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label lblUserInfo;
		public C1.Win.C1FlexGrid.C1FlexGrid GridClient;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.Label lblPhone2;
		private System.Windows.Forms.Label lblPhone1;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lbladdon;
		private System.Windows.Forms.Label lblicq;
		private System.Windows.Forms.Label lblemail;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Label lblcategory;
	}
}