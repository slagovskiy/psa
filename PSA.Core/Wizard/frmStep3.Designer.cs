namespace Photoland.Acceptance.Wizard
{
	partial class frmStep3
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStep3));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnFinish = new System.Windows.Forms.Button();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cldrDate = new C1.Win.C1Schedule.C1Calendar();
			this.lblOrderNo = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lblUserInfo = new System.Windows.Forms.Label();
			this.gridTime = new C1.Win.C1FlexGrid.C1FlexGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblOrderDate = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.checkOpenOrder = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridTime)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(-3, -2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(801, 110);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// btnFinish
			// 
			this.btnFinish.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnFinish.ForeColor = System.Drawing.Color.DarkGreen;
			this.btnFinish.Location = new System.Drawing.Point(665, 524);
			this.btnFinish.Name = "btnFinish";
			this.btnFinish.Size = new System.Drawing.Size(115, 30);
			this.btnFinish.TabIndex = 0;
			this.btnFinish.Text = "Готово";
			this.btnFinish.UseVisualStyleBackColor = false;
			this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
			// 
			// btnBack
			// 
			this.btnBack.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnBack.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnBack.Location = new System.Drawing.Point(544, 524);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(115, 30);
			this.btnBack.TabIndex = 4;
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
			this.btnCancel.Location = new System.Drawing.Point(423, 524);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(115, 30);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// cldrDate
			// 
			this.cldrDate.BoldedDates = new System.DateTime[0];
			this.cldrDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.cldrDate.Location = new System.Drawing.Point(12, 116);
			this.cldrDate.Name = "cldrDate";
			this.cldrDate.Schedule = null;
			this.cldrDate.ShowWeekNumbers = true;
			this.cldrDate.Size = new System.Drawing.Size(362, 271);
			this.cldrDate.TabIndex = 1;
			// 
			// 
			// 
			this.cldrDate.Theme.Name = "Windows XP Blue (modified)";
			this.cldrDate.Theme.XmlDefinition = resources.GetString("resource.XmlDefinition");
			this.cldrDate.VisualStyle = C1.Win.C1Schedule.UI.VisualStyle.Custom;
			this.cldrDate.Click += new System.EventHandler(this.cldrDate_Click);
			// 
			// lblOrderNo
			// 
			this.lblOrderNo.AutoSize = true;
			this.lblOrderNo.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblOrderNo.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblOrderNo.Location = new System.Drawing.Point(494, 114);
			this.lblOrderNo.Name = "lblOrderNo";
			this.lblOrderNo.Size = new System.Drawing.Size(125, 30);
			this.lblOrderNo.TabIndex = 7;
			this.lblOrderNo.Text = "00000000";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(380, 116);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 27);
			this.label1.TabIndex = 6;
			this.label1.Text = "Заказ №";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.lblUserInfo);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox3.Location = new System.Drawing.Point(380, 423);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(400, 95);
			this.groupBox3.TabIndex = 9;
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
			// gridTime
			// 
			this.gridTime.BackColor = System.Drawing.Color.Silver;
			this.gridTime.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
			this.gridTime.ColumnInfo = resources.GetString("gridTime.ColumnInfo");
			this.gridTime.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
			this.gridTime.Location = new System.Drawing.Point(12, 393);
			this.gridTime.Name = "gridTime";
			this.gridTime.Rows.Count = 5;
			this.gridTime.Rows.DefaultSize = 17;
			this.gridTime.Rows.Fixed = 0;
			this.gridTime.Size = new System.Drawing.Size(362, 150);
			this.gridTime.StyleInfo = resources.GetString("gridTime.StyleInfo");
			this.gridTime.TabIndex = 2;
			this.gridTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridTime_KeyPress);
			this.gridTime.Click += new System.EventHandler(this.gridTime_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblOrderDate);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox1.Location = new System.Drawing.Point(380, 146);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(400, 133);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Время выдачи заказа";
			// 
			// lblOrderDate
			// 
			this.lblOrderDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblOrderDate.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblOrderDate.Location = new System.Drawing.Point(11, 46);
			this.lblOrderDate.Name = "lblOrderDate";
			this.lblOrderDate.Size = new System.Drawing.Size(383, 84);
			this.lblOrderDate.TabIndex = 1;
			this.lblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.label2.Location = new System.Drawing.Point(11, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(242, 24);
			this.label2.TabIndex = 0;
			this.label2.Text = "Ваш заказ будет готов:";
			// 
			// checkOpenOrder
			// 
			this.checkOpenOrder.AutoSize = true;
			this.checkOpenOrder.Location = new System.Drawing.Point(380, 400);
			this.checkOpenOrder.Name = "checkOpenOrder";
			this.checkOpenOrder.Size = new System.Drawing.Size(103, 17);
			this.checkOpenOrder.TabIndex = 3;
			this.checkOpenOrder.Text = "Открыть заказ";
			this.checkOpenOrder.UseVisualStyleBackColor = true;
			// 
			// frmStep3
			// 
			this.AcceptButton = this.btnFinish;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 566);
			this.Controls.Add(this.checkOpenOrder);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gridTime);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.lblOrderNo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cldrDate);
			this.Controls.Add(this.btnFinish);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frmStep3";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " ";
			this.Load += new System.EventHandler(this.frmStep3_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridTime)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnFinish;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.Label lblOrderNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label lblUserInfo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblOrderDate;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.CheckBox checkOpenOrder;
		public C1.Win.C1Schedule.C1Calendar cldrDate;
		public C1.Win.C1FlexGrid.C1FlexGrid gridTime;
	}
}