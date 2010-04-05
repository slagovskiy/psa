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
	public partial class frmEditODiscont : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public string id = "";

		public frmEditODiscont(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Дисконтная карта";
			this.db_connection = db_connection;
			this.usr = usr;
		}

		public frmEditODiscont(SqlConnection db_connection, UserInfo usr, string id)
		{
			InitializeComponent();
			this.Text = "Дисконтная карта";
			this.db_connection = db_connection;
			this.usr = usr;
			this.id = id;
		}

		private void frmEditODiscont_Load(object sender, EventArgs e)
		{
			try
			{
				if (id != "")
				{
					SqlCommand c =
						new SqlCommand(
							"SELECT [id_odcard], CAST([del] AS BIT) AS [del], [name], [percent] FROM [vwAdminODCard] WHERE [id_odcard] = " +
							id, db_connection);
					SqlDataReader r = c.ExecuteReader();
					if (r.Read())
					{
						if (!r.IsDBNull(1))
							checkDel.Checked = r.GetBoolean(1);
						else
							checkDel.Checked = false;

						if (!r.IsDBNull(2))
							txtName.Text = r.GetString(2).Trim();
						else
							txtName.Text = "";

						if (!r.IsDBNull(3))
							txtDiscserv.Text = r.GetDecimal(3).ToString();
						else
							txtDiscserv.Text = "0";
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
					txtName.Text = "";
					txtDiscserv.Text = "0";
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
							"UPDATE [odcard] SET [del] = " + d + ", [name] = '" + txtName.Text + "', [percent] = " + txtDiscserv.Text.Replace(",", ".") + " WHERE [id_odcard] = " + id, db_connection);
					c.ExecuteNonQuery();
				}
				else
				{
					SqlCommand c =
						new SqlCommand(
							"INSERT INTO [odcard] ([guid], [del], [name], [percent]) VALUES ('" + System.Guid.NewGuid().ToString() +
							"', 0, '" + txtName.Text +
							"', " + txtDiscserv.Text.Replace(",", ".") + ")", db_connection);
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