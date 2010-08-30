using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Forms.Interface;
using Photoland.Security.Discont;

namespace Photoland.Forms.Interface
{
	public partial class frmGetDiscont : Form
	{
		public SqlConnection db_connection;
		public DiscontInfo discont;
		private Util.Settings prop = new Util.Settings();

		public frmGetDiscont()
		{
			InitializeComponent();
			this.Text = "���������� ����� ��������";
		}

		public frmGetDiscont(SqlConnection db_connection)
		{
			InitializeComponent();
			this.Text = "���������� ����� ��������";
			this.db_connection = db_connection;
		}

		private void frmGetDiscont_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != (char)Keys.Return)
			{
				txtDiscontNo.Text += e.KeyChar.ToString();
			}
			else
			{
				discont = new DiscontInfo(txtDiscontNo.Text, db_connection);
				if ((discont.Id_dcard != 999999999) && (discont.Id_dcard > 0))
				{
					if (MessageBox.Show("��������!\n\n������ �� ���� ����� ����� �� ��������� ���� �.�. �������� ������ �� ��������!\n���� ���� ����������� ������� ����� � ��������� ���, �� ����� ��� ������� �� ����� �.�. �������� �� ��� �� �������.\n\n������������ ��� ������?", "������ ��������� ���������� � �����", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
					{
						discont = new DiscontInfo();
					}
				}
				this.DialogResult = DialogResult.OK;
			}

			e.Handled = true;
		}

		private void btnOther_Click(object sender, EventArgs e)
		{
			frmOtherDiscont fOtherDiscont = new frmOtherDiscont(db_connection);
			fOtherDiscont.ShowDialog();
			if (fOtherDiscont.DialogResult == DialogResult.OK)
			{
				discont = new DiscontInfo("9999999999999", fOtherDiscont.pers);
				this.DialogResult = DialogResult.OK;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (txtDiscontNo.Text != "")
			{
				SqlCommand tmp_cnt = new SqlCommand("DECLARE @C int;  SET @C = (SELECT cnt FROM dbo.dcarduse WHERE (code = '" + txtDiscontNo.Text.Trim() + "') AND (lastdate >= CONVERT(DATETIME, '" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + " 00:00:00', 102)) AND (lastdate <= CONVERT(DATETIME, '" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + " 23:59:00', 102))); SELECT ISNULL(@C, 0) AS cnt;", db_connection);
				int cnt = (int)tmp_cnt.ExecuteScalar();
				discont = new DiscontInfo(txtDiscontNo.Text, db_connection);
				if ((discont.Id_dcard != 999999999) && (discont.Id_dcard > 0))
				{
					if (MessageBox.Show("��������!\n\n������ �� ���� ����� ����� �� ��������� ���� �.�. �������� ������ �� ��������!\n���� ���� ����������� ������� ����� � ��������� ���, �� ����� ��� ������� �� �����.\n\n������������ ��� ������?", "������ ��������� ���������� � �����", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
					{
						discont = new DiscontInfo();
					}
				}
				if ((cnt <= prop.DCard_limit) & (discont.BonusType.Trim() != "Z"))
				{
					this.DialogResult = DialogResult.OK;
				}
				else
				{
					MessageBox.Show("��� ����� ��� ���������� ������� ����� � �� ����� ���� ������������!", "��������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.DialogResult = DialogResult.Cancel;
				}
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}