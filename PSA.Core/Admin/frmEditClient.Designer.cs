namespace Photoland.Forms.Admin
{
	partial class frmEditClient
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
			this.checkDel = new System.Windows.Forms.CheckBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPhone1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPhone2 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtAddres = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtIcq = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtAddon = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtCategory = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// checkDel
			// 
			this.checkDel.AutoSize = true;
			this.checkDel.ForeColor = System.Drawing.Color.Red;
			this.checkDel.Location = new System.Drawing.Point(15, 220);
			this.checkDel.Name = "checkDel";
			this.checkDel.Size = new System.Drawing.Size(107, 17);
			this.checkDel.TabIndex = 22;
			this.checkDel.Text = "Запись удалена";
			this.checkDel.UseVisualStyleBackColor = true;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(126, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(174, 20);
			this.txtName.TabIndex = 20;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 19;
			this.label2.Text = "ФИО";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(225, 248);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(144, 248);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 17;
			this.btnOK.Text = "ОК";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 23;
			this.label1.Text = "Категория";
			// 
			// txtPhone1
			// 
			this.txtPhone1.Location = new System.Drawing.Point(126, 64);
			this.txtPhone1.Name = "txtPhone1";
			this.txtPhone1.Size = new System.Drawing.Size(174, 20);
			this.txtPhone1.TabIndex = 26;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 13);
			this.label3.TabIndex = 25;
			this.label3.Text = "Телефон";
			// 
			// txtPhone2
			// 
			this.txtPhone2.Location = new System.Drawing.Point(126, 90);
			this.txtPhone2.Name = "txtPhone2";
			this.txtPhone2.Size = new System.Drawing.Size(174, 20);
			this.txtPhone2.TabIndex = 28;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 93);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(79, 13);
			this.label4.TabIndex = 27;
			this.label4.Text = "Доп. Телефон";
			// 
			// txtAddres
			// 
			this.txtAddres.Location = new System.Drawing.Point(126, 116);
			this.txtAddres.Name = "txtAddres";
			this.txtAddres.Size = new System.Drawing.Size(174, 20);
			this.txtAddres.TabIndex = 30;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 119);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 29;
			this.label5.Text = "Адрес";
			// 
			// txtEmail
			// 
			this.txtEmail.Location = new System.Drawing.Point(126, 142);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(174, 20);
			this.txtEmail.TabIndex = 32;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 145);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 13);
			this.label6.TabIndex = 31;
			this.label6.Text = "E-Mail";
			// 
			// txtIcq
			// 
			this.txtIcq.Location = new System.Drawing.Point(126, 168);
			this.txtIcq.Name = "txtIcq";
			this.txtIcq.Size = new System.Drawing.Size(174, 20);
			this.txtIcq.TabIndex = 34;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 171);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(25, 13);
			this.label7.TabIndex = 33;
			this.label7.Text = "ICQ";
			// 
			// txtAddon
			// 
			this.txtAddon.Location = new System.Drawing.Point(126, 194);
			this.txtAddon.Name = "txtAddon";
			this.txtAddon.Size = new System.Drawing.Size(174, 20);
			this.txtAddon.TabIndex = 36;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(12, 197);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(73, 13);
			this.label8.TabIndex = 35;
			this.label8.Text = "Информация";
			// 
			// txtCategory
			// 
			this.txtCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtCategory.FormattingEnabled = true;
			this.txtCategory.Location = new System.Drawing.Point(126, 37);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size(174, 21);
			this.txtCategory.TabIndex = 37;
			// 
			// frmEditClient
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(312, 283);
			this.Controls.Add(this.txtCategory);
			this.Controls.Add(this.txtAddon);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.txtIcq);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.txtEmail);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtAddres);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtPhone2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPhone1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkDel);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmEditClient";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmEditClient";
			this.Load += new System.EventHandler(this.frmEditClient_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkDel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPhone1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtPhone2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtAddres;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtIcq;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtAddon;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox txtCategory;
		public System.Windows.Forms.TextBox txtName;
	}
}