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
	public partial class frmEditPrice : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public string id = "";

		private DataTable price = new DataTable("Price");
		private DataTable cat = new DataTable("Category");


		public frmEditPrice(SqlConnection db_connection, UserInfo usr, string id)
		{
			InitializeComponent();
			this.Text = "Прайс";
			this.db_connection = db_connection;
			this.usr = usr;
			this.id = id;

			price.Columns.Add("id_good");
			price.Columns.Add("id_category");
			price.Columns.Add("name_category");
			price.Columns.Add("amount");
			price.Columns.Add("update");
			price.Columns.Add("id_price");
			price.Columns.Add("threshold");
			price.Columns.Add("amount2");
			price.Columns.Add("threshold2");
			price.Columns.Add("amount3");

		}

		private void frmEditPrice_Load(object sender, EventArgs e)
		{
			try
			{
				SqlCommand cgood = new SqlCommand("SELECT [id_good], [name] FROM [good] WHERE [id_good] = '" + this.id + "'", db_connection);
				SqlDataReader rgood;
				rgood = cgood.ExecuteReader();
				if (rgood.Read())
				{
					if (!rgood.IsDBNull(1))
					{
						lblGood.Text = rgood.GetString(1).Trim();
					}
				}
				rgood.Close();


				SqlCommand tmpc = new SqlCommand("SELECT [id_category], [name] FROM [vwCategoryFull] ORDER BY [name]", db_connection);
				SqlDataAdapter tmpa = new SqlDataAdapter(tmpc);
				tmpa.Fill(cat);

				if (id != "")
				{
					for(int i = 0; i<cat.Rows.Count; i++)
					{
						SqlCommand tmp = new SqlCommand("SELECT [id_price], [id_good], [id_category], [amount], [threshold], [amount2], [threshold2], [amount3], [ondate] FROM [price] WHERE [id_category] = " + cat.Rows[i][0].ToString() + " AND [id_good] = '" + id + "' ORDER BY [ondate]", db_connection);
						SqlDataReader r = tmp.ExecuteReader();
						while (r.Read())
						{
							DataRow rw;
							rw = price.NewRow();
							rw["id_good"] = id;
							rw["id_category"] = cat.Rows[i][0].ToString();
							rw["name_category"] = cat.Rows[i][1].ToString().Trim() + " " + r.GetDateTime(8).ToShortDateString();
							if (!r.IsDBNull(3))
								rw["amount"] = r.GetDecimal(3);
							else
								rw["amount"] = 0;

							if (!r.IsDBNull(4))
								rw["threshold"] = r.GetInt32(4);
							else
								rw["threshold"] = 0;

							if (!r.IsDBNull(5))
								rw["amount2"] = r.GetDecimal(5);
							else
								rw["amount2"] = 0;

							if (!r.IsDBNull(6))
								rw["threshold2"] = r.GetInt32(6);
							else
								rw["threshold2"] = 0;

                            if (!r.IsDBNull(7))
                                rw["amount3"] = r.GetDecimal(7);
                            else
                                rw["amount3"] = 0;

							rw["update"] = true;
							rw["id_price"] = r.GetInt32(0);
							price.Rows.Add(rw);
						}
                        /*
						else
						{
							DataRow rw;
							rw = price.NewRow();
							rw["id_good"] = id;
							rw["id_category"] = cat.Rows[i][0].ToString();
							rw["name_category"] = cat.Rows[i][1].ToString() + DateTime.Now.ToShortDateString();
							rw["amount"] = 0;
							rw["amount2"] = 0;
							rw["amount3"] = 0;
							rw["threshold"] = 0;
							rw["threshold2"] = 0;
							rw["update"] = false;
							rw["id_price"] = 0;
							price.Rows.Add(rw);
						}
                        */
						r.Close();
					}
					gridData.DataSource = price;
					for (int j = 1; j < gridData.Rows.Count; j++)
					{
						if (bool.Parse(gridData.Rows[j]["update"].ToString()))
						{
							gridData.Rows[j].Style = gridData.Styles["Green"];
						}
						else
						{
							gridData.Rows[j].Style = gridData.Styles["Red"];
						}
					}

					gridData.Cols[1].Visible = false;
					gridData.Cols[2].Visible = false;
					gridData.Cols[3].Visible = false;
					gridData.Cols[4].Visible = false;
					gridData.Cols[5].Visible = false;
					gridData.Cols[6].Visible = false;
					gridData.Cols[7].Visible = false;
					gridData.Cols[8].Visible = false;
					gridData.Cols[9].Visible = false;
					gridData.Cols[10].Visible = false;

					gridData.Cols[3].Visible = true;
					gridData.Cols[3].Width = 165;
					gridData.Cols[3].AllowDragging = false;
					gridData.Cols[3].AllowEditing = false;
					gridData.Cols[3].AllowMerging = false;
					gridData.Cols[3].AllowResizing = true;
					gridData.Cols[3].AllowSorting = true;
					gridData.Cols[3].Caption = "Категория";

					gridData.Cols[4].Visible = true;
					gridData.Cols[4].Width = 100;
					gridData.Cols[4].AllowDragging = false;
					gridData.Cols[4].AllowEditing = true;
					gridData.Cols[4].AllowMerging = false;
					gridData.Cols[4].AllowResizing = true;
					gridData.Cols[4].AllowSorting = true;
					gridData.Cols[4].DataType = Type.GetType("Decimal");
					gridData.Cols[4].Caption = "Стоимость 1";

					gridData.Cols[7].Visible = true;
					gridData.Cols[7].Width = 100;
					gridData.Cols[7].AllowDragging = false;
					gridData.Cols[7].AllowEditing = true;
					gridData.Cols[7].AllowMerging = false;
					gridData.Cols[7].AllowResizing = true;
					gridData.Cols[7].AllowSorting = true;
					gridData.Cols[7].Caption = "Порог 1";

					gridData.Cols[8].Visible = true;
					gridData.Cols[8].Width = 100;
					gridData.Cols[8].AllowDragging = false;
					gridData.Cols[8].AllowEditing = true;
					gridData.Cols[8].AllowMerging = false;
					gridData.Cols[8].AllowResizing = true;
					gridData.Cols[8].AllowSorting = true;
					gridData.Cols[8].DataType = Type.GetType("Decimal");
					gridData.Cols[8].Caption = "Стоимость 2";

					gridData.Cols[9].Visible = true;
					gridData.Cols[9].Width = 100;
					gridData.Cols[9].AllowDragging = false;
					gridData.Cols[9].AllowEditing = true;
					gridData.Cols[9].AllowMerging = false;
					gridData.Cols[9].AllowResizing = true;
					gridData.Cols[9].AllowSorting = true;
					gridData.Cols[9].Caption = "Порог 2";

					gridData.Cols[10].Visible = true;
					gridData.Cols[10].Width = 100;
					gridData.Cols[10].AllowDragging = false;
					gridData.Cols[10].AllowEditing = true;
					gridData.Cols[10].AllowMerging = false;
					gridData.Cols[10].AllowResizing = true;
					gridData.Cols[10].AllowSorting = true;
					gridData.Cols[10].DataType = Type.GetType("Decimal");
					gridData.Cols[10].Caption = "Стоимость 3";

				}
				else
				{
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
			btnOK.Focus();
			try
			{
				for (int i = 1; i < gridData.Rows.Count; i++)
				{
					if (bool.Parse(gridData.Rows[i]["update"].ToString()))
					{
                        SqlCommand tmp = new SqlCommand("SELECT CASE WHEN ISNULL([zero], -1) = -1 THEN 0 ELSE ISNULL(CAST(zero AS INT), 0) END AS zero FROM [good] WHERE [id_good] = '" + gridData.Rows[i]["id_good"].ToString() + "'", db_connection);
                        int z = (int)tmp.ExecuteScalar();
                        if ((decimal.Parse(gridData.Rows[i]["amount"].ToString()) > 0) || (z == 1))
						{
							int d = 0;
							SqlCommand c =
								new SqlCommand(
									"UPDATE [price] SET [id_good] = '" + gridData.Rows[i]["id_good"].ToString() + "', [id_category] = " +
									gridData.Rows[i]["id_category"].ToString() + ", [amount] = " +
									gridData.Rows[i]["amount"].ToString().Replace(",", ".") + ", [amount2] = " +
									gridData.Rows[i]["amount2"].ToString().Replace(",", ".") + ", [amount3] = " +
									gridData.Rows[i]["amount3"].ToString().Replace(",", ".") + ", [threshold] = " +
									gridData.Rows[i]["threshold"].ToString().Replace(",", ".") + ", [threshold2] = " +
									gridData.Rows[i]["threshold2"].ToString().Replace(",", ".") + " WHERE [id_price] = " +
									gridData.Rows[i]["id_price"].ToString(), db_connection);
							c.ExecuteNonQuery();
						}
						else
						{
							MessageBox.Show("Стоимость 1 не может быть 0!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
					else
					{
                        SqlCommand tmp = new SqlCommand("SELECT CASE WHEN ISNULL([zero], -1) = -1 THEN 0 ELSE ISNULL(CAST(zero AS INT), 0) END AS zero FROM [good] WHERE [id_good] = '" + gridData.Rows[i]["id_good"].ToString() + "'", db_connection);
                        int z = (int)tmp.ExecuteScalar();
                        if ((decimal.Parse(gridData.Rows[i]["amount"].ToString()) > 0) || (z == 1))
						{
							SqlCommand c =
								new SqlCommand(
									"INSERT INTO [price] ([id_good], [id_category], [guid], [del], [amount], [amount2], [amount3], [threshold], [threshold2]) VALUES ('" +
									gridData.Rows[i]["id_good"].ToString() + "', " + gridData.Rows[i]["id_category"].ToString() + ", '" +
									System.Guid.NewGuid().ToString() + "', 0, " + gridData.Rows[i]["amount"].ToString().Replace(",", ".") + ", " +
									gridData.Rows[i]["amount2"].ToString().Replace(",", ".") + ", " +
									gridData.Rows[i]["amount3"].ToString().Replace(",", ".") + ", " +
									gridData.Rows[i]["threshold"].ToString().Replace(",", ".") + ", " +
									gridData.Rows[i]["threshold2"].ToString().Replace(",", ".") + ")", db_connection);
							c.ExecuteNonQuery();
						}
						else
						{
							MessageBox.Show("Стоимость 1 не может быть 0!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
				}
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