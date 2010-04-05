using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security.User;

namespace Photoland.Forms.Admin
{
	public partial class frmEditClient : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public string id = "";

		private DataTable cat = new DataTable("Category");


		public frmEditClient(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Клиент";
			this.db_connection = db_connection;
			this.usr = usr;
		}

		public frmEditClient(SqlConnection db_connection, UserInfo usr, string id)
		{
			InitializeComponent();
			this.Text = "Клиент";
			this.db_connection = db_connection;
			this.usr = usr;
			this.id = id;
		}

		private void frmEditClient_Load(object sender, EventArgs e)
		{
			try
			{
				SqlCommand cc = new SqlCommand("SELECT [id_category], [name] FROM [vwCategoryFull] ORDER BY [name]", db_connection);
				SqlDataAdapter ca = new SqlDataAdapter(cc);
				ca.Fill(cat);
				DataRow rw;
				rw = cat.NewRow();
				rw["name"] = "< ? не выбрано ? >";
				rw["id_category"] = 0;
				cat.Rows.InsertAt(rw, 0);
				txtCategory.DataSource = cat;
				txtCategory.DisplayMember = "name";
				txtCategory.ValueMember = "id_category";

				if (id != "")
				{
					SqlCommand c =
						new SqlCommand(
							"SELECT [id_category], CAST([del] AS BIT) AS [del], [name], [phone_1], [phone_2], [address], [email], [icq], [addon] FROM [client] WHERE [id_client] = " +
							id, db_connection);
					SqlDataReader r = c.ExecuteReader();
					if (r.Read())
					{
                        if (!r.IsDBNull(0))
                        {
                            txtCategory.SelectedValue = r.GetInt32(0);
                        }
                        else
                        {
                            txtCategory.SelectedValue = 0;
                        }

						if (!r.IsDBNull(1))
							checkDel.Checked = r.GetBoolean(1);
						else
							checkDel.Checked = false;

						if (!r.IsDBNull(2))
							txtName.Text = r.GetString(2).Trim();
						else
							txtName.Text = "";

						if (!r.IsDBNull(3))
							txtPhone1.Text = r.GetString(3).Trim();
						else
							txtPhone1.Text = "";

						if (!r.IsDBNull(4))
							txtPhone2.Text = r.GetString(4).Trim();
						else
							txtPhone2.Text = "";

						if (!r.IsDBNull(5))
							txtAddres.Text = r.GetString(5).Trim();
						else
							txtAddres.Text = "";

						if (!r.IsDBNull(6))
							txtEmail.Text = r.GetString(6).Trim();
						else
							txtEmail.Text = "";

						if (!r.IsDBNull(7))
							txtIcq.Text = r.GetString(7).Trim();
						else
							txtIcq.Text = "";

						if (!r.IsDBNull(8))
							txtAddon.Text = r.GetString(8).Trim();
						else
							txtAddon.Text = "";

					}
					else
					{
						MessageBox.Show("Ошибка при открытии записи!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					r.Close();
                    if (txtCategory.SelectedValue.ToString() != "0")
                    {
                        c = new SqlCommand("SELECT [id_category], [input] FROM [vwCategoryFull] WHERE [id_category] = " + txtCategory.SelectedValue.ToString(), db_connection);
                        r = c.ExecuteReader();
                        if (r.Read())
                        {
                            if (!r.IsDBNull(1))
                            {
                                if (r.GetInt32(1) != 1)
                                    txtCategory.Enabled = false;
                                else
                                    txtCategory.Enabled = true;
                            }
                        }
                        else
                        {
                            txtCategory.Enabled = false;
                        }
                        r.Close();
                    }
				}
				else
				{
					txtCategory.SelectedValue = 0;
					checkDel.Checked = false;
					txtName.Text = "";
					txtPhone1.Text = "";
					txtPhone2.Text = "";
					txtAddres.Text = "";
					txtEmail.Text = "";
					txtIcq.Text = "";
					txtAddon.Text = "";
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
			if (txtCategory.SelectedValue.ToString() != "0")
			{
				try
				{
					if (id != "")
					{
						int d = checkDel.Checked ? 1 : 0;
						SqlCommand c =
							new SqlCommand(
								"UPDATE [client] SET [id_category] = " + txtCategory.SelectedValue.ToString() + ", [del] = " + d +
								", [name] = '" +
								txtName.Text + "', [phone_1] = '" + txtPhone1.Text + "', [phone_2] = '" + txtPhone2.Text + "', [address] = '" +
								txtAddres.Text + "', [email] = '" + txtEmail.Text + "', [icq] = '" + txtIcq.Text + "', [addon] = '" +
								txtAddon.Text + "' WHERE id_client = " + id, db_connection);
						c.ExecuteNonQuery();
					}
					else
					{
						SqlCommand c =
							new SqlCommand(
								"INSERT INTO [client] ([id_category], [guid], [del], [name], [phone_1], [phone_2], [address], [email], [icq], [addon]) VALUES (" +
								txtCategory.SelectedValue.ToString() + ", '" + System.Guid.NewGuid().ToString() + "', 0, '" + txtName.Text +
								"', '" + txtPhone1.Text + "', '" + txtPhone2.Text + "', '" + txtAddres.Text + "', '" + txtEmail.Text + "', '" +
								txtIcq.Text + "', '" + txtAddon.Text + "')",
								db_connection);
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
			else
			{
				MessageBox.Show("Укажите категорию", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

	
	
	
	}
}