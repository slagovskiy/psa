namespace Photoland.Forms.Admin
{
	partial class frmEditCategory
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkInput = new System.Windows.Forms.CheckBox();
			this.checkDel = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(229, 87);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(148, 87);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "ОК";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(126, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(174, 20);
			this.txtName.TabIndex = 14;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Наименование";
			// 
			// checkInput
			// 
			this.checkInput.AutoSize = true;
			this.checkInput.Location = new System.Drawing.Point(12, 38);
			this.checkInput.Name = "checkInput";
			this.checkInput.Size = new System.Drawing.Size(292, 17);
			this.checkInput.TabIndex = 15;
			this.checkInput.Text = "Приемка может задавать эту категорию в клиентах";
			this.checkInput.UseVisualStyleBackColor = true;
			// 
			// checkDel
			// 
			this.checkDel.AutoSize = true;
			this.checkDel.ForeColor = System.Drawing.Color.Red;
			this.checkDel.Location = new System.Drawing.Point(12, 61);
			this.checkDel.Name = "checkDel";
			this.checkDel.Size = new System.Drawing.Size(107, 17);
			this.checkDel.TabIndex = 16;
			this.checkDel.Text = "Запись удалена";
			this.checkDel.UseVisualStyleBackColor = true;
			// 
			// frmEditCategory
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(312, 122);
			this.Controls.Add(this.checkDel);
			this.Controls.Add(this.checkInput);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmEditCategory";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmEditCategory";
			this.Load += new System.EventHandler(this.frmEditCategory_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkInput;
		private System.Windows.Forms.CheckBox checkDel;
	}
}