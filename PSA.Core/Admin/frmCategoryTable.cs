﻿using System;
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
	public partial class frmCategoryTable : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tbl = new DataTable("data");
		private FilterRow FilterR;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		public frmCategoryTable(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Категории клиентов";

			this.db_connection = db_connection;
			this.usr = usr;
		}

		private void frmCategoryTable_Load(object sender, EventArgs e)
		{
			FilterR = new FilterRow(gridData);

			LoadData();
		}

		private void LoadData()
		{
			tbl.Rows.Clear();

			db_command = new SqlCommand("SELECT [id_category], [del], [name], CAST([input] AS BIT) AS [input] FROM [vwAdminCategory] ORDER BY [name]", db_connection);
			db_adapter = new SqlDataAdapter(db_command);
			db_adapter.Fill(tbl);

			gridData.DataSource = tbl;

			gridData.Cols[1].Visible = false;
			gridData.Cols[2].Visible = false;
			gridData.Cols[3].Visible = false;
			gridData.Cols[4].Visible = false;

			gridData.Cols[3].Visible = true; 
			gridData.Cols[3].Width = 865;
			gridData.Cols[3].AllowDragging = false;
			gridData.Cols[3].AllowEditing = false;
			gridData.Cols[3].AllowMerging = false;
			gridData.Cols[3].AllowResizing = true;
			gridData.Cols[3].AllowSorting = true;
			gridData.Cols[3].Caption = "Наименование";

			gridData.Cols[4].Visible = true;
			gridData.Cols[4].Width = 80;
			gridData.Cols[4].AllowDragging = false;
			gridData.Cols[4].AllowEditing = false;
			gridData.Cols[4].AllowMerging = false;
			gridData.Cols[4].AllowResizing = true;
			gridData.Cols[4].AllowSorting = true;
			gridData.Cols[4].Caption = "Открыто";

			// выделяеем цветом удаленые записи
			for (int i = 2; i < gridData.Rows.Count; i++)
			{
				if (gridData.GetData(i, 2).ToString().Trim() == "1")
				{
					gridData.Rows[i].Style = gridData.Styles["Red"];
				}
			}


		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				frmEditCategory f = new frmEditCategory(db_connection, usr);
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
					frmEditCategory f = new frmEditCategory(db_connection, usr, gridData.Rows[gridData.Row][1].ToString());
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
							"UPDATE [category] SET [del] = 1" +
							" WHERE id_category = " + gridData.Rows[gridData.Row][1].ToString(), db_connection);
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