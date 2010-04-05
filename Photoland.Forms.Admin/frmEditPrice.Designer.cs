namespace Photoland.Forms.Admin
{
	partial class frmEditPrice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditPrice));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gridData = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.lblGood = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(623, 231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(542, 231);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 19;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gridData
            // 
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.BackColor = System.Drawing.Color.White;
            this.gridData.ColumnInfo = "7,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:165;}\t2{Width:100;}\t3{Width:100;}\t4{Wid" +
                "th:100;}\t5{Width:100;}\t6{Width:100;}\t";
            this.gridData.Location = new System.Drawing.Point(1, 51);
            this.gridData.Name = "gridData";
            this.gridData.Rows.Count = 2;
            this.gridData.Rows.DefaultSize = 17;
            this.gridData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.gridData.Size = new System.Drawing.Size(785, 174);
            this.gridData.StyleInfo = resources.GetString("gridData.StyleInfo");
            this.gridData.TabIndex = 21;
            // 
            // lblGood
            // 
            this.lblGood.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblGood.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblGood.Location = new System.Drawing.Point(12, 9);
            this.lblGood.Name = "lblGood";
            this.lblGood.Size = new System.Drawing.Size(686, 39);
            this.lblGood.TabIndex = 22;
            // 
            // frmEditPrice
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(710, 266);
            this.Controls.Add(this.lblGood);
            this.Controls.Add(this.gridData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditPrice";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEditPrice";
            this.Load += new System.EventHandler(this.frmEditPrice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private C1.Win.C1FlexGrid.C1FlexGrid gridData;
		private System.Windows.Forms.Label lblGood;
	}
}