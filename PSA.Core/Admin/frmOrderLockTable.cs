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
	public partial class frmOrderLockTable : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tbl = new DataTable("data");
		private FilterRow FilterR;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		public frmOrderLockTable(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Блокировки на заказах";

			this.db_connection = db_connection;
			this.usr = usr;
		}

		private void frmOrderLockTable_Load(object sender, EventArgs e)
		{
			FilterR = new FilterRow(gridData);

			LoadData();
		}

		private void LoadData()
		{
			tbl.Rows.Clear();

			db_command = new SqlCommand("SELECT [id_order], [id_user_operator], [number], [id_user_designer], [name_operator], [name_designer], [status], [statuss] FROM [vwAdminLockOrder] ORDER BY [number]", db_connection);
			db_adapter = new SqlDataAdapter(db_command);
			db_adapter.Fill(tbl);

			gridData.DataSource = tbl;

			gridData.Cols[1].Visible = false;
			gridData.Cols[2].Visible = false;
			gridData.Cols[3].Visible = false;
			gridData.Cols[4].Visible = false;
			gridData.Cols[5].Visible = false;
			gridData.Cols[6].Visible = false;
			gridData.Cols[7].Visible = false;
			gridData.Cols[8].Visible = false;

			gridData.Cols[3].Visible = true;
			gridData.Cols[3].Width = 120;
			gridData.Cols[3].AllowDragging = false;
			gridData.Cols[3].AllowEditing = false;
			gridData.Cols[3].AllowMerging = false;
			gridData.Cols[3].AllowResizing = true;
			gridData.Cols[3].AllowSorting = true;
			gridData.Cols[3].Caption = "Номер";

			gridData.Cols[5].Visible = true;
			gridData.Cols[5].Width = 300;
			gridData.Cols[5].AllowDragging = false;
			gridData.Cols[5].AllowEditing = false;
			gridData.Cols[5].AllowMerging = false;
			gridData.Cols[5].AllowResizing = true;
			gridData.Cols[5].AllowSorting = true;
			gridData.Cols[5].Caption = "Оператор";

			gridData.Cols[6].Visible = true;
			gridData.Cols[6].Width = 300;
			gridData.Cols[6].AllowDragging = false;
			gridData.Cols[6].AllowEditing = false;
			gridData.Cols[6].AllowMerging = false;
			gridData.Cols[6].AllowResizing = true;
			gridData.Cols[6].AllowSorting = true;
			gridData.Cols[6].Caption = "Дизайнер";

			gridData.Cols[8].Visible = true;
			gridData.Cols[8].Width = 225;
			gridData.Cols[8].AllowDragging = false;
			gridData.Cols[8].AllowEditing = false;
			gridData.Cols[8].AllowMerging = false;
			gridData.Cols[8].AllowResizing = true;
			gridData.Cols[8].AllowSorting = true;
			gridData.Cols[8].Caption = "Статус";

			// выделяеем цветом удаленые записи
			for (int i = 2; i < gridData.Rows.Count; i++)
			{
				if ((gridData.GetData(i, 2).ToString().Trim() == "1") || (gridData.GetData(i, 2).ToString().Trim() == "True"))
				{
					gridData.Rows[i].Style = gridData.Styles["Red"];
				}
			}


		}

		private void btnClearDesigner_Click(object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("Сбросить данные о дизайнере?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
				{
					SqlCommand c =
						new SqlCommand(
							"UPDATE [order] SET [id_user_designer] = 0, [name_designer] = ''" +
							" WHERE [id_order] = " + gridData.Rows[gridData.Row][1].ToString(), db_connection);
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

		private void btnClearOperator_Click(object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("Сбросить данные об операторе?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
				{
					SqlCommand c =
						new SqlCommand(
							"UPDATE [order] SET [id_user_operator] = 0, [name_operator] = ''" +
							" WHERE [id_order] = " + gridData.Rows[gridData.Row][1].ToString(), db_connection);
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

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			LoadData();
		}

	}
}