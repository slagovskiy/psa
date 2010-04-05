using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface
{
	public partial class frmSemaphores : PSA.Lib.Interface.Templates.frmTDialog
	{
		public frmSemaphores()
		{
			InitializeComponent();
			this.Title = "Семафоры";
		}

		private void CheckRemoteSetupAccess()
		{
			if (checkRemoteSetup.Checked)
			{
				txtRemoteSetupPath.Enabled = true;
				btnRemotePath.Enabled = true;
			}
			else
			{
				txtRemoteSetupPath.Enabled = false;
				btnRemotePath.Enabled = false;
			}
		}

		private void checkRemoteSetup_CheckedChanged(object sender, EventArgs e)
		{
			CheckRemoteSetupAccess();
		}

		private void btnRemotePath_Click(object sender, EventArgs e)
		{
			dlg.ShowDialog();
			if (dlg.SelectedPath != "")
				txtRemoteSetupPath.Text = dlg.SelectedPath;
		}

		private void frmSemaphores_Load(object sender, EventArgs e)
		{
			if(PSA.Lib.Util.Semaphore.semRemoteSettings != "")
			{
				txtRemoteSetupPath.Text = PSA.Lib.Util.Semaphore.semRemoteSettings;
				checkRemoteSetup.Checked = true;
			}
			else
			{
				checkRemoteSetup.Checked = false;
			}
			checkInventory.Checked = PSA.Lib.Util.Semaphore.semInventory;
			CheckRemoteSetupAccess();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if((checkRemoteSetup.Checked) && (txtRemoteSetupPath.Text != ""))
			{
				PSA.Lib.Util.Semaphore.semRemoteSettings = txtRemoteSetupPath.Text;
			}
			PSA.Lib.Util.Semaphore.semInventory = checkInventory.Checked;
			this.Close();
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}
	}
}
