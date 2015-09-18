namespace Photoland.Forms.Admin
{
	partial class frmEditGood
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
            this.txtCode1C = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkDel = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSign = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.checkFolder = new System.Windows.Forms.CheckBox();
            this.txtEI = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtForm = new System.Windows.Forms.ComboBox();
            this.btnHelpByForm = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.checkZero = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtCode1C
            // 
            this.txtCode1C.Location = new System.Drawing.Point(126, 12);
            this.txtCode1C.MaxLength = 30;
            this.txtCode1C.Name = "txtCode1C";
            this.txtCode1C.Size = new System.Drawing.Size(174, 20);
            this.txtCode1C.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Код записи (1С)";
            // 
            // checkDel
            // 
            this.checkDel.AutoSize = true;
            this.checkDel.ForeColor = System.Drawing.Color.Red;
            this.checkDel.Location = new System.Drawing.Point(15, 359);
            this.checkDel.Name = "checkDel";
            this.checkDel.Size = new System.Drawing.Size(239, 17);
            this.checkDel.TabIndex = 39;
            this.checkDel.Text = "Запись удалена                                            ";
            this.checkDel.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(126, 38);
            this.txtName.MaxLength = 1024;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(174, 20);
            this.txtName.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Наименование";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(225, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(144, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(126, 64);
            this.txtDesc.MaxLength = 1024;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(174, 20);
            this.txtDesc.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Описание";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(126, 90);
            this.txtPrefix.MaxLength = 10;
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(174, 20);
            this.txtPrefix.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Префикс";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(126, 129);
            this.txtFolder.MaxLength = 255;
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(174, 20);
            this.txtFolder.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Папка";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "Тип";
            // 
            // txtSign
            // 
            this.txtSign.Location = new System.Drawing.Point(126, 195);
            this.txtSign.MaxLength = 255;
            this.txtSign.Name = "txtSign";
            this.txtSign.Size = new System.Drawing.Size(174, 20);
            this.txtSign.TabIndex = 51;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "Подпись";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Форма";
            // 
            // checkFolder
            // 
            this.checkFolder.AutoSize = true;
            this.checkFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkFolder.Location = new System.Drawing.Point(15, 313);
            this.checkFolder.Name = "checkFolder";
            this.checkFolder.Size = new System.Drawing.Size(275, 17);
            this.checkFolder.TabIndex = 54;
            this.checkFolder.Text = "Автоматически создавать папку для этой услуги";
            this.checkFolder.UseVisualStyleBackColor = true;
            // 
            // txtEI
            // 
            this.txtEI.Location = new System.Drawing.Point(126, 274);
            this.txtEI.Name = "txtEI";
            this.txtEI.Size = new System.Drawing.Size(174, 20);
            this.txtEI.TabIndex = 56;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 277);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 55;
            this.label9.Text = "Списание";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.ForeColor = System.Drawing.Color.DarkGray;
            this.label10.Location = new System.Drawing.Point(80, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(220, 13);
            this.label10.TabIndex = 57;
            this.label10.Text = "Префикс - Резерв (пока не используется)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.DarkGray;
            this.label11.Location = new System.Drawing.Point(31, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(269, 13);
            this.label11.TabIndex = 58;
            this.label11.Text = "Папка, в которую будут сложены файлы для услуги";
            // 
            // txtType
            // 
            this.txtType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtType.FormattingEnabled = true;
            this.txtType.Location = new System.Drawing.Point(126, 168);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(174, 21);
            this.txtType.TabIndex = 59;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.DarkGray;
            this.label12.Location = new System.Drawing.Point(45, 218);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(255, 13);
            this.label12.TabIndex = 60;
            this.label12.Text = "Подпись на кнопке быстрого добавления услуги";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.DarkGray;
            this.label13.Location = new System.Drawing.Point(93, 258);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(207, 13);
            this.label13.TabIndex = 61;
            this.label13.Text = "Форма применения услуги к объектам";
            // 
            // txtForm
            // 
            this.txtForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtForm.FormattingEnabled = true;
            this.txtForm.Location = new System.Drawing.Point(126, 234);
            this.txtForm.Name = "txtForm";
            this.txtForm.Size = new System.Drawing.Size(152, 21);
            this.txtForm.TabIndex = 62;
            // 
            // btnHelpByForm
            // 
            this.btnHelpByForm.Location = new System.Drawing.Point(279, 234);
            this.btnHelpByForm.Name = "btnHelpByForm";
            this.btnHelpByForm.Size = new System.Drawing.Size(21, 21);
            this.btnHelpByForm.TabIndex = 63;
            this.btnHelpByForm.Text = "?";
            this.btnHelpByForm.UseVisualStyleBackColor = true;
            this.btnHelpByForm.Click += new System.EventHandler(this.btnHelpByForm_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.DarkGray;
            this.label14.Location = new System.Drawing.Point(72, 297);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(228, 13);
            this.label14.TabIndex = 64;
            this.label14.Text = "Списание - объем для списания по нормам";
            // 
            // checkZero
            // 
            this.checkZero.AutoSize = true;
            this.checkZero.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkZero.Location = new System.Drawing.Point(15, 336);
            this.checkZero.Name = "checkZero";
            this.checkZero.Size = new System.Drawing.Size(212, 17);
            this.checkZero.TabIndex = 65;
            this.checkZero.Text = "Услуга может быть с нулевой ценой";
            this.checkZero.UseVisualStyleBackColor = true;
            // 
            // frmEditGood
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 425);
            this.Controls.Add(this.checkZero);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnHelpByForm);
            this.Controls.Add(this.txtForm);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtEI);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.checkFolder);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSign);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCode1C);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkDel);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditGood";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEditGood";
            this.Load += new System.EventHandler(this.frmEditGood_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtCode1C;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkDel;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtDesc;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtPrefix;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtFolder;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtSign;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox checkFolder;
		private System.Windows.Forms.TextBox txtEI;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox txtType;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ComboBox txtForm;
		private System.Windows.Forms.Button btnHelpByForm;
		private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkZero;
	}
}