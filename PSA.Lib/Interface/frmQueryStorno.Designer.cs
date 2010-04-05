namespace PSA.Lib.Interface
{
    partial class frmQueryStorno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueryStorno));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioBonus = new System.Windows.Forms.RadioButton();
            this.radioMoney = new System.Windows.Forms.RadioButton();
            this.lblBonus = new System.Windows.Forms.Label();
            this.lblMoney = new System.Windows.Forms.Label();
            this.lblStornoInfo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(344, 188);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(120, 30);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(344, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Возврат за счет:";
            // 
            // radioBonus
            // 
            this.radioBonus.AutoSize = true;
            this.radioBonus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioBonus.Location = new System.Drawing.Point(137, 63);
            this.radioBonus.Name = "radioBonus";
            this.radioBonus.Size = new System.Drawing.Size(82, 20);
            this.radioBonus.TabIndex = 5;
            this.radioBonus.TabStop = true;
            this.radioBonus.Text = "Бонусов";
            this.radioBonus.UseVisualStyleBackColor = true;
            // 
            // radioMoney
            // 
            this.radioMoney.AutoSize = true;
            this.radioMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioMoney.Location = new System.Drawing.Point(137, 89);
            this.radioMoney.Name = "radioMoney";
            this.radioMoney.Size = new System.Drawing.Size(91, 20);
            this.radioMoney.TabIndex = 6;
            this.radioMoney.TabStop = true;
            this.radioMoney.Text = "Наличных";
            this.radioMoney.UseVisualStyleBackColor = true;
            // 
            // lblBonus
            // 
            this.lblBonus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBonus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblBonus.Location = new System.Drawing.Point(234, 67);
            this.lblBonus.Name = "lblBonus";
            this.lblBonus.Size = new System.Drawing.Size(231, 16);
            this.lblBonus.TabIndex = 7;
            this.lblBonus.Text = "Получено: ХХХ б.";
            // 
            // lblMoney
            // 
            this.lblMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMoney.ForeColor = System.Drawing.Color.Green;
            this.lblMoney.Location = new System.Drawing.Point(234, 93);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(231, 16);
            this.lblMoney.TabIndex = 8;
            this.lblMoney.Text = "Получено: ХХХ р.";
            // 
            // lblStornoInfo
            // 
            this.lblStornoInfo.AutoSize = true;
            this.lblStornoInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStornoInfo.ForeColor = System.Drawing.Color.Maroon;
            this.lblStornoInfo.Location = new System.Drawing.Point(12, 112);
            this.lblStornoInfo.Name = "lblStornoInfo";
            this.lblStornoInfo.Size = new System.Drawing.Size(195, 16);
            this.lblStornoInfo.TabIndex = 9;
            this.lblStornoInfo.Text = "Сумма к возврату: ХХХ р.";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(326, 66);
            this.label6.TabIndex = 10;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblInfo.Location = new System.Drawing.Point(12, 131);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(452, 54);
            this.lblInfo.TabIndex = 11;
            this.lblInfo.Text = "Бонусы сняты с/не дисконтной карты.\r\nНе/Возвращаются услуги, расчет за которые де" +
                "лался бонусами.";
            // 
            // frmQueryStorno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(476, 266);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblStornoInfo);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.lblBonus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.radioBonus);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.radioMoney);
            this.Name = "frmQueryStorno";
            this.Text = "м";
            this.Load += new System.EventHandler(this.frmQueryStorno_Load);
            this.Controls.SetChildIndex(this.radioMoney, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.radioBonus, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblBonus, 0);
            this.Controls.SetChildIndex(this.lblMoney, 0);
            this.Controls.SetChildIndex(this.lblStornoInfo, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.lblInfo, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioBonus;
        private System.Windows.Forms.RadioButton radioMoney;
        private System.Windows.Forms.Label lblBonus;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label lblStornoInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblInfo;
    }
}
