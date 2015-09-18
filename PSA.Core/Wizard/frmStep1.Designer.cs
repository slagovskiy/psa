namespace Photoland.Acceptance.Wizard
{
	partial class frmStep1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStep1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioCrop3 = new System.Windows.Forms.RadioButton();
            this.radioCrop2 = new System.Windows.Forms.RadioButton();
            this.radioCrop1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioPapperType2 = new System.Windows.Forms.RadioButton();
            this.radioPapperType1 = new System.Windows.Forms.RadioButton();
            this.GridOrder = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridOrder)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-2, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(802, 110);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(546, 526);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpdate.Location = new System.Drawing.Point(425, 526);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(115, 30);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnNext.Enabled = false;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNext.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnNext.Location = new System.Drawing.Point(667, 526);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(115, 30);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Далее";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioCrop3);
            this.groupBox2.Controls.Add(this.radioCrop2);
            this.groupBox2.Controls.Add(this.radioCrop1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.groupBox2.Location = new System.Drawing.Point(384, 241);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 115);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Размер";
            // 
            // radioCrop3
            // 
            this.radioCrop3.AutoSize = true;
            this.radioCrop3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioCrop3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioCrop3.Location = new System.Drawing.Point(6, 79);
            this.radioCrop3.Name = "radioCrop3";
            this.radioCrop3.Size = new System.Drawing.Size(171, 23);
            this.radioCrop3.TabIndex = 2;
            this.radioCrop3.TabStop = true;
            this.radioCrop3.Text = "Реальный размер";
            this.radioCrop3.UseVisualStyleBackColor = true;
            this.radioCrop3.CheckedChanged += new System.EventHandler(this.radioCrop3_CheckedChanged);
            // 
            // radioCrop2
            // 
            this.radioCrop2.AutoSize = true;
            this.radioCrop2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioCrop2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioCrop2.Location = new System.Drawing.Point(6, 50);
            this.radioCrop2.Name = "radioCrop2";
            this.radioCrop2.Size = new System.Drawing.Size(209, 23);
            this.radioCrop2.TabIndex = 1;
            this.radioCrop2.TabStop = true;
            this.radioCrop2.Text = "Сохранить пропорции";
            this.radioCrop2.UseVisualStyleBackColor = true;
            this.radioCrop2.CheckedChanged += new System.EventHandler(this.radioCrop2_CheckedChanged);
            // 
            // radioCrop1
            // 
            this.radioCrop1.AutoSize = true;
            this.radioCrop1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioCrop1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioCrop1.Location = new System.Drawing.Point(6, 21);
            this.radioCrop1.Name = "radioCrop1";
            this.radioCrop1.Size = new System.Drawing.Size(203, 23);
            this.radioCrop1.TabIndex = 0;
            this.radioCrop1.TabStop = true;
            this.radioCrop1.Text = "Обрезать под формат";
            this.radioCrop1.UseVisualStyleBackColor = true;
            this.radioCrop1.CheckedChanged += new System.EventHandler(this.radioCrop1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioPapperType2);
            this.groupBox1.Controls.Add(this.radioPapperType1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.groupBox1.Location = new System.Drawing.Point(384, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 87);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип бумаги";
            // 
            // radioPapperType2
            // 
            this.radioPapperType2.AutoSize = true;
            this.radioPapperType2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioPapperType2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioPapperType2.Location = new System.Drawing.Point(6, 50);
            this.radioPapperType2.Name = "radioPapperType2";
            this.radioPapperType2.Size = new System.Drawing.Size(95, 23);
            this.radioPapperType2.TabIndex = 1;
            this.radioPapperType2.TabStop = true;
            this.radioPapperType2.Text = "Матовая";
            this.radioPapperType2.UseVisualStyleBackColor = true;
            this.radioPapperType2.CheckedChanged += new System.EventHandler(this.radioPapperType2_CheckedChanged);
            // 
            // radioPapperType1
            // 
            this.radioPapperType1.AutoSize = true;
            this.radioPapperType1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioPapperType1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioPapperType1.Location = new System.Drawing.Point(6, 21);
            this.radioPapperType1.Name = "radioPapperType1";
            this.radioPapperType1.Size = new System.Drawing.Size(112, 23);
            this.radioPapperType1.TabIndex = 0;
            this.radioPapperType1.TabStop = true;
            this.radioPapperType1.Text = "Глянцевая";
            this.radioPapperType1.UseVisualStyleBackColor = true;
            this.radioPapperType1.CheckedChanged += new System.EventHandler(this.radioPapperType1_CheckedChanged);
            // 
            // GridOrder
            // 
            this.GridOrder.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.GridOrder.BackColor = System.Drawing.Color.DarkGray;
            this.GridOrder.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D;
            this.GridOrder.ColumnInfo = resources.GetString("GridOrder.ColumnInfo");
            this.GridOrder.Location = new System.Drawing.Point(12, 117);
            this.GridOrder.Name = "GridOrder";
            this.GridOrder.Rows.Count = 10;
            this.GridOrder.Rows.DefaultSize = 17;
            this.GridOrder.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.GridOrder.Size = new System.Drawing.Size(363, 439);
            this.GridOrder.StyleInfo = resources.GetString("GridOrder.StyleInfo");
            this.GridOrder.TabIndex = 7;
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblOrderNo.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblOrderNo.Location = new System.Drawing.Point(495, 115);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(125, 30);
            this.lblOrderNo.TabIndex = 1;
            this.lblOrderNo.Text = "00000000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(381, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 27);
            this.label1.TabIndex = 0;
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
            this.groupBox3.TabIndex = 8;
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
            // tmr
            // 
            this.tmr.Interval = 5000;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // frmStep1
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(794, 568);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GridOrder);
            this.Controls.Add(this.lblOrderNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmStep1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmStep1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridOrder)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.GroupBox groupBox2;
		public System.Windows.Forms.RadioButton radioCrop3;
		public System.Windows.Forms.RadioButton radioCrop2;
		public System.Windows.Forms.RadioButton radioCrop1;
		private System.Windows.Forms.GroupBox groupBox1;
		public System.Windows.Forms.RadioButton radioPapperType2;
		public System.Windows.Forms.RadioButton radioPapperType1;
		public C1.Win.C1FlexGrid.C1FlexGrid GridOrder;
		public System.Windows.Forms.Label lblOrderNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label lblUserInfo;
		public System.Windows.Forms.Timer tmr;
	}
}

