namespace PSA.Lib.Interface
{
	partial class frmDialogYesNo
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
			this.btnYes = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.txtMessage = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// btnYes
			// 
			this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnYes.Location = new System.Drawing.Point(144, 174);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(115, 30);
			this.btnYes.TabIndex = 3;
			this.btnYes.Text = "ДА";
			this.btnYes.UseVisualStyleBackColor = true;
			// 
			// btnNo
			// 
			this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.btnNo.Location = new System.Drawing.Point(265, 174);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(115, 30);
			this.btnNo.TabIndex = 4;
			this.btnNo.Text = "Нет";
			this.btnNo.UseVisualStyleBackColor = true;
			// 
			// txtMessage
			// 
			this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMessage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtMessage.Location = new System.Drawing.Point(12, 74);
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ReadOnly = true;
			this.txtMessage.Size = new System.Drawing.Size(368, 94);
			this.txtMessage.TabIndex = 5;
			this.txtMessage.Text = "";
			// 
			// frmDialogYesNo
			// 
			this.AcceptButton = this.btnYes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.CancelButton = this.btnNo;
			this.ClientSize = new System.Drawing.Size(392, 216);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.btnYes);
			this.Controls.Add(this.btnNo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "frmDialogYesNo";
			this.Controls.SetChildIndex(this.btnNo, 0);
			this.Controls.SetChildIndex(this.btnYes, 0);
			this.Controls.SetChildIndex(this.txtMessage, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.RichTextBox txtMessage;
	}
}
