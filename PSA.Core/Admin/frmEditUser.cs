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
	public partial class frmEditUser : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public string id = "";

		private DataTable post = new DataTable("Post");
		private DataTable point = new DataTable("Point");

		public frmEditUser(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Пользователь";
			this.db_connection = db_connection;
			this.usr = usr;
		}

		public frmEditUser(SqlConnection db_connection, UserInfo usr, string id)
		{
			InitializeComponent();
			this.Text = "Пользователь";
			this.db_connection = db_connection;
			this.usr = usr;
			this.id = id;
		}

		private void frmEditUser_Load(object sender, EventArgs e)
		{
			try
			{
				SqlCommand cc = new SqlCommand("SELECT [id_point], [name] FROM [vwPointList] ORDER BY [name]", db_connection);
				SqlDataAdapter ca = new SqlDataAdapter(cc);
				ca.Fill(point);
				DataRow rw;
				rw = point.NewRow();
				rw["name"] = "< ? не выбрано ? >";
				rw["id_point"] = 0;
				point.Rows.InsertAt(rw, 0);
				txtPoint.DataSource = point;
				txtPoint.DisplayMember = "name";
				txtPoint.ValueMember = "id_point";

				cc = new SqlCommand("SELECT [id_post], [name] FROM [vwPostList] ORDER BY [name]", db_connection);
				ca = new SqlDataAdapter(cc);
				ca.Fill(post);
				rw = post.NewRow();
				rw["name"] = "< ? не выбрано ? >";
				rw["id_post"] = 0;
				post.Rows.InsertAt(rw, 0);
				txtPost.DataSource = post;
				txtPost.DisplayMember = "name";
				txtPost.ValueMember = "id_post";

				if (id != "")
				{
					SqlCommand c =
						new SqlCommand(
							"SELECT CAST([del] AS BIT) AS [del], [id_post], [id_point], [name], [сontact], [login], [password], [permission] FROM [user] WHERE id_user = " +
							id, db_connection);
					SqlDataReader r = c.ExecuteReader();
					if (r.Read())
					{
						txtName.Enabled = false;
						txtLogin.Enabled = false;
						if (!r.IsDBNull(0))
							checkDel.Checked = r.GetBoolean(0);
						else
							checkDel.Checked = false;

						if (!r.IsDBNull(1))
							txtPost.SelectedValue = r.GetInt32(1);
						else
							txtPost.SelectedValue = 0;

						if (!r.IsDBNull(2))
							txtPoint.SelectedValue = r.GetInt32(2);
						else
							txtPoint.SelectedValue = 0;

						if (!r.IsDBNull(3))
							txtName.Text = r.GetString(3).Trim();
						else
							txtName.Text = "";

						if (!r.IsDBNull(4))
							txtContact.Text = r.GetString(4).Trim();
						else
							txtContact.Text = "";

						if (!r.IsDBNull(5))
							txtLogin.Text = r.GetString(5).Trim();
						else
							txtLogin.Text = "";

						if (!r.IsDBNull(6))
							txtPassword.Text = r.GetString(6).Trim();
						else
							txtPassword.Text = "";

						if (!r.IsDBNull(7))
						{
							decimal perm = r.GetDecimal(7);
							if ((((int)perm & 1) == 1))
								checkP1.Checked = true;
							else
								checkP1.Checked = false;

							if ((((int)perm & 2) == 2))
								checkP2.Checked = true;
							else
								checkP2.Checked = false;

							if ((((int)perm & 4) == 4))
								checkP3.Checked = true;
							else
								checkP3.Checked = false;

							if ((((int)perm & 8) == 8))
								checkP4.Checked = true;
							else
								checkP4.Checked = false;

							if ((((int)perm & 16) == 16))
								checkP5.Checked = true;
							else
								checkP5.Checked = false;

							if ((((int)perm & 32) == 32))
								checkP6.Checked = true;
							else
								checkP6.Checked = false;

							if ((((int)perm & 64) == 64))
								checkP7.Checked = true;
							else
								checkP7.Checked = false;

							if ((((int)perm & 128) == 128))
								checkP8.Checked = true;
							else
								checkP8.Checked = false;

							if ((((int)perm & 256) == 256))
								checkP9.Checked = true;
							else
								checkP9.Checked = false;

							if ((((int)perm & 512) == 512))
								checkP10.Checked = true;
							else
								checkP10.Checked = false;

							if ((((int)perm & 1024) == 1024))
								checkP11.Checked = true;
							else
								checkP11.Checked = false;

							if ((((int)perm & 2048) == 2048))
								checkP12.Checked = true;
							else
								checkP12.Checked = false;

							if ((((int)perm & 4096) == 4096))
								checkP13.Checked = true;
							else
								checkP13.Checked = false;

							if ((((int)perm & 8192) == 8192))
								checkP14.Checked = true;
							else
								checkP14.Checked = false;

							if ((((int)perm & 16384) == 16384))
								checkP15.Checked = true;
							else
								checkP15.Checked = false;
						}
						else
						{
							checkP1.Checked = false;
							checkP2.Checked = false;
							checkP3.Checked = false;
							checkP4.Checked = false;
							checkP5.Checked = false;
							checkP6.Checked = false;
							checkP7.Checked = false;
							checkP8.Checked = false;
							checkP9.Checked = false;
							checkP10.Checked = false;
							checkP11.Checked = false;
							checkP12.Checked = false;
							checkP13.Checked = false;
							checkP14.Checked = false;
							checkP15.Checked = false;
						}
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
					txtPost.SelectedValue = "0";
					txtPoint.SelectedValue = "0";
					txtName.Text = "";
					txtContact.Text = "";
					txtLogin.Text = "";
					txtPassword.Text = "";
					checkP1.Checked = false;
					checkP2.Checked = false;
					checkP3.Checked = false;
					checkP4.Checked = false;
					checkP5.Checked = false;
					checkP6.Checked = false;
					checkP7.Checked = false;
					checkP8.Checked = false;
					checkP9.Checked = false;
					checkP10.Checked = false;
					checkP11.Checked = false;
					checkP12.Checked = false;
					checkP13.Checked = false;
					checkP14.Checked = false;
					checkP15.Checked = false;
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
			if (txtPoint.SelectedValue.ToString() != "0")
			{
				try
				{
					if (id != "")
					{
						int d = checkDel.Checked ? 1 : 0;
						long perm = 0;
						perm ^= checkP1.Checked ? 1 : 0;
						perm ^= checkP2.Checked ? 2 : 0;
						perm ^= checkP3.Checked ? 4 : 0;
						perm ^= checkP4.Checked ? 8 : 0;
						perm ^= checkP5.Checked ? 16 : 0;
						perm ^= checkP6.Checked ? 32 : 0;
						perm ^= checkP7.Checked ? 64 : 0;
						perm ^= checkP8.Checked ? 128 : 0;
						perm ^= checkP9.Checked ? 256 : 0;
						perm ^= checkP10.Checked ? 512 : 0;
						perm ^= checkP11.Checked ? 1024 : 0;
						perm ^= checkP12.Checked ? 2048 : 0;
						perm ^= checkP13.Checked ? 4096 : 0;
						perm ^= checkP14.Checked ? 8192 : 0;
						perm ^= checkP15.Checked ? 16384 : 0;
						SqlCommand c =
							new SqlCommand(
								"UPDATE [user] SET [id_post] = " + txtPost.SelectedValue.ToString() + ", [id_point] = " +
								txtPoint.SelectedValue.ToString() + ", [del] = " + d.ToString() + ", [name] = '" + txtName.Text +
								"', [сontact] = '" + txtContact.Text + "', [login] = '" + txtLogin.Text + "', [password] = '" + txtPassword.Text +
								"', [permission] = " + perm.ToString() + " WHERE id_user = " + id, db_connection);
						c.ExecuteNonQuery();
					}
					else
					{
						long perm = 0;
						perm ^= checkP1.Checked ? 1 : 0;
						perm ^= checkP2.Checked ? 2 : 0;
						perm ^= checkP3.Checked ? 4 : 0;
						perm ^= checkP4.Checked ? 8 : 0;
						perm ^= checkP5.Checked ? 16 : 0;
						perm ^= checkP6.Checked ? 32 : 0;
						perm ^= checkP7.Checked ? 64 : 0;
						perm ^= checkP8.Checked ? 128 : 0;
						perm ^= checkP9.Checked ? 256 : 0;
						perm ^= checkP10.Checked ? 512 : 0;
						perm ^= checkP11.Checked ? 1024 : 0;
						perm ^= checkP12.Checked ? 2048 : 0;
						perm ^= checkP13.Checked ? 4096 : 0;
						perm ^= checkP14.Checked ? 8192 : 0;
						perm ^= checkP15.Checked ? 16384 : 0;
						SqlCommand c =
							new SqlCommand(
								"INSERT INTO [user] ([id_post], [id_point], [guid], [del], [name], [сontact], [login], [password], [permission]) VALUES (" +
								txtPost.SelectedValue.ToString() + ", " + txtPoint.SelectedValue.ToString() + ", '" +
								System.Guid.NewGuid().ToString() + "', 0, '" + txtName.Text + "', '" + txtContact.Text + "', '" + txtLogin.Text +
								"', '" + txtPassword.Text + "', " + perm.ToString() + ")", db_connection);
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