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
	public partial class frmPriceTable : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tbl = new DataTable("data");
		private FilterRow FilterR;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		public frmPriceTable(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Прайс с проверкой";

			this.db_connection = db_connection;
			this.usr = usr;
		}

		private void frmPriceTable_Load(object sender, EventArgs e)
		{
			FilterR = new FilterRow(gridData);

			LoadData();

		}

		private void LoadData()
		{
			tbl.Rows.Clear();

			db_command = new SqlCommand("SELECT [id_good], [name], [type], [types], [check] FROM [vwAdminCheckPrice] ORDER BY [type], [name]", db_connection);
			db_adapter = new SqlDataAdapter(db_command);
			db_adapter.Fill(tbl);

			gridData.DataSource = tbl;

			gridData.Cols[1].Visible = false;
			gridData.Cols[2].Visible = false;
			gridData.Cols[3].Visible = false;
			gridData.Cols[4].Visible = false;
			gridData.Cols[5].Visible = false;

			gridData.Cols[2].Visible = true;
			gridData.Cols[2].Width = 800;
			gridData.Cols[2].AllowDragging = false;
			gridData.Cols[2].AllowEditing = false;
			gridData.Cols[2].AllowMerging = false;
			gridData.Cols[2].AllowResizing = true;
			gridData.Cols[2].AllowSorting = true;
			gridData.Cols[2].Caption = "Наименование";

			gridData.Cols[4].Visible = true;
			gridData.Cols[4].Width = 145;
			gridData.Cols[4].AllowDragging = false;
			gridData.Cols[4].AllowEditing = false;
			gridData.Cols[4].AllowMerging = false;
			gridData.Cols[4].AllowResizing = true;
			gridData.Cols[4].AllowSorting = true;
			gridData.Cols[4].Caption = "Тип";

			// выделяеем цветом удаленые записи
			for (int i = 2; i < gridData.Rows.Count; i++)
			{
				if (gridData.GetData(i, 5).ToString().Trim() == "no")
				{
					gridData.Rows[i].Style = gridData.Styles["Red"];
				}
				if (gridData.GetData(i, 5).ToString().Trim() == "ok")
				{
					gridData.Rows[i].Style = gridData.Styles["Green"];
				}
			}


		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				if (gridData.Rows[gridData.Row][1].ToString() != "")
				{
					frmEditPrice f = new frmEditPrice(db_connection, usr, gridData.Rows[gridData.Row][1].ToString());
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

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			if (prop.PathReportsTemplates != "")
			{
				try
				{
					SqlCommand t =
						new SqlCommand(
							"SELECT * FROM [vwAdminPriceReport]",
							db_connection);
					DataTable tb = new DataTable("Price");
					SqlDataAdapter da = new SqlDataAdapter(t);
					da.Fill(tb);
					rep.Load(prop.PathReportsTemplates, "Price 1");
					rep.DataSource.Recordset = tb;
					PrintPreviewDialog pd = new PrintPreviewDialog();
					pd.Document = rep.Document;
					pd.ShowDialog();
				}
				catch(Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
				}
			}
			else
			{
				MessageBox.Show("Не выбран файл шаблонов отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


	}
}