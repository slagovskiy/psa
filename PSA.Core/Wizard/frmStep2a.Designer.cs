namespace Photoland.Acceptance.Wizard
{
	partial class frmStep2a
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStep2a));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblOrderNo = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lblUserInfo = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblFinalSum = new System.Windows.Forms.Label();
			this.lblFirstPay = new System.Windows.Forms.Label();
			this.lblOrderDiscont = new System.Windows.Forms.Label();
			this.lblOrderSum = new System.Windows.Forms.Label();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnDiscont = new System.Windows.Forms.Button();
			this.btnFirstPay = new System.Windows.Forms.Button();
			this.btnFirstPayClear = new System.Windows.Forms.Button();
			this.btnDiscontClear = new System.Windows.Forms.Button();
			this.btnFinalPaymentClear = new System.Windows.Forms.Button();
			this.btnFinalPayment = new System.Windows.Forms.Button();
			this.lblTotalPayment = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(-2, -1);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(801, 110);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// lblOrderNo
			// 
			this.lblOrderNo.AutoSize = true;
			this.lblOrderNo.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblOrderNo.ForeColor = System.Drawing.Color.MediumBlue;
			this.lblOrderNo.Location = new System.Drawing.Point(495, 115);
			this.lblOrderNo.Name = "lblOrderNo";
			this.lblOrderNo.Size = new System.Drawing.Size(125, 30);
			this.lblOrderNo.TabIndex = 10;
			this.lblOrderNo.Text = "00000000";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(381, 117);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 27);
			this.label1.TabIndex = 9;
			this.label1.Text = "Заказ №";
			// 
			// btnNext
			// 
			this.btnNext.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnNext.ForeColor = System.Drawing.Color.DarkGreen;
			this.btnNext.Location = new System.Drawing.Point(667, 526);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(115, 30);
			this.btnNext.TabIndex = 3;
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
			this.btnBack.TabIndex = 4;
			this.btnBack.Text = "Назад";
			this.btnBack.UseVisualStyleBackColor = false;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCancel.Location = new System.Drawing.Point(425, 526);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(115, 30);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.lblUserInfo);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.ForeColor = System.Drawing.SystemColors.GrayText;
			this.groupBox3.Location = new System.Drawing.Point(382, 425);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(400, 95);
			this.groupBox3.TabIndex = 14;
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.DarkBlue;
			this.label2.Location = new System.Drawing.Point(120, 162);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.label2.Size = new System.Drawing.Size(228, 50);
			this.label2.TabIndex = 15;
			this.label2.Text = "Общая стоимость заказа:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.Color.DarkBlue;
			this.label3.Location = new System.Drawing.Point(272, 262);
			this.label3.Name = "label3";
			this.label3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.label3.Size = new System.Drawing.Size(76, 50);
			this.label3.TabIndex = 16;
			this.label3.Text = "Скидка:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.ForeColor = System.Drawing.Color.DarkBlue;
			this.label4.Location = new System.Drawing.Point(228, 212);
			this.label4.Name = "label4";
			this.label4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.label4.Size = new System.Drawing.Size(120, 50);
			this.label4.TabIndex = 17;
			this.label4.Text = "Предоплата:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.ForeColor = System.Drawing.Color.DarkBlue;
			this.label5.Location = new System.Drawing.Point(257, 323);
			this.label5.Name = "label5";
			this.label5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.label5.Size = new System.Drawing.Size(91, 50);
			this.label5.TabIndex = 18;
			this.label5.Text = "К оплате:";
			// 
			// lblFinalSum
			// 
			this.lblFinalSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblFinalSum.ForeColor = System.Drawing.Color.DarkBlue;
			this.lblFinalSum.Location = new System.Drawing.Point(354, 320);
			this.lblFinalSum.Name = "lblFinalSum";
			this.lblFinalSum.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.lblFinalSum.Size = new System.Drawing.Size(135, 53);
			this.lblFinalSum.TabIndex = 22;
			this.lblFinalSum.Text = "0";
			this.lblFinalSum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblFirstPay
			// 
			this.lblFirstPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblFirstPay.ForeColor = System.Drawing.Color.DarkBlue;
			this.lblFirstPay.Location = new System.Drawing.Point(354, 209);
			this.lblFirstPay.Name = "lblFirstPay";
			this.lblFirstPay.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.lblFirstPay.Size = new System.Drawing.Size(135, 50);
			this.lblFirstPay.TabIndex = 21;
			this.lblFirstPay.Text = "0";
			this.lblFirstPay.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblOrderDiscont
			// 
			this.lblOrderDiscont.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblOrderDiscont.ForeColor = System.Drawing.Color.DarkBlue;
			this.lblOrderDiscont.Location = new System.Drawing.Point(354, 259);
			this.lblOrderDiscont.Name = "lblOrderDiscont";
			this.lblOrderDiscont.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.lblOrderDiscont.Size = new System.Drawing.Size(135, 53);
			this.lblOrderDiscont.TabIndex = 20;
			this.lblOrderDiscont.Text = "0";
			this.lblOrderDiscont.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblOrderSum
			// 
			this.lblOrderSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblOrderSum.ForeColor = System.Drawing.Color.DarkBlue;
			this.lblOrderSum.Location = new System.Drawing.Point(354, 159);
			this.lblOrderSum.Name = "lblOrderSum";
			this.lblOrderSum.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.lblOrderSum.Size = new System.Drawing.Size(135, 53);
			this.lblOrderSum.TabIndex = 19;
			this.lblOrderSum.Text = "0";
			this.lblOrderSum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnUpdate
			// 
			this.btnUpdate.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnUpdate.Location = new System.Drawing.Point(500, 162);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(75, 23);
			this.btnUpdate.TabIndex = 6;
			this.btnUpdate.Text = "Обновить";
			this.btnUpdate.UseVisualStyleBackColor = false;
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnDiscont
			// 
			this.btnDiscont.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnDiscont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDiscont.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDiscont.Location = new System.Drawing.Point(500, 262);
			this.btnDiscont.Name = "btnDiscont";
			this.btnDiscont.Size = new System.Drawing.Size(75, 23);
			this.btnDiscont.TabIndex = 1;
			this.btnDiscont.Text = "Выставить";
			this.btnDiscont.UseVisualStyleBackColor = false;
			this.btnDiscont.Click += new System.EventHandler(this.btnDiscont_Click);
			// 
			// btnFirstPay
			// 
			this.btnFirstPay.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnFirstPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFirstPay.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnFirstPay.Location = new System.Drawing.Point(500, 212);
			this.btnFirstPay.Name = "btnFirstPay";
			this.btnFirstPay.Size = new System.Drawing.Size(75, 23);
			this.btnFirstPay.TabIndex = 0;
			this.btnFirstPay.Text = "Внести";
			this.btnFirstPay.UseVisualStyleBackColor = false;
			this.btnFirstPay.Click += new System.EventHandler(this.btnFirstPay_Click);
			// 
			// btnFirstPayClear
			// 
			this.btnFirstPayClear.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnFirstPayClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFirstPayClear.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnFirstPayClear.Location = new System.Drawing.Point(581, 212);
			this.btnFirstPayClear.Name = "btnFirstPayClear";
			this.btnFirstPayClear.Size = new System.Drawing.Size(21, 23);
			this.btnFirstPayClear.TabIndex = 7;
			this.btnFirstPayClear.Text = "X";
			this.btnFirstPayClear.UseVisualStyleBackColor = false;
			this.btnFirstPayClear.Click += new System.EventHandler(this.btnFirstPayClear_Click);
			// 
			// btnDiscontClear
			// 
			this.btnDiscontClear.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnDiscontClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDiscontClear.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnDiscontClear.Location = new System.Drawing.Point(581, 262);
			this.btnDiscontClear.Name = "btnDiscontClear";
			this.btnDiscontClear.Size = new System.Drawing.Size(21, 23);
			this.btnDiscontClear.TabIndex = 8;
			this.btnDiscontClear.Text = "X";
			this.btnDiscontClear.UseVisualStyleBackColor = false;
			this.btnDiscontClear.Click += new System.EventHandler(this.btnDiscontClear_Click);
			// 
			// btnFinalPaymentClear
			// 
			this.btnFinalPaymentClear.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnFinalPaymentClear.Enabled = false;
			this.btnFinalPaymentClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFinalPaymentClear.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnFinalPaymentClear.Location = new System.Drawing.Point(581, 323);
			this.btnFinalPaymentClear.Name = "btnFinalPaymentClear";
			this.btnFinalPaymentClear.Size = new System.Drawing.Size(21, 23);
			this.btnFinalPaymentClear.TabIndex = 9;
			this.btnFinalPaymentClear.Text = "X";
			this.btnFinalPaymentClear.UseVisualStyleBackColor = false;
			this.btnFinalPaymentClear.Visible = false;
			this.btnFinalPaymentClear.Click += new System.EventHandler(this.btnFinalPaymentClear_Click);
			// 
			// btnFinalPayment
			// 
			this.btnFinalPayment.BackColor = System.Drawing.Color.LightGoldenrodYellow;
			this.btnFinalPayment.Enabled = false;
			this.btnFinalPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFinalPayment.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnFinalPayment.Location = new System.Drawing.Point(500, 323);
			this.btnFinalPayment.Name = "btnFinalPayment";
			this.btnFinalPayment.Size = new System.Drawing.Size(75, 23);
			this.btnFinalPayment.TabIndex = 2;
			this.btnFinalPayment.Text = "Оплатить";
			this.btnFinalPayment.UseVisualStyleBackColor = false;
			this.btnFinalPayment.Visible = false;
			this.btnFinalPayment.Click += new System.EventHandler(this.btnFinalPayment_Click);
			// 
			// lblTotalPayment
			// 
			this.lblTotalPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblTotalPayment.ForeColor = System.Drawing.Color.DarkBlue;
			this.lblTotalPayment.Location = new System.Drawing.Point(354, 370);
			this.lblTotalPayment.Name = "lblTotalPayment";
			this.lblTotalPayment.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.lblTotalPayment.Size = new System.Drawing.Size(135, 53);
			this.lblTotalPayment.TabIndex = 31;
			this.lblTotalPayment.Text = "0";
			this.lblTotalPayment.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.ForeColor = System.Drawing.Color.DarkBlue;
			this.label7.Location = new System.Drawing.Point(201, 373);
			this.label7.Name = "label7";
			this.label7.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
			this.label7.Size = new System.Drawing.Size(147, 50);
			this.label7.TabIndex = 30;
			this.label7.Text = "Всего получено:";
			// 
			// frmStep2a
			// 
			this.AcceptButton = this.btnNext;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 568);
			this.Controls.Add(this.lblTotalPayment);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnFinalPaymentClear);
			this.Controls.Add(this.btnFinalPayment);
			this.Controls.Add(this.btnDiscontClear);
			this.Controls.Add(this.btnFirstPayClear);
			this.Controls.Add(this.btnFirstPay);
			this.Controls.Add(this.btnDiscont);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.lblFinalSum);
			this.Controls.Add(this.lblFirstPay);
			this.Controls.Add(this.lblOrderDiscont);
			this.Controls.Add(this.lblOrderSum);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblOrderNo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "frmStep2a";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmStep2a";
			this.Load += new System.EventHandler(this.frmStep2a_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		public System.Windows.Forms.Label lblOrderNo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label lblUserInfo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblFinalSum;
		private System.Windows.Forms.Label lblFirstPay;
		private System.Windows.Forms.Label lblOrderDiscont;
		private System.Windows.Forms.Label lblOrderSum;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnDiscont;
		private System.Windows.Forms.Button btnFirstPay;
		private System.Windows.Forms.Button btnFirstPayClear;
		private System.Windows.Forms.Button btnDiscontClear;
		private System.Windows.Forms.Button btnFinalPaymentClear;
		private System.Windows.Forms.Button btnFinalPayment;
		private System.Windows.Forms.Label lblTotalPayment;
		private System.Windows.Forms.Label label7;
	}
}