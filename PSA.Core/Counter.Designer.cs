namespace Photoland.Components.Counter
{
	partial class Counter
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.text = new System.Windows.Forms.Label();
			this.lbl = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// text
			// 
			this.text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.text.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.text.Location = new System.Drawing.Point(-1, -1);
			this.text.Name = "text";
			this.text.Size = new System.Drawing.Size(29, 27);
			this.text.TabIndex = 3;
			this.text.Text = "33A";
			this.text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl
			// 
			this.lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl.BackColor = System.Drawing.Color.White;
			this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lbl.Location = new System.Drawing.Point(0, 25);
			this.lbl.Name = "lbl";
			this.lbl.Size = new System.Drawing.Size(28, 29);
			this.lbl.TabIndex = 2;
			this.lbl.Text = "3";
			this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseClick);
			this.lbl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseDoubleClick);
			// 
			// Counter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.text);
			this.Controls.Add(this.lbl);
			this.Name = "Counter";
			this.Size = new System.Drawing.Size(27, 54);
			this.Load += new System.EventHandler(this.Counter_Load);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Counter_MouseClick);
			this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Counter_MouseDoubleClick);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label text;
		private System.Windows.Forms.Label lbl;

	}
}
