using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.User;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Components.FilterRow;

namespace Photoland.Acceptance.Wizard
{
	public partial class frmStep2 : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public ClientInfo client;

		private SqlCommand db_command = new SqlCommand();
		//private SqlDataReader db_reader;
		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		private FilterRow fRow;


        //////////////////////////////////////////////////////////////////////////
        private bool CheckState(SqlConnection c)
        {
            bool r = false;
            if (!TestCommand(c))
            {
                frmWaitConnection w = new frmWaitConnection();
                while (!TestCommand(c))
                {
                    Application.DoEvents();
                    w = new frmWaitConnection();
                    w.ShowDialog();
                    if (w.DialogResult == DialogResult.Cancel)
                    {
                        r = false;
                        break;
                    }
                }
                if (TestCommand(c))
                    r = true;
            }
            else
            {
                r = true;
            }
            return r;
        }

        private bool TestCommand(SqlConnection c)
        {
            bool r = false;
            try
            {
                if (c.State != ConnectionState.Open)
                    c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select getdate()";
                DateTime _r = (DateTime)cmd.ExecuteScalar();
                r = true;
            }
            catch (Exception ex)
            {
                r = false;
            }
            return r;
        }
        //////////////////////////////////////////////////////////////////////////

        public frmStep2()
		{
			InitializeComponent();
			this.Text = "Мастер приемки заказов: шаг 2";
			fRow = new FilterRow(GridClient);
		}

		private void frmStep2_Load(object sender, EventArgs e)
		{
			ReBildTable();
			lblUserInfo.Text = usr.Name + "\n" + usr.Post + "\n" + usr.Point;
		}

		private void ReBildTable()
		{
            if (CheckState(db_connection))
            {
                db_command.Connection = db_connection;
                db_command.CommandText = "SELECT [id_client], [name] FROM [vwClientList] ORDER BY [name]";
                SqlDataAdapter db_adapter = new SqlDataAdapter(db_command);
                DataTable dt = new DataTable();

                db_adapter.Fill(dt);
                dt.Columns["name"].ColumnName = "Клиенты";
                GridClient.DataSource = dt;
                GridClient.Cols[0].Visible = false;
                GridClient.Cols[1].Width = 337;
                GridClient.Cols[1].AllowDragging = false;
                GridClient.Cols[1].AllowEditing = false;
                GridClient.Cols[1].AllowMerging = false;
                GridClient.Cols[1].AllowResizing = true;
                GridClient.Cols[1].AllowSorting = true;
                GridClient.Rows.DefaultSize = 25;
            }
		}

        private void SelectClient(string id_client, string guid)
        {
            if (CheckState(db_connection))
            {
                if (id_client != "")
                    client = new ClientInfo(int.Parse(id_client), db_connection);
                else
                    client = new ClientInfo(0, guid, db_connection);
                lblName.Text = client.Name;
                lblPhone1.Text = "Телефон: " + client.Phone1;
                lblPhone2.Text = "Доп телефон: " + client.Phone2;
                lblAddress.Text = "Адрес: " + client.Address;
                lblemail.Text = "Эл. почта: " + client.Email;
                lblicq.Text = "ICQ: " + client.Icq;
                lbladdon.Text = "Дополнительно: " + client.Addon;
                lblcategory.Text = "Категория: " + client.Category_name;

                btnNext.Enabled = true;
                /*
                db_command.Connection = db_connection;
                if (guid != "")
                {
                    db_command.CommandText = "SELECT [id_client], [name], [phone_1], [phone_2], [address], [email], [icq], [addon], [categoryname] FROM [PSAv1].[dbo].[vwClientFull] WHERE [guid] = '" + guid + "'";
                }
                if (id_client != "")
                {
                    db_command.CommandText = "SELECT [id_client], [name], [phone_1], [phone_2], [address], [email], [icq], [addon], [categoryname] FROM [PSAv1].[dbo].[vwClientFull] WHERE [id_client] = " + id_client;
                }
                db_reader = db_command.ExecuteReader();
                if (db_reader.Read())
                {
                    if (!db_reader.IsDBNull(1))
                        lblName.Text = db_reader.GetString(1);
                    else
                        lblName.Text = "";
                    if (!db_reader.IsDBNull(2))
                        lblPhone1.Text = "Телефон: " + db_reader.GetString(2);
                    else
                        lblPhone1.Text = "Телефон: -";
                    if (!db_reader.IsDBNull(3))
                        lblPhone2.Text = "Доп телефон: " + db_reader.GetString(3);
                    else
                        lblPhone2.Text = "Телефон: -";
                    if (!db_reader.IsDBNull(4))
                        lblAddress.Text = "Адрес: " + db_reader.GetString(4);
                    else
                        lblAddress.Text = "Адрес: -";
                    if (!db_reader.IsDBNull(5))
                        lblemail.Text = "Эл. почта: " + db_reader.GetString(5);
                    else
                        lblemail.Text = "Эл. почта: -";
                    if (!db_reader.IsDBNull(6))
                        lblicq.Text = "ICQ: " + db_reader.GetString(6);
                    else
                        lblicq.Text = "ICQ: -";
                    if (!db_reader.IsDBNull(7))
                        lbladdon.Text = "Дополнительно: " + db_reader.GetString(7);
                    else
                        lbladdon.Text = "Дополнительно: -";
                    if (!db_reader.IsDBNull(8))
                        lblcategory.Text = "Категория: " + db_reader.GetString(8);
                    else
                        lblcategory.Text = "Категория: ?!";

                    btnNext.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Ошибка получения данных о клиенте!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnNext.Enabled = false;
                }
                db_reader.Close();
                */
            }
        }

		private void btnBack_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Retry;
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Отменить заказ?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				this.DialogResult = DialogResult.Cancel;
		}

		private void GridClient_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.ToString() == "\r")
				if (GridClient.GetData(GridClient.Row, 0) != null)
				{
					SelectClient(GridClient.GetData(GridClient.Row, 0).ToString(), "");
				}
				else
					e.Handled = false;
			else
				e.Handled = false;
		}

		private void GridClient_DoubleClick(object sender, EventArgs e)
		{
			if (GridClient.GetData(GridClient.Row, 0) != null)
			{
				SelectClient(GridClient.GetData(GridClient.Row, 0).ToString(), "");
			}
		}

		private void GridClient_AfterResizeColumn(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
		{
			if (GridClient.Cols[1].Width < 337)
				GridClient.Cols[1].Width = 337;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			lblName.Text = "Имя";
			lblPhone1.Text = "Телефон: ";
			lblPhone2.Text = "Доп телефон: ";
			lblAddress.Text = "Адрес: ";
			lblemail.Text = "Эл. почта: ";
			lblicq.Text = "ICQ: ";
			lbladdon.Text = "Дополнительно: ";
			lblcategory.Text = "Категория: ";
			btnNext.Enabled = false;
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                frmClient fClient = new frmClient(GridClient.GetData(GridClient.Row, 0).ToString());
                fClient.usr = usr;
                fClient.db_connection = db_connection;
                fClient.input = 1;
                fClient.ShowDialog();
                if (fClient.DialogResult == DialogResult.OK)
                {
                    SelectClient(GridClient.GetData(GridClient.Row, 0).ToString(), "");
                }
                fClient.Close();
            }
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                frmClient fClient = new frmClient();
                fClient.usr = usr;
                fClient.db_connection = db_connection;
                fClient.input = 1;
                fClient.ShowDialog();
                if (fClient.DialogResult == DialogResult.OK)
                {
                    SelectClient("", fClient.guid);
                }
                fClient.Close();
                ReBildTable();
            }
		}



	}
}