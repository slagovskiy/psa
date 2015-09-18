using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface
{
	public partial class frmGetBarCode : PSA.Lib.Interface.Templates.frmTDialog
	{
		private string _barcode;
		public string BarCode
		{
			get
			{
				return _barcode;
			}
		}

		public frmGetBarCode()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void frmGetBarCode_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != (char)Keys.Return)
			{
				lblBarCode.Text += e.KeyChar.ToString();
			}
			else if (e.KeyChar == (char)Keys.Back)
			{
				lblBarCode.Text = lblBarCode.Text.Substring(0, lblBarCode.Text.Length - 1);
			}
			else if (e.KeyChar == (char)Keys.Return)
			{
				_barcode = lblBarCode.Text;
				this.DialogResult = DialogResult.OK;
			}

			e.Handled = true;
		}

		private void frmGetBarCode_Load(object sender, EventArgs e)
		{
			lblBarCode.Text = "";
			lblBarCode.Select();
		}
	}
}
