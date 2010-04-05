using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Exchanger
{
	public partial class frmAskDialog : Form
	{
		public frmAskDialog(string Text)
		{
			InitializeComponent();
			lbl.Text = Text;
		}

		private void frmAskDialog_Load(object sender, EventArgs e)
		{
			
		}

		private void btnYes_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void btnYesAll_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Yes;
		}

		private void btnNo_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.No;
		}

		private void btnNoAll_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Abort;
		}
	}
}