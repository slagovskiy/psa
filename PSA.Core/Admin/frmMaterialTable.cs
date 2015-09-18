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
	public partial class frmMaterialTable : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tbl = new DataTable("data");
		private FilterRow FilterR;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		public frmMaterialTable(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Материал";

			this.db_connection = db_connection;
			this.usr = usr;
		}

		private void frmMaterialTable_Load(object sender, EventArgs e)
		{
			FilterR = new FilterRow(gridData);

			LoadData();

		}
		private void LoadData()
		{
			tbl.Rows.Clear();

			db_command = new SqlCommand("SELECT [id_material], CAST([del] AS BIT) AS [del], [material], [remainder] FROM [vwAdminMaterial] ORDER BY [material]", db_connection);
			db_adapter = new SqlDataAdapter(db_command);
			db_adapter.Fill(tbl);

			gridData.DataSource = tbl;

			gridData.Cols[1].Visible = false;
			gridData.Cols[2].Visible = false;
			gridData.Cols[3].Visible = false;
			gridData.Cols[4].Visible = false;

			gridData.Cols[1].Visible = true;
			gridData.Cols[1].Width = 100;
			gridData.Cols[1].AllowDragging = false;
			gridData.Cols[1].AllowEditing = false;
			gridData.Cols[1].AllowMerging = false;
			gridData.Cols[1].AllowResizing = true;
			gridData.Cols[1].AllowSorting = true;
			gridData.Cols[1].Caption = "Код 1С";

			gridData.Cols[3].Visible = true;
			gridData.Cols[3].Width = 745;
			gridData.Cols[3].AllowDragging = false;
			gridData.Cols[3].AllowEditing = false;
			gridData.Cols[3].AllowMerging = false;
			gridData.Cols[3].AllowResizing = true;
			gridData.Cols[3].AllowSorting = true;
			gridData.Cols[3].Caption = "Материал";

			gridData.Cols[4].Visible = true;
			gridData.Cols[4].Width = 100;
			gridData.Cols[4].AllowDragging = false;
			gridData.Cols[4].AllowEditing = false;
			gridData.Cols[4].AllowMerging = false;
			gridData.Cols[4].AllowResizing = true;
			gridData.Cols[4].AllowSorting = true;
			gridData.Cols[4].Caption = "Остаток";

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
				frmEditMaterial f = new frmEditMaterial(db_connection, usr);
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
					frmEditMaterial f = new frmEditMaterial(db_connection, usr, gridData.Rows[gridData.Row][1].ToString());
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
							"UPDATE [material] SET [del] = 1" +
							" WHERE [id_material] = '" + gridData.Rows[gridData.Row][1].ToString() + "'", db_connection);
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