using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
	public partial class frmFinalPayment : Form
	{
		public decimal size = 0;

		private decimal _payment;
		public decimal Payment
		{
			get { return _payment; }
			set { _payment = value; }
		}
		
		public frmFinalPayment()
		{
			InitializeComponent();
			this.Text = "Окончательный расчет";
		}

		public frmFinalPayment(decimal size)
		{
			InitializeComponent();
			this.Text = "Окончательный расчет";
			txtPayment.Text = size.ToString();
			this.size = size;
		}

		private void frmFinalPayment_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
			{
				if (e.KeyChar == (char)Keys.Return)
				{
					if (decimal.Parse(txtPayment.Text) < size)
					{
						MessageBox.Show("Закрывающий платеж не может быть меньше чем " + size.ToString() + "!", "Ошибка",
						                MessageBoxButtons.OK, MessageBoxIcon.Warning);
						e.Handled = true;
					}
					else
					{
						this._payment = decimal.Parse(txtPayment.Text);
						this.DialogResult = DialogResult.OK;
						e.Handled = true;
					}
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

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (decimal.Parse(txtPayment.Text) < size)
			{
				MessageBox.Show("Закрывающий платеж не может быть меньше чем " + size.ToString() + "!", "Ошибка",
								MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				this._payment = decimal.Parse(txtPayment.Text);
				this.DialogResult = DialogResult.OK;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this._payment = 0;
			this.DialogResult = DialogResult.Cancel;
		}
	}
}