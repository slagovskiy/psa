namespace Photoland.Forms.Interface
{
	partial class frmQueryFrameParam
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueryFrameParam));
			this.preview = new System.Windows.Forms.PictureBox();
			this.txtW = new System.Windows.Forms.TextBox();
			this.txtH = new System.Windows.Forms.TextBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtS = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnSelectFile = new System.Windows.Forms.Button();
			this.lblPath = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.preview)).BeginInit();
			this.SuspendLayout();
			// 
			// preview
			// 
			this.preview.Image = ((System.Drawing.Image)(resources.GetObject("preview.Image")));
			this.preview.Location = new System.Drawing.Point(12, 32);
			this.preview.Name = "preview";
			this.preview.Size = new System.Drawing.Size(311, 283);
			this.preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.preview.TabIndex = 1;
			this.preview.TabStop = false;
			// 
			// txtW
			// 
			this.txtW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(215)))));
			this.txtW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtW.Location = new System.Drawing.Point(65, 11);
			this.txtW.Name = "txtW";
			this.txtW.Size = new System.Drawing.Size(258, 20);
			this.txtW.TabIndex = 2;
			this.txtW.Text = "0";
			this.txtW.TextChanged += new System.EventHandler(this.txtW_TextChanged);
			this.txtW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtW_KeyPress);
			// 
			// txtH
			// 
			this.txtH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
			this.txtH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtH.Location = new System.Drawing.Point(62, 316);
			this.txtH.Name = "txtH";
			this.txtH.Size = new System.Drawing.Size(261, 20);
			this.txtH.TabIndex = 3;
			this.txtH.Text = "0";
			this.txtH.TextChanged += new System.EventHandler(this.txtH_TextChanged);
			this.txtH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtH_KeyPress);
			// 
			// txtComment
			// 
			this.txtComment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtComment.Location = new System.Drawing.Point(329, 29);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtComment.Size = new System.Drawing.Size(283, 104);
			this.txtComment.TabIndex = 4;
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Location = new System.Drawing.Point(369, 306);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(120, 35);
			this.btnApply.TabIndex = 22;
			this.btnApply.Text = "Применить";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(495, 306);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(120, 35);
			this.btnCancel.TabIndex = 21;
			this.btnCancel.Text = "Отменить";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 23;
			this.label1.Text = "Ширина";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 318);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 24;
			this.label2.Text = "Высота";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(329, 189);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 13);
			this.label3.TabIndex = 25;
			this.label3.Text = "Площадь";
			// 
			// txtS
			// 
			this.txtS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.txtS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtS.Enabled = false;
			this.txtS.Location = new System.Drawing.Point(389, 187);
			this.txtS.Name = "txtS";
			this.txtS.Size = new System.Drawing.Size(223, 20);
			this.txtS.TabIndex = 26;
			this.txtS.Text = "0";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(329, 219);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(283, 73);
			this.label4.TabIndex = 27;
			this.label4.Text = "Площадь является основным показателем, на основании которого будет расчитана стои" +
				"мость, остальные параметры просто для информации.";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(329, 13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 28;
			this.label5.Text = "Комментарий";
			// 
			// txtFile
			// 
			this.txtFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFile.Location = new System.Drawing.Point(389, 161);
			this.txtFile.Name = "txtFile";
			this.txtFile.ReadOnly = true;
			this.txtFile.Size = new System.Drawing.Size(192, 20);
			this.txtFile.TabIndex = 30;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(329, 163);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(36, 13);
			this.label6.TabIndex = 29;
			this.label6.Text = "Файл";
			// 
			// btnSelectFile
			// 
			this.btnSelectFile.Location = new System.Drawing.Point(587, 161);
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.Size = new System.Drawing.Size(25, 20);
			this.btnSelectFile.TabIndex = 31;
			this.btnSelectFile.Text = "...";
			this.btnSelectFile.UseVisualStyleBackColor = true;
			this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
			// 
			// lblPath
			// 
			this.lblPath.Font = new System.Drawing.Font("Lucida Sans", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblPath.Location = new System.Drawing.Point(326, 136);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(286, 22);
			this.lblPath.TabIndex = 32;
			// 
			// frmQueryFrameParam
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 353);
			this.Controls.Add(this.lblPath);
			this.Controls.Add(this.btnSelectFile);
			this.Controls.Add(this.txtFile);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtS);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.txtH);
			this.Controls.Add(this.txtW);
			this.Controls.Add(this.preview);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmQueryFrameParam";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmQueryFrameParam";
			this.Load += new System.EventHandler(this.frmQueryFrameParam_Load);
			((System.ComponentModel.ISupportInitialize)(this.preview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox preview;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.TextBox txtW;
		public System.Windows.Forms.TextBox txtH;
		public System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox txtS;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnSelectFile;
		public System.Windows.Forms.Label lblPath;
	}
}