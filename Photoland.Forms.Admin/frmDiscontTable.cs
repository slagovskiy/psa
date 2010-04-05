using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Components.FilterRow;
using Photoland.Security;
using Photoland.Security.User;

namespace Photoland.Forms.Admin
{
	public partial class frmDiscontTable : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tbl = new DataTable("data");
		private FilterRow FilterR;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		public frmDiscontTable(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Дисконтные карты";

			this.db_connection = db_connection;
			this.usr = usr;
		}

		private void frmDiscontTable_Load(object sender, EventArgs e)
		{
			FilterR = new FilterRow(gridData);

			LoadData();
		}


		private void LoadData()
		{
			tbl.Rows.Clear();

			db_command = new SqlCommand("SELECT [id_dcard], [del], [code], [name], [disc], [discserv] FROM [vwAdminDCard] ORDER BY [code]", db_connection);
			db_adapter = new SqlDataAdapter(db_command);
			db_adapter.Fill(tbl);

			gridData.DataSource = tbl;

			gridData.Cols[1].Visible = false;
			gridData.Cols[2].Visible = false;
			gridData.Cols[3].Visible = false;
			gridData.Cols[4].Visible = false;
			gridData.Cols[5].Visible = false;
			gridData.Cols[5].Visible = false;

			gridData.Cols[3].Visible = true;
			gridData.Cols[3].Width = 200;
			gridData.Cols[3].AllowDragging = false;
			gridData.Cols[3].AllowEditing = false;
			gridData.Cols[3].AllowMerging = false;
			gridData.Cols[3].AllowResizing = true;
			gridData.Cols[3].AllowSorting = true;
			gridData.Cols[3].Caption = "Код";

			gridData.Cols[4].Visible = true;
			gridData.Cols[4].Width = 545;
			gridData.Cols[4].AllowDragging = false;
			gridData.Cols[4].AllowEditing = false;
			gridData.Cols[4].AllowMerging = false;
			gridData.Cols[4].AllowResizing = true;
			gridData.Cols[4].AllowSorting = true;
			gridData.Cols[4].Caption = "Имя";

			gridData.Cols[5].Visible = true;
			gridData.Cols[5].Width = 100;
			gridData.Cols[5].AllowDragging = false;
			gridData.Cols[5].AllowEditing = false;
			gridData.Cols[5].AllowMerging = false;
			gridData.Cols[5].AllowResizing = true;
			gridData.Cols[5].AllowSorting = true;
			gridData.Cols[5].Caption = "Товары";

			gridData.Cols[6].Visible = true;
			gridData.Cols[6].Width = 100;
			gridData.Cols[6].AllowDragging = false;
			gridData.Cols[6].AllowEditing = false;
			gridData.Cols[6].AllowMerging = false;
			gridData.Cols[6].AllowResizing = true;
			gridData.Cols[6].AllowSorting = true;
			gridData.Cols[6].Caption = "Услуги";

			// выделяеем цветом удаленые записи
			for (int i = 2; i < gridData.Rows.Count; i++)
			{
				if ((gridData.GetData(i, 2).ToString().Trim() == "1") || (gridData.GetData(i, 2).ToString().Trim() == "True"))
				{
					gridData.Rows[i].Style = gridData.Styles["Red"];
				}
			}


		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				frmEditDiscont f = new frmEditDiscont(db_connection, usr);
				f.ShowDialog();
				if (f.DialogResult == DialogResult.OK)
					LoadData();
				f.Close();
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show(
					"Во время работы произошла ошибка.\nИнформация для разработчика:\n" + ex.Message + "\n" + ex.Source, "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				if (gridData.Rows[gridData.Row][1].ToString() != "")
				{
					frmEditDiscont f = new frmEditDiscont(db_connection, usr, gridData.Rows[gridData.Row][1].ToString());
					f.ShowDialog();
					if (f.DialogResult == DialogResult.OK)
						LoadData();
					f.Close();
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("Удалить эту запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
				{
					SqlCommand c =
						new SqlCommand(
							"UPDATE [dcard] SET [del] = 1" +
							" WHERE id_dcard = " + gridData.Rows[gridData.Row][1].ToString(), db_connection);
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
				LoadData();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}


	}
}