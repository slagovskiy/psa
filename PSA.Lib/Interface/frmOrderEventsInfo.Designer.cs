namespace PSA.Lib.Interface
{
    partial class frmOrderEventsInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderEventsInfo));
            this.data = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.Label();
            this.txtAdvPay = new System.Windows.Forms.Label();
            this.txtFinalPay = new System.Windows.Forms.Label();
            this.txtBonus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.SuspendLayout();
            // 
            // data
            // 
            this.data.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.data.AllowEditing = false;
            this.data.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this.data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.data.ColumnInfo = "1,1,0,0,0,85,Columns:0{Width:20;}\t";
            this.data.Location = new System.Drawing.Point(12, 126);
            this.data.Name = "data";
            this.data.Rows.DefaultSize = 17;
            this.data.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.data.Size = new System.Drawing.Size(608, 292);
            this.data.StyleInfo = resources.GetString("data.StyleInfo");
            this.data.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(500, 424);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 30);
            this.button1.TabIndex = 8;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtInfo.Location = new System.Drawing.Point(12, 60);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(608, 23);
            this.txtInfo.TabIndex = 10;
            // 
            // txtAdvPay
            // 
            this.txtAdvPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtAdvPay.Location = new System.Drawing.Point(12, 83);
            this.txtAdvPay.Name = "txtAdvPay";
            this.txtAdvPay.Size = new System.Drawing.Size(292, 20);
            this.txtAdvPay.TabIndex = 11;
            // 
            // txtFinalPay
            // 
            this.txtFinalPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFinalPay.Location = new System.Drawing.Point(328, 83);
            this.txtFinalPay.Name = "txtFinalPay";
            this.txtFinalPay.Size = new System.Drawing.Size(292, 20);
            this.txtFinalPay.TabIndex = 12;
            // 
            // txtBonus
            // 
            this.txtBonus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtBonus.Location = new System.Drawing.Point(12, 103);
            this.txtBonus.Name = "txtBonus";
            this.txtBonus.Size = new System.Drawing.Size(292, 20);
            this.txtBonus.TabIndex = 13;
            // 
            // frmOrderEventsInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 466);
            this.Controls.Add(this.txtBonus);
            this.Controls.Add(this.txtFinalPay);
            this.Controls.Add(this.txtAdvPay);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.data);
            this.Controls.Add(this.button1);
            this.Name = "frmOrderEventsInfo";
            this.Text = "frmOrderEventsInfo";
            this.Load += new System.EventHandler(this.frmOrderEventsInfo_Load);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.data, 0);
            this.Controls.SetChildIndex(this.txtInfo, 0);
            this.Controls.SetChildIndex(this.txtAdvPay, 0);
            this.Controls.SetChildIndex(this.txtFinalPay, 0);
            this.Controls.SetChildIndex(this.txtBonus, 0);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid data;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label txtInfo;
        private System.Windows.Forms.Label txtAdvPay;
        private System.Windows.Forms.Label txtFinalPay;
        private System.Windows.Forms.Label txtBonus;
    }
}