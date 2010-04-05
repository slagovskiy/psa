namespace Photoland.Operator
{
    partial class frmSelectMashineAndPaper
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
			this.btnOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtMashine = new System.Windows.Forms.ComboBox();
			this.txtPaper = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtCounter = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.txtCounter)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(177, 171);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(402, 70);
			this.label1.TabIndex = 1;
			this.label1.Text = "Перед началом работы необходимо указать машину, на которой производятся работы, а" +
				" так же значение счетчика отпечатков.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 85);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Машина";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Enabled = false;
			this.label3.Location = new System.Drawing.Point(12, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Бумага";
			// 
			// txtMashine
			// 
			this.txtMashine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtMashine.FormattingEnabled = true;
			this.txtMashine.Location = new System.Drawing.Point(76, 82);
			this.txtMashine.Name = "txtMashine";
			this.txtMashine.Size = new System.Drawing.Size(338, 21);
			this.txtMashine.TabIndex = 0;
			this.txtMashine.SelectedIndexChanged += new System.EventHandler(this.txtMashine_SelectedIndexChanged);
			// 
			// txtPaper
			// 
			this.txtPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtPaper.Enabled = false;
			this.txtPaper.FormattingEnabled = true;
			this.txtPaper.Location = new System.Drawing.Point(76, 109);
			this.txtPaper.Name = "txtPaper";
			this.txtPaper.Size = new System.Drawing.Size(338, 21);
			this.txtPaper.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 138);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Счетчик";
			// 
			// txtCounter
			// 
			this.txtCounter.Location = new System.Drawing.Point(76, 136);
			this.txtCounter.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
			this.txtCounter.Name = "txtCounter";
			this.txtCounter.Size = new System.Drawing.Size(120, 20);
			this.txtCounter.TabIndex = 6;
			this.txtCounter.ValueChanged += new System.EventHandler(this.txtCounter_ValueChanged);
			this.txtCounter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCounter_KeyUp);
			// 
			// frmSelectMashineAndPaper
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(426, 206);
			this.Controls.Add(this.txtCounter);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPaper);
			this.Controls.Add(this.txtMashine);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSelectMashineAndPaper";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmSelectMashineAndPaper_Load);
			((System.ComponentModel.ISupportInitialize)(this.txtCounter)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox txtMashine;
        public System.Windows.Forms.ComboBox txtPaper;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.NumericUpDown txtCounter;
    }
}