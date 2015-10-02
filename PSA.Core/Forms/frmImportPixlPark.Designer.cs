namespace PSA.Core.Forms
{
    partial class frmImportPixlPark
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportPixlPark));
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.btnImportA = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnImportO = new System.Windows.Forms.Button();
            this.btnImportD = new System.Windows.Forms.Button();
            this.rep = new C1.Win.C1Report.C1Report();
            ((System.ComponentModel.ISupportInitialize)(this.rep)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите номер заказа:";
            // 
            // txtOrder
            // 
            this.txtOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtOrder.Location = new System.Drawing.Point(16, 48);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(221, 35);
            this.txtOrder.TabIndex = 1;
            // 
            // btnImportA
            // 
            this.btnImportA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnImportA.ForeColor = System.Drawing.Color.Red;
            this.btnImportA.Location = new System.Drawing.Point(12, 89);
            this.btnImportA.Name = "btnImportA";
            this.btnImportA.Size = new System.Drawing.Size(225, 30);
            this.btnImportA.TabIndex = 2;
            this.btnImportA.Text = "На выдачу";
            this.btnImportA.UseVisualStyleBackColor = true;
            this.btnImportA.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 125);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(225, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnImportO
            // 
            this.btnImportO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnImportO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnImportO.Location = new System.Drawing.Point(12, 161);
            this.btnImportO.Name = "btnImportO";
            this.btnImportO.Size = new System.Drawing.Size(225, 30);
            this.btnImportO.TabIndex = 4;
            this.btnImportO.Text = "В печать";
            this.btnImportO.UseVisualStyleBackColor = true;
            this.btnImportO.Click += new System.EventHandler(this.btnImportO_Click);
            // 
            // btnImportD
            // 
            this.btnImportD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnImportD.ForeColor = System.Drawing.Color.Green;
            this.btnImportD.Location = new System.Drawing.Point(12, 198);
            this.btnImportD.Name = "btnImportD";
            this.btnImportD.Size = new System.Drawing.Size(225, 30);
            this.btnImportD.TabIndex = 5;
            this.btnImportD.Text = "На обработку";
            this.btnImportD.UseVisualStyleBackColor = true;
            this.btnImportD.Click += new System.EventHandler(this.btnImportD_Click);
            // 
            // rep
            // 
            this.rep.ReportDefinition = resources.GetString("rep.ReportDefinition");
            // 
            // frmImportPixlPark
            // 
            this.AcceptButton = this.btnImportA;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(249, 240);
            this.Controls.Add(this.btnImportD);
            this.Controls.Add(this.btnImportO);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnImportA);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.label1);
            this.Name = "frmImportPixlPark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "print.fotoland.ru";
            this.Load += new System.EventHandler(this.frmImportPixlPark_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.Button btnImportA;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnImportO;
        private System.Windows.Forms.Button btnImportD;
        private C1.Win.C1Report.C1Report rep;
    }
}