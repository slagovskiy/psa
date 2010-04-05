namespace Photoland.Operator
{
    partial class frmDiscardingReportPrep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiscardingReportPrep));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDateBegin = new System.Windows.Forms.DateTimePicker();
            this.txtDateEnd = new System.Windows.Forms.DateTimePicker();
            this.checkCurentUser = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.rep = new C1.Win.C1Report.C1Report();
            ((System.ComponentModel.ISupportInitialize)(this.rep)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Начальная дата";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Конечная дата";
            // 
            // txtDateBegin
            // 
            this.txtDateBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateBegin.Location = new System.Drawing.Point(106, 9);
            this.txtDateBegin.Name = "txtDateBegin";
            this.txtDateBegin.Size = new System.Drawing.Size(113, 20);
            this.txtDateBegin.TabIndex = 2;
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDateEnd.Location = new System.Drawing.Point(106, 35);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(113, 20);
            this.txtDateEnd.TabIndex = 3;
            // 
            // checkCurentUser
            // 
            this.checkCurentUser.AutoSize = true;
            this.checkCurentUser.Checked = true;
            this.checkCurentUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkCurentUser.Location = new System.Drawing.Point(12, 61);
            this.checkCurentUser.Name = "checkCurentUser";
            this.checkCurentUser.Size = new System.Drawing.Size(207, 17);
            this.checkCurentUser.TabIndex = 4;
            this.checkCurentUser.Text = "Только по текущему пользователю";
            this.checkCurentUser.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(63, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(144, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // rep
            // 
            this.rep.ReportDefinition = resources.GetString("rep.ReportDefinition");
            // 
            // frmDiscardingReportPrep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 116);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkCurentUser);
            this.Controls.Add(this.txtDateEnd);
            this.Controls.Add(this.txtDateBegin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDiscardingReportPrep";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDiscardingReportPrep";
            ((System.ComponentModel.ISupportInitialize)(this.rep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtDateBegin;
        private System.Windows.Forms.DateTimePicker txtDateEnd;
        private System.Windows.Forms.CheckBox checkCurentUser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private C1.Win.C1Report.C1Report rep;
    }
}