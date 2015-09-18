using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
	public partial class frmSelectWork : Form
	{
		public int rez = 0;

		public frmSelectWork()
		{
			InitializeComponent();
		}

		private void frmSelectWork_Load(object sender, EventArgs e)
		{

		}

		private void btnDesing_Click(object sender, EventArgs e)
		{
			rez = 1;
			DialogResult = DialogResult.OK;
		}

		private void btnOperator_Click(object sender, EventArgs e)
		{
			rez = 2;
			DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			rez = 0;
			DialogResult = DialogResult.Cancel;
		}
	}
}
