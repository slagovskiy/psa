namespace PSA.Lib.Interface
{
	partial class frmGetBarCodeEdit
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
			this.txtBarCode = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtBarCode
			// 
			this.txtBarCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtBarCode.Location = new System.Drawing.Point(12, 12);
			this.txtBarCode.Name = "txtBarCode";
			this.txtBarCode.Size = new System.Drawing.Size(236, 38);
			this.txtBarCode.TabIndex = 2;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(12, 56);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(236, 42);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// frmGetBarCodeEdit
			// 
			this.AcceptButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(260, 108);
			this.Controls.Add(this.txtBarCode);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Name = "frmGetBarCodeEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Номер заказа";
			this.Load += new System.EventHandler(this.frmGetBarCodeEdit_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmGetBarCodeEdit_KeyPress);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtBarCode;
		private System.Windows.Forms.Button btnOk;
	}
}
