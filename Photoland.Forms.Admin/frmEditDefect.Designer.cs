namespace Photoland.Forms.Admin
{
    partial class frmEditDefect
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
            this.txtGood = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInfoOperator = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblInfoDesigner = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblInfoInput = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkOK = new System.Windows.Forms.CheckBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblwho = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtGood
            // 
            this.txtGood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtGood.Enabled = false;
            this.txtGood.FormattingEnabled = true;
            this.txtGood.Location = new System.Drawing.Point(134, 12);
            this.txtGood.Name = "txtGood";
            this.txtGood.Size = new System.Drawing.Size(296, 21);
            this.txtGood.TabIndex = 47;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(14, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 46;
            this.label14.Text = "Услуга";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblInfoOperator);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblInfoDesigner);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblInfoInput);
            this.groupBox1.Location = new System.Drawing.Point(17, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 73);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация об исполнителях заказа";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Indigo;
            this.label4.Location = new System.Drawing.Point(11, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Приемка:";
            // 
            // lblInfoOperator
            // 
            this.lblInfoOperator.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoOperator.ForeColor = System.Drawing.Color.Maroon;
            this.lblInfoOperator.Location = new System.Drawing.Point(73, 50);
            this.lblInfoOperator.Name = "lblInfoOperator";
            this.lblInfoOperator.Size = new System.Drawing.Size(331, 20);
            this.lblInfoOperator.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkGreen;
            this.label5.Location = new System.Drawing.Point(6, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Дизайнер:";
            // 
            // lblInfoDesigner
            // 
            this.lblInfoDesigner.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoDesigner.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblInfoDesigner.Location = new System.Drawing.Point(73, 33);
            this.lblInfoDesigner.Name = "lblInfoDesigner";
            this.lblInfoDesigner.Size = new System.Drawing.Size(331, 17);
            this.lblInfoDesigner.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(8, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Оператор:";
            // 
            // lblInfoInput
            // 
            this.lblInfoInput.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoInput.ForeColor = System.Drawing.Color.Indigo;
            this.lblInfoInput.Location = new System.Drawing.Point(73, 16);
            this.lblInfoInput.Name = "lblInfoInput";
            this.lblInfoInput.Size = new System.Drawing.Size(331, 17);
            this.lblInfoInput.TabIndex = 15;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(274, 316);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 35;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(355, 316);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtType
            // 
            this.txtType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtType.FormattingEnabled = true;
            this.txtType.Location = new System.Drawing.Point(134, 206);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(296, 21);
            this.txtType.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(14, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Причина";
            // 
            // txtUser
            // 
            this.txtUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtUser.FormattingEnabled = true;
            this.txtUser.Location = new System.Drawing.Point(134, 179);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(296, 21);
            this.txtUser.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(14, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "На кого списание";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(134, 39);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(93, 20);
            this.txtCount.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(14, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Количество";
            // 
            // checkOK
            // 
            this.checkOK.AutoSize = true;
            this.checkOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkOK.Location = new System.Drawing.Point(134, 290);
            this.checkOK.Name = "checkOK";
            this.checkOK.Size = new System.Drawing.Size(137, 20);
            this.checkOK.TabIndex = 48;
            this.checkOK.Text = "Подтверждено";
            this.checkOK.UseVisualStyleBackColor = true;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(134, 233);
            this.txtComment.MaxLength = 1020;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComment.Size = new System.Drawing.Size(296, 51);
            this.txtComment.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(14, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "Комментарий";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(17, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(407, 40);
            this.label8.TabIndex = 51;
            this.label8.Text = "Из за изменения количества в возвратах может произойти пересчет заказа!";
            // 
            // lblwho
            // 
            this.lblwho.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblwho.Location = new System.Drawing.Point(134, 179);
            this.lblwho.Name = "lblwho";
            this.lblwho.Size = new System.Drawing.Size(296, 21);
            this.lblwho.TabIndex = 52;
            this.lblwho.Visible = false;
            // 
            // frmEditDefect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 353);
            this.Controls.Add(this.lblwho);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.checkOK);
            this.Controls.Add(this.txtGood);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditDefect";
            this.ShowInTaskbar = false;
            this.Text = "frmEditDefect";
            this.Load += new System.EventHandler(this.frmEditDefect_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmQueryCount_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox txtGood;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInfoOperator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblInfoDesigner;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblInfoInput;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.ComboBox txtType;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox txtUser;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox checkOK;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblwho;
    }
}