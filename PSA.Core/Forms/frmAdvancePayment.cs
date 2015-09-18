using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
	public partial class frmAdvancePayment : Form
	{
		public double max = 0;
		private double _payment;

		public double Payment
		{
			get { return _payment; }
			set { _payment = value; }
		}

		public frmAdvancePayment()
		{
			InitializeComponent();
			this.Text = "Предоплата";
		}

		public frmAdvancePayment(double max)
		{
			InitializeComponent();
			this.Text = "Предоплата";
			this.max = max;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.max > 0)
				{
					if (double.Parse(txtPayment.Text) > max)
					{
						MessageBox.Show("Платеж не может быть больше чем " + max.ToString() + "!");
						txtPayment.Text = "0";
					}
				}
				this._payment = double.Parse(txtPayment.Text);
				this.DialogResult = DialogResult.OK;
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				this._payment = 0;
				this.DialogResult = DialogResult.Cancel;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this._payment = 0;
			this.DialogResult = DialogResult.Cancel;
		}

		private void frmAdvancePayment_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
			{
				if (e.KeyChar == (char)Keys.Return)
				{
					this._payment = double.Parse(txtPayment.Text);
					this.DialogResult = DialogResult.OK;
					e.Handled = true;
				}
				else if (e.KeyChar == (char)Keys.Escape)
				{
					this._payment = 0;
					this.DialogResult = DialogResult.Cancel;
					e.Handled = true;
				}
				else if ((e.KeyChar == '.') || (e.KeyChar == ','))
				{
					if (txtPayment.Text.Length > 0)
					{
						if ((txtPayment.Text.IndexOf(',') > 0) || (txtPayment.Text.IndexOf('.') > 0))
						{
							e.Handled = true;
						}
						else
						{
							if (e.KeyChar == '.')
								e.KeyChar = ',';
							e.Handled = false;
						}
					}
					else
					{
						e.Handled = true;
					}
				}
				else
				{
					e.Handled = false;
				}
			}
			else
			{
				e.Handled = true;
			}
		}
	}
}