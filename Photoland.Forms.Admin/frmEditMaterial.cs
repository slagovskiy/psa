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
	public partial class frmEditMaterial : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public string id = "";

		public frmEditMaterial(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Материал";
			this.db_connection = db_connection;
			this.usr = usr;
		}

		public frmEditMaterial(SqlConnection db_connection, UserInfo usr, string id)
		{
			InitializeComponent();
			this.Text = "Материал";
			this.db_connection = db_connection;
			this.usr = usr;
			this.id = id;
		}

		private void frmEditMaterial_Load(object sender, EventArgs e)
		{
			try
			{
				if (id != "")
				{
					SqlCommand c =
						new SqlCommand(
							"SELECT [id_material], CAST([del] AS BIT) AS [del], [material], [remainder] FROM [material] WHERE [id_material] = '" +
							id + "'", db_connection);
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

						if (!r.IsDBNull(0))
							txtCode1C.Text = r.GetString(0).Trim();
						else
							txtCode1C.Text = "";

						if (!r.IsDBNull(3))
							txtCount.Text = r.GetDecimal(3).ToString();
						else
							txtCount.Text = "";
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
					checkDel.Enabled = false;
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
							"UPDATE [material] SET [id_material] = '" + txtCode1C.Text + "', [del] = " + d.ToString() + ", [material] = '" + txtName.Text + "', [remainder] = " + txtCount.Text.Replace(",", ".") + " WHERE [id_material] = '" + id + "'", db_connection);
					c.ExecuteNonQuery();
				}
				else
				{
					SqlCommand c =
						new SqlCommand(
							"INSERT INTO [material] ([id_material], [del], [material], [remainder]) VALUES ('" + txtCode1C.Text + "', 0, '" + txtName.Text + "', " + txtCount.Text.Replace(",", ".") + ")", db_connection);
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