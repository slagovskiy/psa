namespace PSA.Lib.Interface
{
    partial class frmOrderEvents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderEvents));
            this.button1 = new System.Windows.Forms.Button();
            this.data = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(660, 424);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.data.Location = new System.Drawing.Point(3, 63);
            this.data.Name = "data";
            this.data.Rows.DefaultSize = 17;
            this.data.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.data.Size = new System.Drawing.Size(784, 355);
            this.data.StyleInfo = resources.GetString("data.StyleInfo");
            this.data.TabIndex = 7;
            this.data.DoubleClick += new System.EventHandler(this.data_DoubleClick);
            // 
            // frmOrderEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(792, 466);
            this.Controls.Add(this.data);
            this.Controls.Add(this.button1);
            this.Name = "frmOrderEvents";
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.data, 0);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private C1.Win.C1FlexGrid.C1FlexGrid data;
    }
}
