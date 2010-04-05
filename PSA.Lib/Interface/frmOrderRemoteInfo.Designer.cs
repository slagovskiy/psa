namespace PSA.Lib.Interface
{
	partial class frmOrderRemoteInfo
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
			this.lblInfo = new System.Windows.Forms.Label();
			this.data = new System.Windows.Forms.DataGridView();
			this.btnCloase = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
			this.SuspendLayout();
			// 
			// lblInfo
			// 
			this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblInfo.Location = new System.Drawing.Point(12, 60);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(608, 109);
			this.lblInfo.TabIndex = 2;
			// 
			// data
			// 
			this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.data.Location = new System.Drawing.Point(3, 172);
			this.data.Name = "data";
			this.data.Size = new System.Drawing.Size(626, 159);
			this.data.TabIndex = 3;
			// 
			// btnCloase
			// 
			this.btnCloase.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCloase.Location = new System.Drawing.Point(500, 337);
			this.btnCloase.Name = "btnCloase";
			this.btnCloase.Size = new System.Drawing.Size(120, 30);
			this.btnCloase.TabIndex = 4;
			this.btnCloase.Text = "Закрыть";
			this.btnCloase.UseVisualStyleBackColor = true;
			this.btnCloase.Click += new System.EventHandler(this.btnCloase_Click);
			// 
			// frmOrderRemoteInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.CancelButton = this.btnCloase;
			this.ClientSize = new System.Drawing.Size(632, 378);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.data);
			this.Controls.Add(this.btnCloase);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmOrderRemoteInfo";
			this.ShowInTaskbar = false;
			this.Text = "         Информация о заказе - PSA v.9.0.21022.8";
			this.Title = "            Информация о заказе";
			this.Controls.SetChildIndex(this.btnCloase, 0);
			this.Controls.SetChildIndex(this.data, 0);
			this.Controls.SetChildIndex(this.lblInfo, 0);
			((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.DataGridView data;
		private System.Windows.Forms.Button btnCloase;
	}
}
