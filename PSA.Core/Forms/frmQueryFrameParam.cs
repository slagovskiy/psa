using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Interface;

namespace Photoland.Forms.Interface
{
	public partial class frmQueryFrameParam : Form
	{

		public frmQueryFrameParam()
		{
			InitializeComponent();
			this.Text = "Параметры заказа";
		}

		private void frmQueryFrameParam_Load(object sender, EventArgs e)
		{

		}

		private void Calc()
		{
			try
			{
				txtS.Text = (decimal.Parse(txtH.Text) * decimal.Parse(txtW.Text)).ToString();
			}
			catch(Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
			}
		}

		private void txtW_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
			{
				if ((e.KeyChar == '.') || (e.KeyChar == ','))
				{
					if (txtW.Text.Length > 0)
					{
						if ((txtW.Text.IndexOf(',') > 0) || (txtW.Text.IndexOf('.') > 0))
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
			Calc();
		}

		private void txtH_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
			{
				if ((e.KeyChar == '.') || (e.KeyChar == ','))
				{
					if (txtW.Text.Length > 0)
					{
						if ((txtH.Text.IndexOf(',') > 0) || (txtH.Text.IndexOf('.') > 0))
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
			Calc();
		}

		private void txtW_TextChanged(object sender, EventArgs e)
		{
			Calc();
		}

		private void txtH_TextChanged(object sender, EventArgs e)
		{
			Calc();
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
            try
            {
                if ((decimal.Parse(txtS.Text) > 0) && (txtFile.Text != ""))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            catch(Exception ex)
            {
				ErrorNfo.WriteErrorInfo(ex);
            }
		}

		private void btnSelectFile_Click(object sender, EventArgs e)
		{
			using (frmSelectLamdaFile frm = new frmSelectLamdaFile())
			{
				frm.path = lblPath.Text;
				if (frm.ShowDialog() == DialogResult.OK)
					txtFile.Text = frm.SelectedFile;
			}
		}

	}
}