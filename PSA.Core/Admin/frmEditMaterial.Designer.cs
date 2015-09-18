namespace Photoland.Forms.Admin
{
	partial class frmEditMaterial
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
			this.txtCount = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtCode1C
			// 
			this.txtCode1C.Location = new System.Drawing.Point(126, 12);
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
			this.checkDel.Location = new System.Drawing.Point(15, 90);
			this.checkDel.Name = "checkDel";
			this.checkDel.Size = new System.Drawing.Size(107, 17);
			this.checkDel.TabIndex = 39;
			this.checkDel.Text = "Запись удалена";
			this.checkDel.UseVisualStyleBackColor = true;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(126, 38);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(174, 20);
			this.txtName.TabIndex = 38;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 37;
			this.label2.Text = "Материал";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(225, 118);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 36;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(144, 118);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 35;
			this.btnOK.Text = "ОК";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtCount
			// 
			this.txtCount.Location = new System.Drawing.Point(126, 64);
			this.txtCount.Name = "txtCount";
			this.txtCount.Size = new System.Drawing.Size(174, 20);
			this.txtCount.TabIndex = 43;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(49, 13);
			this.label3.TabIndex = 42;
			this.label3.Text = "Остаток";
			// 
			// frmEditMaterial
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(312, 153);
			this.Controls.Add(this.txtCount);
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
			this.Name = "frmEditMaterial";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmEditMaterial";
			this.Load += new System.EventHandler(this.frmEditMaterial_Load);
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
		private System.Windows.Forms.TextBox txtCount;
		private System.Windows.Forms.Label label3;
	}
}