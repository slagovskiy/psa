namespace PSA.Lib.Interface
{
	partial class frmSemaphores
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSemaphores));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btnRemotePath = new System.Windows.Forms.Button();
			this.txtRemoteSetupPath = new System.Windows.Forms.TextBox();
			this.label63 = new System.Windows.Forms.Label();
			this.checkRemoteSetup = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.dlg = new System.Windows.Forms.FolderBrowserDialog();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.checkInventory = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 63);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(448, 175);
			this.tabControl1.TabIndex = 2;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.btnRemotePath);
			this.tabPage1.Controls.Add(this.txtRemoteSetupPath);
			this.tabPage1.Controls.Add(this.label63);
			this.tabPage1.Controls.Add(this.checkRemoteSetup);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(440, 149);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Удаленные настройки";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btnRemotePath
			// 
			this.btnRemotePath.Location = new System.Drawing.Point(402, 92);
			this.btnRemotePath.Name = "btnRemotePath";
			this.btnRemotePath.Size = new System.Drawing.Size(32, 23);
			this.btnRemotePath.TabIndex = 7;
			this.btnRemotePath.Text = "...";
			this.btnRemotePath.UseVisualStyleBackColor = true;
			this.btnRemotePath.Click += new System.EventHandler(this.btnRemotePath_Click);
			// 
			// txtRemoteSetupPath
			// 
			this.txtRemoteSetupPath.Location = new System.Drawing.Point(118, 93);
			this.txtRemoteSetupPath.Name = "txtRemoteSetupPath";
			this.txtRemoteSetupPath.Size = new System.Drawing.Size(278, 20);
			this.txtRemoteSetupPath.TabIndex = 6;
			// 
			// label63
			// 
			this.label63.AutoSize = true;
			this.label63.Location = new System.Drawing.Point(3, 97);
			this.label63.Name = "label63";
			this.label63.Size = new System.Drawing.Size(104, 13);
			this.label63.TabIndex = 5;
			this.label63.Text = "Путь к настройкам";
			// 
			// checkRemoteSetup
			// 
			this.checkRemoteSetup.AutoSize = true;
			this.checkRemoteSetup.Location = new System.Drawing.Point(6, 70);
			this.checkRemoteSetup.Name = "checkRemoteSetup";
			this.checkRemoteSetup.Size = new System.Drawing.Size(174, 17);
			this.checkRemoteSetup.TabIndex = 4;
			this.checkRemoteSetup.Text = "Хранить настройки удаленно";
			this.checkRemoteSetup.UseVisualStyleBackColor = true;
			this.checkRemoteSetup.CheckedChanged += new System.EventHandler(this.checkRemoteSetup_CheckedChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(6, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(428, 64);
			this.label2.TabIndex = 0;
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(224, 244);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(115, 30);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "Сохранить";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(345, 244);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(115, 30);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.checkInventory);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(440, 149);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Инвентаризация";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// checkInventory
			// 
			this.checkInventory.AutoSize = true;
			this.checkInventory.Location = new System.Drawing.Point(6, 70);
			this.checkInventory.Name = "checkInventory";
			this.checkInventory.Size = new System.Drawing.Size(283, 17);
			this.checkInventory.TabIndex = 6;
			this.checkInventory.Text = "Блокировать доступ (проводится инвентаризация)";
			this.checkInventory.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(6, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(428, 64);
			this.label3.TabIndex = 5;
			this.label3.Text = "Этот семафор отвечает за блокировку работы всех модулей (кроме администратора и р" +
				"оботов) на момент проведения инвентаризации.";
			// 
			// frmSemaphores
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(472, 286);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Name = "frmSemaphores";
			this.Load += new System.EventHandler(this.frmSemaphores_Load);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.tabControl1, 0);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnRemotePath;
		private System.Windows.Forms.TextBox txtRemoteSetupPath;
		private System.Windows.Forms.Label label63;
		private System.Windows.Forms.CheckBox checkRemoteSetup;
		private System.Windows.Forms.FolderBrowserDialog dlg;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.CheckBox checkInventory;
		private System.Windows.Forms.Label label3;
	}
}
