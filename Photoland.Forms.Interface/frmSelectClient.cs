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
using Photoland.Security.User;
using Photoland.Security.Client;

namespace Photoland.Forms.Interface
{
	public partial class frmSelectClient : Form
	{

		public SqlConnection db_connection;
		public ClientInfo client;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private UserInfo usr;
		private int idcategory = 0;


		public frmSelectClient(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Выбор клиента";
			this.db_connection = db_connection;
			this.usr = usr;
			FilterRow fRow = new FilterRow(GridClient);

			//ReBildTable();
		}

		public frmSelectClient(SqlConnection db_connection, UserInfo usr, string title)
		{
			InitializeComponent();
			this.Text = title;
			this.db_connection = db_connection;
			this.usr = usr;
			FilterRow fRow = new FilterRow(GridClient);
			
			//ReBildTable();
		}

		public frmSelectClient(SqlConnection db_connection, UserInfo usr, string title, int idcat)
		{
			InitializeComponent();
			this.Text = title;
			this.db_connection = db_connection;
			this.usr = usr;
			FilterRow fRow = new FilterRow(GridClient);
			idcategory = idcat;

			//ReBildTable();
		}

		private void ReBildTable(string strSearch)
		{
			if(idcategory > 0)
				db_command = new SqlCommand("SELECT [id_client], [name], [categoryname], [phone_1] + ' ' + [phone_2] as [phone], [address], [email], [icq], [addon] FROM [vwClientFull] WHERE [name] LIKE '" + strSearch + "%' AND [id_category] = " + idcategory.ToString() + " ORDER BY [name]", db_connection);
			else
				db_command = new SqlCommand("SELECT [id_client], [name], [categoryname], [phone_1] + ' ' + [phone_2] as [phone], [address], [email], [icq], [addon] FROM [vwClientFull] WHERE [name] LIKE '" + strSearch + "%' ORDER BY [name]", db_connection);
			db_command.CommandTimeout = 9000;
			db_adapter = new SqlDataAdapter(db_command);
			DataTable dt = new DataTable();

			db_adapter.Fill(dt);
			dt.Columns["name"].ColumnName = "Клиенты";
			dt.Columns["phone"].ColumnName = "Телефон";
			dt.Columns["address"].ColumnName = "Адрес";
			dt.Columns["email"].ColumnName = "e-mail";
			dt.Columns["icq"].ColumnName = "icq";
			dt.Columns["addon"].ColumnName = "Инфо";
			dt.Columns["categoryname"].ColumnName = "Категория";

			GridClient.DataSource = dt;
			GridClient.Cols[0].Visible = false;

			GridClient.Cols[1].Width = 450;
			GridClient.Cols[1].AllowDragging = false;
			GridClient.Cols[1].AllowEditing = false;
			GridClient.Cols[1].AllowMerging = false;
			GridClient.Cols[1].AllowResizing = true;
			GridClient.Cols[1].AllowSorting = true;

			GridClient.Cols[2].Width = 110;
			GridClient.Cols[2].AllowDragging = false;
			GridClient.Cols[2].AllowEditing = false;
			GridClient.Cols[2].AllowMerging = false;
			GridClient.Cols[2].AllowResizing = true;
			GridClient.Cols[2].AllowSorting = true;

			GridClient.Cols[3].Width = 90;
			GridClient.Cols[3].AllowDragging = false;
			GridClient.Cols[3].AllowEditing = false;
			GridClient.Cols[3].AllowMerging = false;
			GridClient.Cols[3].AllowResizing = true;
			GridClient.Cols[3].AllowSorting = true;

			GridClient.Cols[4].Width = 90;
			GridClient.Cols[4].AllowDragging = false;
			GridClient.Cols[4].AllowEditing = false;
			GridClient.Cols[4].AllowMerging = false;
			GridClient.Cols[4].AllowResizing = true;
			GridClient.Cols[4].AllowSorting = true;

			GridClient.Cols[5].Width = 90;
			GridClient.Cols[5].AllowDragging = false;
			GridClient.Cols[5].AllowEditing = false;
			GridClient.Cols[5].AllowMerging = false;
			GridClient.Cols[5].AllowResizing = true;
			GridClient.Cols[5].AllowSorting = true;

			GridClient.Cols[6].Width = 90;
			GridClient.Cols[6].AllowDragging = false;
			GridClient.Cols[6].AllowEditing = false;
			GridClient.Cols[6].AllowMerging = false;
			GridClient.Cols[6].AllowResizing = true;
			GridClient.Cols[6].AllowSorting = true;

			GridClient.Cols[7].Width = 90;
			GridClient.Cols[7].AllowDragging = false;
			GridClient.Cols[7].AllowEditing = false;
			GridClient.Cols[7].AllowMerging = false;
			GridClient.Cols[7].AllowResizing = true;
			GridClient.Cols[7].AllowSorting = true;
			
			GridClient.Rows.DefaultSize = 20;
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			frmClient fClient = new frmClient();
			fClient.usr = usr;
			fClient.db_connection = db_connection;
			fClient.input = 1;
			fClient.ShowDialog();
            txtSearch.Text = fClient.txtName.Text;
            tmr.Stop();
            tmr.Start();
			fClient.Close();
			//ReBildTable();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			frmClient fClient = new frmClient(GridClient.GetData(GridClient.Row, 0).ToString());
			fClient.usr = usr;
			fClient.db_connection = db_connection;
			fClient.input = 1;
			fClient.ShowDialog();
			fClient.Close();
		}

		private void SelectClient(string id)
		{
			client = new ClientInfo(int.Parse(id), db_connection);
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
            if ((GridClient.Cols.Count > 0) && (GridClient.Row != -1))
            {
                if (GridClient.GetData(GridClient.Row, 0) != null)
                {
                    SelectClient(GridClient.GetData(GridClient.Row, 0).ToString());
                }
                if (this.client != null)
                    this.DialogResult = DialogResult.OK;
                else
                    this.DialogResult = DialogResult.Cancel;
            }
		}

		private void GridClient_DoubleClick(object sender, EventArgs e)
		{
            if (GridClient.Cols.Count != 0)
            {
                if (GridClient.GetData(GridClient.Row, 0) != null)
                {
                    SelectClient(GridClient.GetData(GridClient.Row, 0).ToString());
                }
            }
		}

		private void GridClient_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.ToString() == "\r")
                if (GridClient.Cols.Count != 0)
                {
                    if (GridClient.GetData(GridClient.Row, 0) != null)
                    {
                        SelectClient(GridClient.GetData(GridClient.Row, 0).ToString());
                    }
                    else
                        e.Handled = false;
                }
                else
                {
                }
            else
               e.Handled = false;
            
		}

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                tmr.Stop();
                tmr.Start();
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                GridClient.Enabled = false;
				lblLoad.Visible = true;
				Application.DoEvents();
				ReBildTable(txtSearch.Text);
                tmr.Stop();
				lblLoad.Visible = false;
                GridClient.Enabled = true;
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            GridClient.Enabled = false;
			lblLoad.Visible = true;
			Application.DoEvents();
			ReBildTable("");
            tmr.Stop();
			lblLoad.Visible = false;
            GridClient.Enabled = true;
        }

	}
}