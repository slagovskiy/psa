using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Operator
{
	public partial class frmMomentalCounter : PSA.Lib.Interface.Templates.frmTDialog
	{
		public frmMomentalCounter()
		{
			InitializeComponent();
		}

		public frmMomentalCounter(long count)
		{
			InitializeComponent();
			txtCounter.Value = count;
		}

		private void frmMomentalCounter_Load(object sender, EventArgs e)
		{
			this.Title = "Счетчик принтера";
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
