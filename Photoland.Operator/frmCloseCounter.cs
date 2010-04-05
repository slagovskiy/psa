using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Operator
{
	public partial class frmCloseCounter : PSA.Lib.Interface.Templates.frmTDialog
	{
		public frmCloseCounter()
		{
			InitializeComponent();
		}

		private void txtCounter_ValueChanged(object sender, EventArgs e)
		{
			if (txtCounter.Value > 0)
			{
				btnSave.Enabled = true;
			}
			else
			{
				btnSave.Enabled = false;
			}
		}

		private void txtCounter_KeyUp(object sender, KeyEventArgs e)
		{
			if (txtCounter.Value > 0)
			{
				btnSave.Enabled = true;
			}
			else
			{
				btnSave.Enabled = false;
			}
		}

		private void frmCloseCounter_Load(object sender, EventArgs e)
		{
			this.Title = "Закрытие счетчика";
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

	}
}
