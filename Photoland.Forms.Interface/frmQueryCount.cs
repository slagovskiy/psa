using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
	public partial class frmQueryCount : Form
	{
		private decimal _count = 0;
		public decimal Count
		{
			get { return _count; }
			set { _count = value; }
		}

		public frmQueryCount()
		{
			InitializeComponent();
			this.Text = "Количество?";
		}

		private void frmQueryCount_KeyPress(object sender, KeyPressEventArgs e)
		{

			if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
			{
				if (e.KeyChar == (char)Keys.Return)
				{
					try
					{
						_count = decimal.Parse(txtCount.Text);
						this.DialogResult = DialogResult.OK;
						e.Handled = true;
					}
					catch(Exception ex)
					{
						ErrorNfo.WriteErrorInfo(ex);
					}
				}
				else if (e.KeyChar == (char)Keys.Escape)
				{
					_count = 0;
					this.DialogResult = DialogResult.Cancel;
					e.Handled = true;
				}
				else if ((e.KeyChar == '.') || (e.KeyChar == ','))
				{
					if (txtCount.Text.Length > 0)
					{
						if ((txtCount.Text.IndexOf(',') > 0) || (txtCount.Text.IndexOf('.') > 0))
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
			try
			{
				_count = decimal.Parse(txtCount.Text);
				this.DialogResult = DialogResult.OK;
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			_count = 0;
			this.DialogResult = DialogResult.Cancel;
		}

        private void frmQueryCount_Load(object sender, EventArgs e)
        {
            txtCount.Text = Count.ToString();

        }
	}
}