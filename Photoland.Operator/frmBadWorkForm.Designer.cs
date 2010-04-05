namespace Photoland.Operator
{
    partial class frmBadWorkForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtGood = new System.Windows.Forms.ComboBox();
			this.txtOrderNo = new System.Windows.Forms.TextBox();
			this.txtQuantity = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnSub = new System.Windows.Forms.Button();
			this.txtPaper = new System.Windows.Forms.ComboBox();
			this.txtMashine = new System.Windows.Forms.ComboBox();
			this.txtType = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Заказ №";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Услуга";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(12, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Бумага";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(12, 95);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Машина";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(12, 122);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(76, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Количество";
			// 
			// txtGood
			// 
			this.txtGood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtGood.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtGood.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtGood.FormattingEnabled = true;
			this.txtGood.Location = new System.Drawing.Point(108, 38);
			this.txtGood.Name = "txtGood";
			this.txtGood.Size = new System.Drawing.Size(363, 21);
			this.txtGood.TabIndex = 0;
			// 
			// txtOrderNo
			// 
			this.txtOrderNo.Location = new System.Drawing.Point(108, 12);
			this.txtOrderNo.Name = "txtOrderNo";
			this.txtOrderNo.ReadOnly = true;
			this.txtOrderNo.Size = new System.Drawing.Size(363, 20);
			this.txtOrderNo.TabIndex = 7;
			// 
			// txtQuantity
			// 
			this.txtQuantity.Location = new System.Drawing.Point(108, 119);
			this.txtQuantity.Name = "txtQuantity";
			this.txtQuantity.Size = new System.Drawing.Size(324, 20);
			this.txtQuantity.TabIndex = 3;
			this.txtQuantity.Text = "0";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(397, 173);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(316, 173);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "ОК";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnAdd.Location = new System.Drawing.Point(432, 119);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(20, 20);
			this.btnAdd.TabIndex = 8;
			this.btnAdd.Text = "+";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnSub
			// 
			this.btnSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSub.Location = new System.Drawing.Point(451, 119);
			this.btnSub.Name = "btnSub";
			this.btnSub.Size = new System.Drawing.Size(20, 20);
			this.btnSub.TabIndex = 9;
			this.btnSub.Text = "-";
			this.btnSub.UseVisualStyleBackColor = true;
			this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
			// 
			// txtPaper
			// 
			this.txtPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtPaper.FormattingEnabled = true;
			this.txtPaper.Location = new System.Drawing.Point(108, 65);
			this.txtPaper.Name = "txtPaper";
			this.txtPaper.Size = new System.Drawing.Size(363, 21);
			this.txtPaper.TabIndex = 1;
			// 
			// txtMashine
			// 
			this.txtMashine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtMashine.FormattingEnabled = true;
			this.txtMashine.Location = new System.Drawing.Point(108, 92);
			this.txtMashine.Name = "txtMashine";
			this.txtMashine.Size = new System.Drawing.Size(363, 21);
			this.txtMashine.TabIndex = 2;
			// 
			// txtType
			// 
			this.txtType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.txtType.FormattingEnabled = true;
			this.txtType.Location = new System.Drawing.Point(108, 145);
			this.txtType.Name = "txtType";
			this.txtType.Size = new System.Drawing.Size(363, 21);
			this.txtType.TabIndex = 4;
			this.txtType.SelectedIndexChanged += new System.EventHandler(this.txtType_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.Location = new System.Drawing.Point(12, 148);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(57, 13);
			this.label6.TabIndex = 37;
			this.label6.Text = "Причина";
			// 
			// frmBadWorkForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 208);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtType);
			this.Controls.Add(this.txtPaper);
			this.Controls.Add(this.txtMashine);
			this.Controls.Add(this.btnSub);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtQuantity);
			this.Controls.Add(this.txtOrderNo);
			this.Controls.Add(this.txtGood);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmBadWorkForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmBadWorkForm";
			this.Load += new System.EventHandler(this.frmBadWorkForm_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmBadWorkForm_KeyPress);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox txtGood;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSub;
        public System.Windows.Forms.ComboBox txtPaper;
        public System.Windows.Forms.ComboBox txtMashine;
        public System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.ComboBox txtType;
        private System.Windows.Forms.Label label6;
    }
}