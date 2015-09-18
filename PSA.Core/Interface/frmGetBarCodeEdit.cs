using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface
{
	public partial class frmGetBarCodeEdit : Form
	{
		public string barcode = "";

		public frmGetBarCodeEdit()
		{
			InitializeComponent();
		}

		private void frmGetBarCodeEdit_Load(object sender, EventArgs e)
		{

		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.barcode = txtBarCode.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void frmGetBarCodeEdit_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				this.barcode = txtBarCode.Text;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			else if(e.KeyChar == (char)Keys.Cancel)
			{
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			}
		}
	}
}
