namespace Photoland.Forms.Interface
{
	partial class frmSelectWork
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
			this.btnDesing = new System.Windows.Forms.Button();
			this.btnOperator = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnDesing
			// 
			this.btnDesing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnDesing.ForeColor = System.Drawing.Color.Navy;
			this.btnDesing.Location = new System.Drawing.Point(12, 17);
			this.btnDesing.Name = "btnDesing";
			this.btnDesing.Size = new System.Drawing.Size(250, 50);
			this.btnDesing.TabIndex = 0;
			this.btnDesing.Text = "Передать ДИЗАЙНЕРУ";
			this.btnDesing.UseVisualStyleBackColor = true;
			this.btnDesing.Click += new System.EventHandler(this.btnDesing_Click);
			// 
			// btnOperator
			// 
			this.btnOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnOperator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.btnOperator.Location = new System.Drawing.Point(12, 73);
			this.btnOperator.Name = "btnOperator";
			this.btnOperator.Size = new System.Drawing.Size(250, 50);
			this.btnOperator.TabIndex = 1;
			this.btnOperator.Text = "Передать ОПЕРАТОРУ";
			this.btnOperator.UseVisualStyleBackColor = true;
			this.btnOperator.Click += new System.EventHandler(this.btnOperator_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.btnCancel.Location = new System.Drawing.Point(12, 165);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(250, 50);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Вернуться к заказу";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmSelectWork
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(274, 227);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOperator);
			this.Controls.Add(this.btnDesing);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSelectWork";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Кому передать?";
			this.Load += new System.EventHandler(this.frmSelectWork_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnDesing;
		private System.Windows.Forms.Button btnOperator;
		private System.Windows.Forms.Button btnCancel;
	}
}