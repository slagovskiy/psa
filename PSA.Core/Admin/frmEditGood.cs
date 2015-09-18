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
	public partial class frmEditGood : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public string id = "";

		private DataTable form = new DataTable("Form");
		private DataTable tp = new DataTable("Type");

		public frmEditGood(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Товар/Услуга";
			this.db_connection = db_connection;
			this.usr = usr;
		}

		public frmEditGood(SqlConnection db_connection, UserInfo usr, string id)
		{
			InitializeComponent();
			this.Text = "Товар/Услуга";
			this.db_connection = db_connection;
			this.usr = usr;
			this.id = id;
		}

		private void frmEditGood_Load(object sender, EventArgs e)
		{
			form.Columns.Add("id");
			form.Columns.Add("name");
			DataRow row;
			row = form.NewRow();
			row["id"] = "";
			row["name"] = "Запросить количество";
			form.Rows.Add(row);
			row = form.NewRow();
			row["id"] = "00001";
			row["name"] = "Применить к файлам";
			form.Rows.Add(row);
			row = form.NewRow();
			row["id"] = "00002";
			row["name"] = "Применить к кадрам";
			form.Rows.Add(row);
			row = form.NewRow();
			row["id"] = "00003";
			row["name"] = "Заказы для Lambda";
			form.Rows.Add(row);
			txtForm.DataSource = form;
			txtForm.DisplayMember = "name";
			txtForm.ValueMember = "id";

			tp.Columns.Add("id");
			tp.Columns.Add("name");
			row = tp.NewRow();
			row["id"] = "0";
			row["name"] = "< ! не выбрано ! >";
			tp.Rows.Add(row);
			row = tp.NewRow();
			row["id"] = "1";
			row["name"] = "Печать оператором";
			tp.Rows.Add(row);
			row = tp.NewRow();
			row["id"] = "2";
			row["name"] = "Обработка дизайнером";
			tp.Rows.Add(row);
			txtType.DataSource = tp;
			txtType.DisplayMember = "name";
			txtType.ValueMember = "id";



			try
			{
				if (id != "")
				{
					SqlCommand c =
						new SqlCommand(
                            "SELECT [id_good], CAST([del] AS BIT) AS [del], [name], [description], [prefix], [folder], [type], CAST([checked] AS BIT) AS [checked], [sign], [apply_form], [EI], CAST([zero] AS BIT) AS [zero] FROM [good] WHERE [id_good] = '" +
							id + "'", db_connection);
					SqlDataReader r = c.ExecuteReader();
					/* 0  [id_good], 
					 * 1  [del], 
					 * 2  [name], 
					 * 3  [description], 
					 * 4  [prefix], 
					 * 5  [folder], 
					 * 6  [type], 
					 * 7  [checked], 
					 * 8  [sign], 
					 * 9  [apply_form], 
					 * 10 [EI]
					 */
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
							txtDesc.Text = r.GetString(3).Trim();
						else
							txtDesc.Text = "";

						if (!r.IsDBNull(4))
							txtPrefix.Text = r.GetString(4).Trim();
						else
							txtPrefix.Text = "";

						if (!r.IsDBNull(5))
							txtFolder.Text = r.GetString(5).Trim();
						else
							txtFolder.Text = "";

						if (!r.IsDBNull(6))
							txtType.SelectedValue = r.GetString(6).Trim();
						else
							txtType.SelectedValue = "0";

						if (!r.IsDBNull(7))
							checkFolder.Checked = r.GetBoolean(7);
						else
							checkFolder.Checked = false;

						if (!r.IsDBNull(8))
							txtSign.Text = r.GetString(8).Trim();
						else
							txtSign.Text = "";

						if (!r.IsDBNull(9))
							txtForm.SelectedValue = r.GetString(9).Trim();
						else
							txtForm.SelectedValue = "";

                        if (!r.IsDBNull(10))
                            txtEI.Text = r.GetDecimal(10).ToString();
                        else
                            txtEI.Text = "0";

                        if (!r.IsDBNull(11))
                            checkZero.Checked = r.GetBoolean(11);
                        else
                            checkZero.Checked = false;

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
					txtCode1C.Text = "";
					txtDesc.Text = "";
					txtPrefix.Text = "";
					txtFolder.Text = "";
					txtType.SelectedValue = "0";
					checkFolder.Checked = false;
					txtSign.Text = "";
					txtForm.SelectedValue = "";
					txtEI.Text = "0";
                    checkZero.Checked = false;
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
					int f = checkFolder.Checked ? 1 : 0;
                    int z = checkZero.Checked ? 1 : 0;
					SqlCommand c =
						new SqlCommand(
                            "UPDATE [good] SET [id_good] = '" + txtCode1C.Text + "' ,[del] = " + d.ToString() + " ,[zero] = " + z.ToString() + ", [name] = '" + txtName.Text + "', [description] = '" + txtDesc.Text + "', [prefix] = '" + txtPrefix.Text + "', [folder] = '" + txtFolder.Text + "', [type] = '" + txtType.SelectedValue.ToString() + "', [checked] = " + f.ToString() + ", [sign] = '" + txtSign.Text + "', [apply_form] = '" + txtForm.SelectedValue.ToString() + "', [EI] = " + txtEI.Text.Replace(",", ".") + " WHERE [id_good] = '" + id + "'", db_connection);
					c.ExecuteNonQuery();
				}
				else
				{
					int f = checkFolder.Checked ? 1 : 0;
                    int z = checkZero.Checked ? 1 : 0;
					SqlCommand c =
						new SqlCommand(
                            "INSERT INTO [good] ([id_good], [guid], [del], [zero], [name], [description], [prefix], [folder], [type], [checked], [sign], [apply_form], [EI]) VALUES ('" + txtCode1C.Text + "', '" + System.Guid.NewGuid().ToString() + "', 0, " + z.ToString() + ", '" + txtName.Text + "', '" + txtDesc.Text + "', '" + txtPrefix.Text + "', '" + txtFolder.Text + "', '" + txtType.SelectedValue.ToString() + "', " + f.ToString() + ", '" + txtSign.Text + "', '" + txtForm.SelectedValue.ToString() + "', " + txtEI.Text.Replace(",", ".") + ")", db_connection);
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

		private void btnHelpByForm_Click(object sender, EventArgs e)
		{
			frmHelpGoodForm f = new frmHelpGoodForm(txtForm.SelectedValue.ToString().Trim());
			f.ShowDialog();
		}


	}
}