namespace PSA.Lib.Interface
{
	partial class frmKioskItem
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtId = new System.Windows.Forms.TextBox();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSetPath = new System.Windows.Forms.Button();
			this.checkDeleted = new System.Windows.Forms.CheckBox();
			this.btnCheck = new System.Windows.Forms.Button();
			this.btnSync = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.dlg = new System.Windows.Forms.FolderBrowserDialog();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Location = new System.Drawing.Point(2, 63);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(390, 310);
			this.panel1.TabIndex = 2;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.tableLayoutPanel1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(390, 265);
			this.panel3.TabIndex = 2;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.113F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.887F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.txtId, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtCode, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtPath, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnSetPath, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.checkDeleted, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.btnCheck, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.btnSync, 1, 6);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 265);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(134, 26);
			this.label3.TabIndex = 1;
			this.label3.Text = "Номер";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 52);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134, 26);
			this.label4.TabIndex = 2;
			this.label4.Text = "Описание";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 78);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(134, 26);
			this.label5.TabIndex = 3;
			this.label5.Text = "Путь к базе";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtId
			// 
			this.txtId.Location = new System.Drawing.Point(143, 3);
			this.txtId.Name = "txtId";
			this.txtId.ReadOnly = true;
			this.txtId.Size = new System.Drawing.Size(203, 20);
			this.txtId.TabIndex = 4;
			// 
			// txtCode
			// 
			this.txtCode.Location = new System.Drawing.Point(143, 29);
			this.txtCode.MaxLength = 3;
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(203, 20);
			this.txtCode.TabIndex = 5;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(143, 55);
			this.txtName.MaxLength = 1024;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(203, 20);
			this.txtName.TabIndex = 6;
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(143, 81);
			this.txtPath.MaxLength = 1024;
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(203, 20);
			this.txtPath.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(134, 26);
			this.label2.TabIndex = 0;
			this.label2.Text = "Код";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSetPath
			// 
			this.btnSetPath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSetPath.Location = new System.Drawing.Point(352, 81);
			this.btnSetPath.Name = "btnSetPath";
			this.btnSetPath.Size = new System.Drawing.Size(35, 20);
			this.btnSetPath.TabIndex = 8;
			this.btnSetPath.Text = "...";
			this.btnSetPath.UseVisualStyleBackColor = true;
			this.btnSetPath.Click += new System.EventHandler(this.btnSetPath_Click);
			// 
			// checkDeleted
			// 
			this.checkDeleted.AutoSize = true;
			this.checkDeleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkDeleted.ForeColor = System.Drawing.Color.Red;
			this.checkDeleted.Location = new System.Drawing.Point(143, 107);
			this.checkDeleted.Name = "checkDeleted";
			this.checkDeleted.Size = new System.Drawing.Size(171, 17);
			this.checkDeleted.TabIndex = 9;
			this.checkDeleted.Text = "Удален (не опрашивать)";
			this.checkDeleted.UseVisualStyleBackColor = true;
			// 
			// btnCheck
			// 
			this.btnCheck.Location = new System.Drawing.Point(143, 130);
			this.btnCheck.Name = "btnCheck";
			this.btnCheck.Size = new System.Drawing.Size(203, 30);
			this.btnCheck.TabIndex = 10;
			this.btnCheck.Text = "Проверить связь";
			this.btnCheck.UseVisualStyleBackColor = true;
			this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
			// 
			// btnSync
			// 
			this.btnSync.Location = new System.Drawing.Point(143, 166);
			this.btnSync.Name = "btnSync";
			this.btnSync.Size = new System.Drawing.Size(203, 30);
			this.btnSync.TabIndex = 11;
			this.btnSync.Text = "Синхронизировать принятые заказы";
			this.btnSync.UseVisualStyleBackColor = true;
			this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnSave);
			this.panel2.Controls.Add(this.btnClose);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 265);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(390, 45);
			this.panel2.TabIndex = 1;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(151, 10);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(115, 30);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(272, 10);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(115, 30);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Закрыть";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmKioskItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(392, 373);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmKioskItem";
			this.Text = "               Фотокиоск - PSA v.9.0.21022.8";
			this.Title = "                  Фотокиоск";
			this.Load += new System.EventHandler(this.KioskItem_Load);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnSetPath;
		private System.Windows.Forms.CheckBox checkDeleted;
		private System.Windows.Forms.FolderBrowserDialog dlg;
		private System.Windows.Forms.Button btnCheck;
		private System.Windows.Forms.Button btnSync;
	}
}
