using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
    public partial class frmWaitConnection : Form
    {
		private int cnt = 5;

		public frmWaitConnection()
		{
			InitializeComponent();
		}

		private void frmWaitConnection_Load(object sender, EventArgs e)
		{
			tmr.Start();
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			if (cnt > 1)
			{
				cnt--;
				txtInfo.Text = cnt.ToString();
			}
			else
			{
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Прервать попытки соединения с базой?\nВсе не сохраненные данные будут утеряны!", "Ошибка соединения с базой", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
			{
				DialogResult = DialogResult.Cancel;
				this.Close();
			}
		}
    }
}
