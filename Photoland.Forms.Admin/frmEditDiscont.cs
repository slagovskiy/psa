using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using Photoland.Security.User;

namespace Photoland.Forms.Admin
{
	public partial class frmEditDiscont : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public string id = "";

		public frmEditDiscont(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Дисконтная карта";
			this.db_connection = db_connection;
			this.usr = usr;
		}

		public frmEditDiscont(SqlConnection db_connection, UserInfo usr, string id)
		{
			InitializeComponent();
			this.Text = "Дисконтная карта";
			this.db_connection = db_connection;
			this.usr = usr;
			this.id = id;
		}

		private void frmEditDiscont_Load(object sender, EventArgs e)
		{
			try
			{
				if (id != "")
				{
					SqlCommand c =
						new SqlCommand(
							"SELECT [id_dcard], CAST([del] AS BIT) AS [del], [code], [name], [disc], [discserv], [phone], [email] FROM [dcard] WHERE [id_dcard] = " +
							id, db_connection);
					SqlDataReader r = c.ExecuteReader();
					if (r.Read())
					{
						if (!r.IsDBNull(1))
							checkDel.Checked = r.GetBoolean(1);
						else
							checkDel.Checked = false;

						if (!r.IsDBNull(2))
							txtCode.Text = r.GetString(2).Trim();
						else
							txtCode.Text = "";

						if (!r.IsDBNull(3))
							txtName.Text = r.GetString(3).Trim();
						else
							txtName.Text = "";

						if (!r.IsDBNull(4))
							txtDisc.Text = r.GetDecimal(4).ToString();
						else
							txtDisc.Text = "0";

						if (!r.IsDBNull(5))
							txtDiscserv.Text = r.GetDecimal(5).ToString();
						else
							txtDiscserv.Text = "0";

						if (!r.IsDBNull(6))
							txtPhone.Text = r.GetString(6).Trim();
						else
							txtPhone.Text = "";

						if (!r.IsDBNull(7))
							txtEmail.Text = r.GetString(7).Trim();
						else
							txtEmail.Text = "";
					}
					else
					{
						MessageBox.Show("Ошибка при открытии записи!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					r.Close();
				}
				else
				{
					checkDel.Checked = false;
					txtCode.Text = "";
					txtName.Text = "";
					txtDisc.Text = "0";
					txtDiscserv.Text = "0";
					txtPhone.Text = "";
					txtEmail.Text = "";
				}
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show(
					"Во время работы произошла ошибка.\nИнформация для разработчика:\n" + ex.Message + "\n" + ex.Source, "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (id != "")
				{
					int d = checkDel.Checked ? 1 : 0;
					SqlCommand c =
						new SqlCommand(
							"UPDATE [dcard] SET [del] = " + d + ", [code] = '" + txtCode.Text + "', [name] = '" + txtName.Text + "', [disc] = " + txtDisc.Text.Replace(",", ".") + ", [discserv] = " + txtDiscserv.Text.Replace(",", ".") + ", [phone] = '" + txtPhone.Text + "', [email] = '" + txtEmail.Text + "' WHERE [id_dcard] = " + id, db_connection);
					c.ExecuteNonQuery();
				}
				else
				{
					SqlCommand c =
						new SqlCommand(
							"INSERT INTO [dcard] ([guid], [del], [code], [name], [disc], [discserv], [phone], [email]) VALUES ('" + System.Guid.NewGuid().ToString() + "', 0, '" + txtCode.Text + "', '" + txtName.Text +
							"', " + txtDisc.Text.Replace(",", ".") + ", " + txtDiscserv.Text.Replace(",", ".") + ", '" + txtPhone.Text +
							"', '" + txtEmail.Text + "')", db_connection);
					c.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show(
					"Во время работы произошла ошибка.\nИнформация для разработчика:\n" + ex.Message + "\n" + ex.Source, "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				this.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
		}


	}
}