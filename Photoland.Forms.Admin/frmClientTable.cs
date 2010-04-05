using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Components.FilterRow;
using Photoland.Security.User;

namespace Photoland.Forms.Admin
{
	public partial class frmClientTable : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tbl = new DataTable("data");
		private FilterRow FilterR;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		public frmClientTable(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Клиенты";

			this.db_connection = db_connection;
			this.usr = usr;
		}

		private void frmClientTable_Load(object sender, EventArgs e)
		{
			FilterR = new FilterRow(gridData);

			//LoadData();
		}

		private void LoadData(string strSearch)
		{
			tbl.Rows.Clear();

			db_command = new SqlCommand("SELECT [id_client], [del], [name], [category], [phone_1], [phone_2], [address], [email], [icq], [addon] FROM [vwAdminClient] WHERE [name] LIKE '" + strSearch + "%' ORDER BY [category], [name]", db_connection);
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
			gridData.Cols[9].Visible = false;
			gridData.Cols[10].Visible = false;

			gridData.Cols[3].Visible = true;
			gridData.Cols[3].Width = 300;
			gridData.Cols[3].AllowDragging = false;
			gridData.Cols[3].AllowEditing = false;
			gridData.Cols[3].AllowMerging = false;
			gridData.Cols[3].AllowResizing = true;
			gridData.Cols[3].AllowSorting = true;
			gridData.Cols[3].Caption = "ФИО";

			gridData.Cols[4].Visible = true;
			gridData.Cols[4].Width = 120;
			gridData.Cols[4].AllowDragging = false;
			gridData.Cols[4].AllowEditing = false;
			gridData.Cols[4].AllowMerging = false;
			gridData.Cols[4].AllowResizing = true;
			gridData.Cols[4].AllowSorting = true;
			gridData.Cols[4].Caption = "Категория";

			gridData.Cols[5].Visible = true;
			gridData.Cols[5].Width = 150;
			gridData.Cols[5].AllowDragging = false;
			gridData.Cols[5].AllowEditing = false;
			gridData.Cols[5].AllowMerging = false;
			gridData.Cols[5].AllowResizing = true;
			gridData.Cols[5].AllowSorting = true;
			gridData.Cols[5].Caption = "Телефон";

			gridData.Cols[6].Visible = true;
			gridData.Cols[6].Width = 150;
			gridData.Cols[6].AllowDragging = false;
			gridData.Cols[6].AllowEditing = false;
			gridData.Cols[6].AllowMerging = false;
			gridData.Cols[6].AllowResizing = true;
			gridData.Cols[6].AllowSorting = true;
			gridData.Cols[6].Caption = "Доп. Телефон";

			gridData.Cols[7].Visible = true;
			gridData.Cols[7].Width = 150;
			gridData.Cols[7].AllowDragging = false;
			gridData.Cols[7].AllowEditing = false;
			gridData.Cols[7].AllowMerging = false;
			gridData.Cols[7].AllowResizing = true;
			gridData.Cols[7].AllowSorting = true;
			gridData.Cols[7].Caption = "Адрес";

			gridData.Cols[8].Visible = true;
			gridData.Cols[8].Width = 150;
			gridData.Cols[8].AllowDragging = false;
			gridData.Cols[8].AllowEditing = false;
			gridData.Cols[8].AllowMerging = false;
			gridData.Cols[8].AllowResizing = true;
			gridData.Cols[8].AllowSorting = true;
			gridData.Cols[8].Caption = "E-mail";

			gridData.Cols[9].Visible = true;
			gridData.Cols[9].Width = 120;
			gridData.Cols[9].AllowDragging = false;
			gridData.Cols[9].AllowEditing = false;
			gridData.Cols[9].AllowMerging = false;
			gridData.Cols[9].AllowResizing = true;
			gridData.Cols[9].AllowSorting = true;
			gridData.Cols[9].Caption = "ICQ";

			gridData.Cols[10].Visible = true;
			gridData.Cols[10].Width = 200;
			gridData.Cols[10].AllowDragging = false;
			gridData.Cols[10].AllowEditing = false;
			gridData.Cols[10].AllowMerging = false;
			gridData.Cols[10].AllowResizing = true;
			gridData.Cols[10].AllowSorting = true;
			gridData.Cols[10].Caption = "Информация";

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
				frmEditClient f = new frmEditClient(db_connection, usr);
				f.ShowDialog();
				if (f.DialogResult == DialogResult.OK)
					LoadData(f.txtName.Text.Trim());
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
					frmEditClient f = new frmEditClient(db_connection, usr, gridData.Rows[gridData.Row][1].ToString());
					f.ShowDialog();
					if (f.DialogResult == DialogResult.OK)
						LoadData(f.txtName.Text.Trim());
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
							"UPDATE [client] SET [del] = 1" +
							" WHERE id_client = " + gridData.Rows[gridData.Row][1].ToString(), db_connection);
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
				LoadData("~~~");
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void gridData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (gridData.Rows[gridData.Row][1].ToString() != "")
                {
                    frmEditClient f = new frmEditClient(db_connection, usr, gridData.Rows[gridData.Row][1].ToString());
                    f.ShowDialog();
                    if (f.DialogResult == DialogResult.OK)
                        LoadData(f.txtName.Text.Trim());
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

		private void tmr_Tick(object sender, EventArgs e)
		{
			if (txtSearch.Text != "")
			{
				gridData.Enabled = false;
				lblLoad.Visible = true;
				Application.DoEvents();
				LoadData(txtSearch.Text);
				tmr.Stop();
				lblLoad.Visible = false;
				gridData.Enabled = true;
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			if (txtSearch.Text != "")
			{
				tmr.Stop();
				tmr.Start();
			}
		}

		private void btnShowAll_Click(object sender, EventArgs e)
		{
			gridData.Enabled = false;
			lblLoad.Visible = true;
			Application.DoEvents();
			LoadData("");
			tmr.Stop();
			lblLoad.Visible = false;
			gridData.Enabled = true;
		}

	
	
	}
}